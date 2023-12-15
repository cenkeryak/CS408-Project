using Microsoft.VisualBasic.Logging;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ProjectServer
{

    public partial class Form1 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> userNames = new List<string>();
        List<int> userNameSocketIdx = new List<int>(); // size of this will be always same with UserNames and idx corressponds to each other (thread lock ensures this)
        // idx of userName corresponds to Socket of Client

        // Thread Lock ( it is needed to lock modifying userNames list) 
        static readonly object threadLock = new object();

        // these lists keep the subscribed usernames.
        List<string> spsSubs = new List<string>();
        List<string> ifSubs = new List<string>();

        bool terminating = false;
        bool listening = false;



        public Form1()
        {
            // Initialize components
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }




        

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // This code will execute when the form is in closing state
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBoxPort.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen();

                listening = true;
                buttonListen.Enabled = false;

                // Create accept thread that accepts the connection request
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                richTextBoxEventLog.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richTextBoxEventLog.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {

                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);

                    richTextBoxEventLog.AppendText("A client is connected.\n");

                    // Creates a recieve thread for each connected client
                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                catch
                {
                    // If there is a problem with .Accept()
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richTextBoxEventLog.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;

            // buffer size is determined by the max char size of message, max char size of username and enconding format (UTF-8)
            Byte[] buffer = new Byte[4096];

            string clientUserName;
            // Get username from client 
            try
            {
                // connected clients must send usernames to the server first. (in predetermined format, ' ' must be at the end of username)
                thisClient.Receive(buffer);

                string incomingMessage = Encoding.UTF8.GetString(buffer);
                incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf('\0'));

                // parse string to check username.
                clientUserName = incomingMessage.Substring(0, incomingMessage.IndexOf(' '));

                // check if the username is in the userNames;
                if (userNames.Contains(clientUserName))
                {
                    // username is not unique, do not accept

                    string messageToSend = "NO ";
                    buffer = Encoding.UTF8.GetBytes(messageToSend);
                    richTextBoxEventLog.AppendText("username is not accepted (not unique), given username: " + clientUserName + "\n");
                    try
                    {
                        thisClient.Send(buffer);
                    }
                    catch
                    {
                        richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                    }

                    // By design choice, when client enters non-unique username. We disconnect the user after informing cliet that it did not accept the username
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
                else
                {

                    // Username is unique, User is accepted
                    lock (threadLock)
                    {
                        // idx of username in usernames will be equal to idx of socketIdx in userNameSocketIdx list.
                        // socketIdx is the idx of users socket in socketList
                        userNames.Add(clientUserName);
                        userNameSocketIdx.Add(clientSockets.IndexOf(thisClient));
                    }


                    //this method reflesh the connected list
                    updateConnectedTB();

                    string messageToSend = "OK ";
                    buffer = Encoding.UTF8.GetBytes(messageToSend);

                    try
                    {
                        thisClient.Send(buffer);
                    }
                    catch
                    {
                        richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                    }

                }
            }
            catch
            {
                if (!terminating)
                {
                    richTextBoxEventLog.AppendText("A client has disconnected\n");
                }
                thisClient.Close();
                clientSockets.Remove(thisClient);
                connected = false;
                return;
            }

            // This is the main loop that listens the client with accepted username
            while (connected && !terminating)
            {
                try
                {
                    buffer = new Byte[4096];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.UTF8.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf('\0'));
                    richTextBoxEventLog.AppendText("Client: " + incomingMessage + "\n");

                    // Evaluate the message of the client

                    // parse the username
                    clientUserName = incomingMessage.Substring(0, incomingMessage.IndexOf(' '));

                    // Normally username must be in client username and this must always evaluate true. Check is done to be on the safe side
                    if (userNames.Contains(clientUserName))
                    {
                        // the command will be evaluated by this function
                        evaluateUserCommand(thisClient, clientUserName, incomingMessage);
                    }
                    else
                    {
                        // Although client defined correct username before, it doesn't send it correctly.
                        richTextBoxEventLog.AppendText("Client did not identify it self correctly!\n");
                    }
                }
                catch
                {
                    // lock is used since there is a removal from both usernames and usernameSocketIdx
                    lock (threadLock)
                    {
                        if (!terminating)
                        {
                            richTextBoxEventLog.AppendText("A client has disconnected\n");
                        }





                        thisClient.Close();
                        clientSockets.Remove(thisClient);
                        if (clientUserName != null)
                        {
                            // Username will be removed from lists
                            userNameSocketIdx.Remove(userNames.IndexOf(clientUserName));
                            userNames.Remove(clientUserName);


                            // Remove them if they are in subList
                            if (spsSubs.Contains(clientUserName))
                            {
                                spsSubs.Remove(clientUserName);
                            }
                            if (ifSubs.Contains(clientUserName))
                            {
                                ifSubs.Remove(clientUserName);
                            }
                        }
                    }

                    // Since these lists are updated (sps and if may not updated), reflesh all richTextBox of them
                    updateConnectedTB();
                    updateSpsTB();
                    updateIfTB();

                    connected = false;
                }
            }


        }

        // This function evaluates the command from the client (gets the whole message as incomingMessage)
        private void evaluateUserCommand(Socket client, string userName, string incomingMessage)
        {
            // from the 0th idx to first space is username. it is already parsed in the caller and passed to this function as parameter


            int idxFirstSpace = incomingMessage.IndexOf(' ');
            int idxSecondSpace = incomingMessage.IndexOf(' ', idxFirstSpace + 1);
            string userRequest = incomingMessage.Substring(idxFirstSpace + 1, idxSecondSpace - idxFirstSpace - 1);

            // user sends command after it send its username
            if (userRequest == "subscribe")
            {
                // check which channel user asks to subscribe
                int idxThirdSpace = incomingMessage.IndexOf(' ', idxSecondSpace + 1);
                string channelToSub = incomingMessage.Substring(idxSecondSpace + 1, idxThirdSpace - idxSecondSpace - 1);

                if (channelToSub == "sps101")
                {
                    if (!spsSubs.Contains(userName))
                    {
                        spsSubs.Add(userName);

                        richTextBoxEventLog.AppendText("a user with username:" + userName + " added to SPS 101 Channel. \n");
                        updateSpsTB();



                        string messageToSend = "subscribe sps101 OK ";
                        try
                        {
                            // respond to the user
                            Byte[] buffer = new Byte[4096];
                            buffer = Encoding.UTF8.GetBytes(messageToSend);

                            client.Send(buffer);
                        }
                        catch
                        {
                            richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                        }

                        // sending this info to all user in corresponding channel
                        string stringToSend = "sps101 " + userName + " subscribed ";

                        // this function will create sender thread to notify its subscribers
                        Thread sendThread = new Thread(() => sendMessageToSubs(spsSubs, stringToSend));
                        sendThread.Start();



                    } // ELSE: Do nothing since already user is in the channel (no need to inform)


                }
                else if (channelToSub == "if100")
                {
                    if (!ifSubs.Contains(userName))
                    {
                        ifSubs.Add(userName);

                        richTextBoxEventLog.AppendText("a user with username:" + userName + " added to IF 100 Channel. \n");
                        updateIfTB();



                        string messageToSend = "subscribe if100 OK ";
                        try
                        {
                            // respond to the user
                            Byte[] buffer = new Byte[4096];
                            buffer = Encoding.UTF8.GetBytes(messageToSend);

                            client.Send(buffer);
                        }
                        catch
                        {
                            richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                        }

                        // sending this info to all user in corresponding channel
                        string stringToSend = "if100 " + userName + " subscribed ";


                        // this function will create sender thread to notify its subscribers
                        Thread sendThread = new Thread(() => sendMessageToSubs(ifSubs, stringToSend));
                        sendThread.Start();

                    } // ELSE: Do nothing since already user is in the channel (no need to inform)
                }
                else
                {
                    // ERROR, WRONG CHANNEL NAME. This is not possible in our case since we only know 2 channel and client knows it
                    // So, it is not implemented
                }
            }
            else if (userRequest == "unsubscribe")
            {
                // check which channel user asks to subscribe
                int idxThirdSpace = incomingMessage.IndexOf(' ', idxSecondSpace + 1);
                string channelToUnSub = incomingMessage.Substring(idxSecondSpace + 1, idxThirdSpace - idxSecondSpace - 1);

                if (channelToUnSub == "sps101")
                {
                    if (spsSubs.Contains(userName))
                    {

                        spsSubs.Remove(userName);

                        richTextBoxEventLog.AppendText("a user with username:" + userName + " removed from SPS 101 Channel. \n");
                        updateSpsTB();



                        string messageToSend = "unsubscribe sps101 OK ";
                        try
                        {
                            // respond to the user
                            Byte[] buffer = new Byte[4096];
                            buffer = Encoding.UTF8.GetBytes(messageToSend);

                            client.Send(buffer);
                        }
                        catch
                        {
                            richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                        }

                        // sending this info to all user in corresponding channel
                        string stringToSend = "sps101 " + userName + " unsubscribed ";


                        // this function will create sender thread to notify its subscribers
                        Thread sendThread = new Thread(() => sendMessageToSubs(spsSubs, stringToSend));
                        sendThread.Start();
                    }
                }
                else if (channelToUnSub == "if100")
                {
                    if (ifSubs.Contains(userName))
                    {

                        ifSubs.Remove(userName);

                        richTextBoxEventLog.AppendText("a user with username:" + userName + " removed from IF 100 Channel. \n");
                        updateIfTB();



                        string messageToSend = "unsubscribe if100 OK ";
                        try
                        {
                            // respond to the user
                            Byte[] buffer = new Byte[4096];
                            buffer = Encoding.UTF8.GetBytes(messageToSend);

                            client.Send(buffer);
                        }
                        catch
                        {
                            richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                        }

                        // sending this info to all user in corresponding channel
                        string stringToSend = "if100 " + userName + " unsubscribed ";

                        // this function will create sender thread to notify its subscribers
                        Thread sendThread = new Thread(() => sendMessageToSubs(ifSubs, stringToSend));
                        sendThread.Start();


                    }
                }

            }
            else if (userRequest == "message")
            {
                // check which channel user asks to send message
                int idxThirdSpace = incomingMessage.IndexOf(' ', idxSecondSpace + 1);
                string channelToSend = incomingMessage.Substring(idxSecondSpace + 1, idxThirdSpace - idxSecondSpace - 1);

                int strLength = incomingMessage.Length;
                string messageToSend = incomingMessage.Substring(idxThirdSpace + 1, strLength - idxThirdSpace - 1);
                if (channelToSend == "if100")
                {
                    string stringToSend = "if100 " + userName + " message " + messageToSend; // Optional: there is no space at the end of message (for other responds, there is space) 

                    // This will create a seperate thread that send the given message to all the subscribers of the given channel
                    Thread sendThread = new Thread(() => sendMessageToSubs(ifSubs, stringToSend));
                    sendThread.Start();

                    richTextBoxEventLog.AppendText("message to IF 100 from:" + userName + " message:" + messageToSend + "\n");
                }
                else if (channelToSend == "sps101")
                {
                    string stringToSend = "sps101 " + userName + " message " + messageToSend; // Optional: there is no space at the end of message (for other responds, there is space) 

                    // This will create a seperate thread that send the given message to all the subscribers of the given channel
                    Thread sendThread = new Thread(() => sendMessageToSubs(spsSubs, stringToSend));
                    sendThread.Start();

                    richTextBoxEventLog.AppendText("message to SPS 101 from:" + userName + " message:" + messageToSend + "\n");
                }


            }


        }

        // This is a thread function to send message to all of the subscrribers of the given channnel. 
        private void sendMessageToSubs(List<string> channel, string message)
        {
            foreach (string userName in channel)
            {
                int indexOfUsername;
                int indexOfSocket;
                lock (threadLock)
                {
                    // By the design choice, username idx in usernames is the same as socketIdx in userNameSockerIdx list. socketIdx points to the position of the socket in socketList (by meaning points, it keeps the index)
                    // threadLock ensures their idx remains same.
                    indexOfUsername = userNames.FindIndex(x => x == userName);
                    indexOfSocket = userNameSocketIdx.IndexOf(indexOfUsername);

                    if (indexOfSocket >= 0)
                    {

                        Socket socketToSend = clientSockets[indexOfSocket];

                        try
                        {
                            Byte[] buffer = new Byte[4096];
                            buffer = Encoding.UTF8.GetBytes(message);

                            // send message to given socket of user
                            socketToSend.Send(buffer);
                        }
                        catch
                        {
                            richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + message + "\n");
                        }

                    }
                }



            }
        }


        // refleshes the connected list
        private void updateConnectedTB()
        {
            string connectedTB = "";
            foreach (string user in userNames)
            {
                connectedTB += user + "\n";
            }
            richTextBoxConnected.Text = connectedTB;
        }

        // refleshes the sps101 subs list
        private void updateSpsTB()
        {
            string spsTB = "";
            foreach (string user in spsSubs)
            {
                spsTB += user + "\n";
            }
            richTextBoxSps.Text = spsTB;
        }
        // refleshes the if100 subs list
        private void updateIfTB()
        {
            string ifTB = "";
            foreach (string user in ifSubs)
            {
                ifTB += user + "\n";
            }
            richTextBoxIf.Text = ifTB;
        }
    }
}