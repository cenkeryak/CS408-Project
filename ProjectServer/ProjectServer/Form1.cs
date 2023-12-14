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
        // idx of userName corresponds to Socket of Client

        List<string> spsSubs = new List<string>();
        List<string> ifSubs = new List<string>();

        bool terminating = false;
        bool listening = false;


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }




        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
                richTextBoxEventLog.AppendText(endPoint.ToString()+"\n");
                serverSocket.Bind(endPoint);
                serverSocket.Listen(10);

                listening = true;
                buttonListen.Enabled = false;


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

                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                catch
                {
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
            Byte[] buffer = new Byte[4096];

            string clientUserName;
            // Get username from client
            try
            {

                thisClient.Receive(buffer);

                string incomingMessage = Encoding.UTF8.GetString(buffer);
                incomingMessage = incomingMessage.Substring(0,incomingMessage.IndexOf('\0'));
                

                richTextBoxEventLog.AppendText("Message received: " + incomingMessage + "\n");

                // parse string to check username.
                clientUserName = incomingMessage.Substring(0, incomingMessage.IndexOf(" "));

                // check if the username is in the userNames;
                if (userNames.Contains(clientUserName))
                {
                    // username is not unique, do not accept

                    string messageToSend = "NO ";
                    buffer = Encoding.UTF8.GetBytes(messageToSend);

                    try
                    {
                        thisClient.Send(buffer);
                    }
                    catch
                    {
                        richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + messageToSend + "\n");
                    }

                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
                else
                {
                    // Username is unique, User is accepted
                    userNames.Add(clientUserName);

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
                    clientUserName = incomingMessage.Substring(0, incomingMessage.IndexOf(" "));

                    if (userNames.Contains(clientUserName))
                    {
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
                    if (!terminating)
                    {
                        richTextBoxEventLog.AppendText("A client has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    if (clientUserName != null)
                    {
                        // Username will be removed from the userName list only. it won't be removed from subscribed list. (Subject to change)
                        userNames.Remove(clientUserName);
                    }

                    connected = false;
                }
            }


        }
        private void evaluateUserCommand(Socket client, string userName, string incomingMessage)
        {
            int idxFirstSpace = incomingMessage.IndexOf(" ");
            int idxSecondSpace = incomingMessage.IndexOf(" ", idxFirstSpace + 1);
            string userRequest = incomingMessage.Substring(idxFirstSpace + 1, idxSecondSpace - idxFirstSpace - 1);

            if (userRequest == "subscribe")
            {
                // check which channel user asks to subscribe
                int idxThirdSpace = incomingMessage.IndexOf(" ", idxSecondSpace + 1);
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
                        sendMessageToSubs(spsSubs, stringToSend);

                        // Trigger event so that all subs of sps will know it. 

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
                        sendMessageToSubs(ifSubs, stringToSend);


                    } // ELSE: Do nothing since already user is in the channel (no need to inform)
                }
                else
                {
                    // ERROR, WRONG CHANNEL NAME.
                }
            }
            else if (userRequest == "unsubscribe")
            {
                // check which channel user asks to subscribe
                int idxThirdSpace = incomingMessage.IndexOf(" ", idxSecondSpace + 1);
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
                        sendMessageToSubs(spsSubs, stringToSend);
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
                        sendMessageToSubs(ifSubs, stringToSend);
                    }
                }

            }
            else if (userRequest == "message")
            {
                // check which channel user asks to send message
                int idxThirdSpace = incomingMessage.IndexOf(" ", idxSecondSpace + 1);
                string channelToSend = incomingMessage.Substring(idxSecondSpace + 1, idxThirdSpace - idxSecondSpace - 1);

                int strLength = incomingMessage.Length;
                string messageToSend = incomingMessage.Substring(idxThirdSpace + 1, strLength - idxThirdSpace - 1);
                if (channelToSend == "if100")
                {
                    string stringToSend = "if100 " + userName + " message " + messageToSend; // Optional: there is no space at the end of message (for other responds, there is space) 

                    Thread sendThread = new Thread(() => sendMessageToSubs(ifSubs, stringToSend)); // updated
                    sendThread.Start();

                    richTextBoxEventLog.AppendText("message to IF 100 from:" + userName + " message:" + messageToSend + "\n");
                }
                else if (channelToSend == "sps101")
                {
                    string stringToSend = "sps101 " + userName + " message " + messageToSend; // Optional: there is no space at the end of message (for other responds, there is space) 

                    Thread sendThread = new Thread(() => sendMessageToSubs(ifSubs, stringToSend));
                    sendThread.Start();

                    richTextBoxEventLog.AppendText("message to SPS 101 from:" + userName + " message:" + messageToSend + "\n");
                }


            }


        }
        private void sendMessageToSubs(List<string> channel, string message)
        {
            foreach (string userName in channel)
            {
                int indexOfUsername = userNames.FindIndex(x => x == userName);
                if (indexOfUsername >= 0)
                {
                    Socket socketToSend = clientSockets[indexOfUsername];

                    try
                    {
                        Byte[] buffer = new Byte[4096];
                        buffer = Encoding.UTF8.GetBytes(message);

                        socketToSend.Send(buffer);
                    }
                    catch
                    {
                        richTextBoxEventLog.AppendText("There is a problem! Server could not send the message:" + message + "\n");
                    }

                }
            }
        }



        private void updateConnectedTB()
        {
            string connectedTB = "";
            foreach (string user in userNames)
            {
                connectedTB += user + "\n";
            }
            richTextBoxConnected.Text = connectedTB;
        }

        private void updateSpsTB()
        {
            string spsTB = "";
            foreach (string user in spsSubs)
            {
                spsTB += user + "\n";
            }
            richTextBoxSps.Text = spsTB;
        }
        private void updateIfTB()
        {
            string ifTB = "";
            foreach (string user in ifSubs)
            {
                ifTB += user + "\n";
            }
            richTextBoxIf.Text = ifTB;
        }

        private void richTextBoxEventLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}