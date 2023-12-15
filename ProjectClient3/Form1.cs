using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ProjectClient
{



    public partial class Form1 : Form
    {
        Socket clientSocket;
        bool isConnected = false;
        bool isTerminated = true;
        bool isUsernameOk = false;




        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;

            chatBox100.Text = "YOU ARE NOT CONNECTED!";
            chatBox101.Text = "YOU ARE NOT CONNECTED!";


            warningLabel100.Visible = false;
            warningLabel101.Visible = false;
        }






        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isConnected = false;
            isTerminated = true;
            Environment.Exit(0);
        }






        private void Receive()
        {

            while (isConnected && clientSocket != null && clientSocket.Connected)
            {
                try
                {


                    Byte[] buffer = new Byte[4096];
                    clientSocket.Receive(buffer);
                    string receivedStr = Encoding.UTF8.GetString(buffer);
                    receivedStr = receivedStr.Substring(0, receivedStr.IndexOf('\0'));

                    if (!isConnected)
                    {
                        return;
                    }


                    // Checking the validity of the username
                    if (!isUsernameOk)
                    {
                        if (receivedStr == "OK ")
                        {
                            isUsernameOk = true;
                            tabPage2.Enabled = true;
                            tabPage3.Enabled = true;
                            chatBox100.Text = "";
                            chatBox101.Text = "";
                            chatBox100.ReadOnly = true;
                            chatBox101.ReadOnly = true;


                            messageBoxButton100.Enabled = false;
                            messageBoxButton101.Enabled = false;

                            richBox.AppendText("Connected succesfully!\n You can check the channels in the tab section\n");
                            pictureBox1.Image = ProjectClient.Properties.Resources.smile;

                        }
                        else
                        {
                            isConnected = false;
                            connectButton.Enabled = true;
                            nameText.Text = "";
                            richBox.AppendText("Username denied!\n");
                            connectButton.PerformClick();
                        }
                    }
                    else
                    {
                        //GENERAL MESSAGES AFTER USERNAME IS VERIFIED




                        int spaceIndex = receivedStr.IndexOf(" ");
                        string flag = receivedStr.Substring(0, spaceIndex);

                        //SUBSCRIBE RETURN  FORMAT:

                        // subscribe channel_name OK
                        if (flag == "subscribe")
                        {
                            int idxFirstSpace = receivedStr.IndexOf(" ");
                            int idxSecondSpace = receivedStr.IndexOf(" ", idxFirstSpace + 1);

                            string channelName = receivedStr.Substring(idxFirstSpace + 1, idxSecondSpace - idxFirstSpace - 1);
                            if (channelName == "sps101")
                            {
                                subscribeButton101.Text = "UNSUBSCRIBE";
                                messageBoxButton101.Enabled = true;
                            }
                            else
                            {
                                subscribeButton100.Text = "UNSUBSCRIBE";
                                messageBoxButton100.Enabled = true;
                            }
                        }



                        //UNSUBSCRIBE RETURN FORMAT:

                        // unsubscribe channel_name OK

                        else if (flag == "unsubscribe")
                        {
                            int idxFirstSpace = receivedStr.IndexOf(" ");
                            int idxSecondSpace = receivedStr.IndexOf(" ", idxFirstSpace + 1);

                            string channelName = receivedStr.Substring(idxFirstSpace + 1, idxSecondSpace - idxFirstSpace - 1);
                            if (channelName == "sps101")
                            {
                                subscribeButton101.Text = "SUBSCRIBE";
                                messageBoxButton101.Enabled = false;
                                chatBox101.AppendText("You left the channel.\n");
                            }
                            else
                            {
                                subscribeButton100.Text = "SUBSCRIBE";
                                messageBoxButton100.Enabled = false;
                                chatBox100.AppendText("You left the channel.\n");
                            }
                        }

                        //MESSAGE RETURN FORMAT

                        // message OK
                        else if (flag == "message")
                        {

                        }


                        //We will get our own message to display in our chat box, or someone else's message or info to display in our chatbox

                        //channel_name gonderen_kisi_username message [MESSAGE]
                        //channel_name gelen_kisi_username subscribe(unsubscribe)

                        else
                        {
                            //flag is now equal to channel_name



                            int idxFirstSpace = receivedStr.IndexOf(" ");
                            int idxSecondSpace = receivedStr.IndexOf(" ", idxFirstSpace + 1);
                            string usernameInChannel = receivedStr.Substring(idxFirstSpace + 1, idxSecondSpace - idxFirstSpace - 1);







                            char nextFlagInitial = receivedStr[receivedStr.IndexOf(usernameInChannel) + usernameInChannel.Length + 1];

                            if (nextFlagInitial == 'm')
                            {
                                //message

                                //could be someone else's message or our own
                                int startIndexOfMessage = receivedStr.IndexOf("message") + 8;

                                string messageInChannel = receivedStr.Substring(startIndexOfMessage, receivedStr.Length - startIndexOfMessage);

                                if (flag == "sps101")
                                {
                                    chatBox101.AppendText(usernameInChannel + ": " + messageInChannel + "\n");
                                }
                                else
                                {
                                    chatBox100.AppendText(usernameInChannel + ": " + messageInChannel + "\n");
                                }
                            }
                            else if (nextFlagInitial == 's')
                            {
                                //subscribe
                                // someone subscribed the channel and we get the info
                                if (flag == "sps101")
                                {
                                    chatBox101.AppendText(usernameInChannel + "  has joined the channel.\n");
                                }
                                else
                                {
                                    chatBox100.AppendText(usernameInChannel + " has joined the channel.\n");
                                }
                            }
                            else
                            {
                                //unsubscribe
                                // someone unsubscribed the channel and we get the info

                                if (flag == "sps101")
                                {
                                    chatBox101.AppendText(usernameInChannel + "  has left the channel.\n");
                                }
                                else
                                {
                                    chatBox100.AppendText(usernameInChannel + " has left the channel.\n");
                                }
                            }




                        }


                    }



                }
                catch
                {
                    if (isConnected)
                    {
                        //that means while we are connected to the server, our connection got lost due to server closing.
                        //if isConnected was false, that would mean we are the one that is disconnecting because connectButtonClick,
                        //sets the isConnected as false if we are disconnecting
                        richBox.AppendText("Server closed: ");
                        tabControl.SelectedTab = tabPage1;
                        connectButton.PerformClick();
                    }


                }
            }


        }







        //SUBSCRIBE BUTTON CLICK

        //username subscribe channel_name 



        //UNSUBSCRIBE BUTTON CLICK
        //username unsubscribe channel_name


        //SEND MESSAGE BUTTON CLICK

        // AFTER CHAR LENGTH CHECKS
        // username message channel_name [MESSAGE]



        //in order to avoid freezing, connection is dealt within a thread
        private void connectionThread()
        {
            string IP = ipText.Text;
            int portNumber;

            if (Int32.TryParse(portText.Text, out portNumber))
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {

                    string username = nameText.Text;
                    username = username.Trim();

                    if (username.Length < 2 || username.Length > 32 || username.IndexOf(" ") > 0)
                    {
                        richBox.AppendText("Username length should be between 2-32 characters and should not contain whitespace between\n");

                        nameText.Text = "";
                    }
                    else
                    {


                        clientSocket.Connect(IP, portNumber);
                        isConnected = true;


                        clientSocket.Send(Encoding.UTF8.GetBytes(username + " "));

                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();


                        tabPage2.Enabled = true;
                        tabPage3.Enabled = true;

                        chatBox100.Text = "";
                        chatBox101.Text = "";


                        connectButton.Text = "DISCONNECT";
                        connectButton.BackColor = Color.RebeccaPurple;


                    }

                }
                catch
                {
                    richBox.AppendText("Problem occured while connecting...\n");

                }



            }
            else
            {
                richBox.AppendText("Problem occured while connecting...\n");
            }
        }
        private void connectButton_Click(object sender, EventArgs e)
        {

            //In order to have a toggling effect, we have a if check.
            //Because as we connect, we set the text as disconnect and vice versa.
            if (connectButton.Text == "DISCONNECT")
            {


                isConnected = false;
                isUsernameOk = false;
                tabPage2.Refresh();
                tabPage3.Refresh();

                tabPage2.Enabled = false;
                tabPage3.Enabled = false;

                chatBox100.Text = "YOU ARE NOT CONNECTED!";
                chatBox101.Text = "YOU ARE NOT CONNECTED!";

                subscribeButton100.Text = "SUBSCRIBE";
                subscribeButton101.Text = "SUBSCRIBE";


                clientSocket.Shutdown(SocketShutdown.Send);
                clientSocket.Close();
                connectButton.Text = "CONNECT";
                connectButton.BackColor = Color.Green;

                richBox.AppendText("Disconnected successfully!\n");
                pictureBox1.Image = ProjectClient.Properties.Resources.normal;

                return;
            }

            Thread connectThread = new Thread(connectionThread);
            connectThread.Start();

            
            


        }
        //Listening subscribe button in IF100 channel
        private void subscribeButton100_Click(object sender, EventArgs e)
        {
            string textToBeSent;

            if (subscribeButton100.Text == "SUBSCRIBE")
            {
                textToBeSent = nameText.Text + " " + "subscribe" + " " + "if100";

            }
            else
            {
                textToBeSent = nameText.Text + " " + "unsubscribe" + " " + "if100";

            }
            clientSocket.Send(Encoding.UTF8.GetBytes(textToBeSent + " "));

        }

        //Listening subscribe button in SPS101 channel
        private void subscribeButton101_Click(object sender, EventArgs e)
        {
            string textToBeSent;

            if (subscribeButton101.Text == "SUBSCRIBE")
            {
                textToBeSent = nameText.Text + " " + "subscribe" + " " + "sps101";

            }
            else
            {
                textToBeSent = nameText.Text + " " + "unsubscribe" + " " + "sps101";

            }

            clientSocket.Send(Encoding.UTF8.GetBytes(textToBeSent + " "));
        }
        //Listening subscribe button in IF100 channel
        private void messageBoxButton100_Click(object sender, EventArgs e)
        {
            string enteredMessage = messageBox100.Text;
            if (enteredMessage.Length < 1 || enteredMessage.Length > 1000)
            {
                warningLabel100.Text = "Message length should be between 1-1000";
                warningLabel100.Visible = true;
            }
            else
            {
                if (warningLabel100.Visible)
                {
                    warningLabel100.Visible = false;
                }
                string textToBeSent = nameText.Text + " " + "message" + " " + "if100" + " " + enteredMessage;
                clientSocket.Send(Encoding.UTF8.GetBytes(textToBeSent + " "));
            }
            messageBox100.Text = "";
        }
        //Listening subscribe button in SPS101 channel
        private void messageBoxButton101_Click(object sender, EventArgs e)
        {
            string enteredMessage = messageBox101.Text;
            if (enteredMessage.Length < 1 || enteredMessage.Length > 1000)
            {
                warningLabel101.Text = "Message length should be between 1-1000";
                warningLabel101.Visible = true;
            }
            else
            {
                if (warningLabel101.Visible)
                {
                    warningLabel101.Visible = false;
                }
                string textToBeSent = nameText.Text + " " + "message" + " " + "sps101" + " " + enteredMessage;
                clientSocket.Send(Encoding.UTF8.GetBytes(textToBeSent + " "));
                messageBox101.Text = "";
            }
        }
    }
}