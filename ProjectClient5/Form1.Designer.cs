namespace ProjectClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.brandLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.richBox = new System.Windows.Forms.RichTextBox();
            this.nameText = new System.Windows.Forms.TextBox();
            this.portText = new System.Windows.Forms.TextBox();
            this.ipText = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.warningLabel100 = new System.Windows.Forms.Label();
            this.messageBoxButton100 = new System.Windows.Forms.Button();
            this.messageBox100 = new System.Windows.Forms.RichTextBox();
            this.subscribeButton100 = new System.Windows.Forms.Button();
            this.chatBoxLabel100 = new System.Windows.Forms.Label();
            this.chatBox100 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.warningLabel101 = new System.Windows.Forms.Label();
            this.messageBoxButton101 = new System.Windows.Forms.Button();
            this.messageBox101 = new System.Windows.Forms.RichTextBox();
            this.subscribeButton101 = new System.Windows.Forms.Button();
            this.chatBoxLabel101 = new System.Windows.Forms.Label();
            this.chatBox101 = new System.Windows.Forms.RichTextBox();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.SlateBlue;
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.brandLabel);
            this.tabPage1.Controls.Add(this.connectButton);
            this.tabPage1.Controls.Add(this.richBox);
            this.tabPage1.Controls.Add(this.nameText);
            this.tabPage1.Controls.Add(this.portText);
            this.tabPage1.Controls.Add(this.ipText);
            this.tabPage1.Controls.Add(this.nameLabel);
            this.tabPage1.Controls.Add(this.portLabel);
            this.tabPage1.Controls.Add(this.ipLabel);
            this.tabPage1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 419);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HOME";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProjectClient.Properties.Resources.normal;
            this.pictureBox1.Location = new System.Drawing.Point(556, 312);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // brandLabel
            // 
            this.brandLabel.AutoSize = true;
            this.brandLabel.BackColor = System.Drawing.Color.SlateBlue;
            this.brandLabel.Font = new System.Drawing.Font("Nexa Bold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.brandLabel.Location = new System.Drawing.Point(472, 240);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.Size = new System.Drawing.Size(294, 74);
            this.brandLabel.TabIndex = 8;
            this.brandLabel.Text = "diSUcord";
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.Color.Green;
            this.connectButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.connectButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.connectButton.Location = new System.Drawing.Point(44, 322);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(355, 59);
            this.connectButton.TabIndex = 7;
            this.connectButton.Text = "CONNECT";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // richBox
            // 
            this.richBox.BackColor = System.Drawing.Color.LavenderBlush;
            this.richBox.Location = new System.Drawing.Point(472, 58);
            this.richBox.Name = "richBox";
            this.richBox.Size = new System.Drawing.Size(285, 190);
            this.richBox.TabIndex = 6;
            this.richBox.Text = "";
            // 
            // nameText
            // 
            this.nameText.BackColor = System.Drawing.Color.Pink;
            this.nameText.Location = new System.Drawing.Point(195, 221);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(204, 27);
            this.nameText.TabIndex = 5;
            // 
            // portText
            // 
            this.portText.BackColor = System.Drawing.Color.Pink;
            this.portText.Location = new System.Drawing.Point(195, 138);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(204, 27);
            this.portText.TabIndex = 4;
            // 
            // ipText
            // 
            this.ipText.BackColor = System.Drawing.Color.Pink;
            this.ipText.Location = new System.Drawing.Point(195, 64);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(204, 27);
            this.ipText.TabIndex = 3;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameLabel.Location = new System.Drawing.Point(44, 217);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(133, 31);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "USERNAME";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.portLabel.Location = new System.Drawing.Point(44, 132);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(69, 31);
            this.portLabel.TabIndex = 1;
            this.portLabel.Text = "PORT";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ipLabel.Location = new System.Drawing.Point(44, 58);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(33, 31);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "IP";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(0, 1);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 452);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.warningLabel100);
            this.tabPage2.Controls.Add(this.messageBoxButton100);
            this.tabPage2.Controls.Add(this.messageBox100);
            this.tabPage2.Controls.Add(this.subscribeButton100);
            this.tabPage2.Controls.Add(this.chatBoxLabel100);
            this.tabPage2.Controls.Add(this.chatBox100);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 419);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "IF100";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // warningLabel100
            // 
            this.warningLabel100.AutoSize = true;
            this.warningLabel100.Location = new System.Drawing.Point(407, 259);
            this.warningLabel100.Name = "warningLabel100";
            this.warningLabel100.Size = new System.Drawing.Size(122, 20);
            this.warningLabel100.TabIndex = 5;
            this.warningLabel100.Text = "warningLabel100";
            // 
            // messageBoxButton100
            // 
            this.messageBoxButton100.Location = new System.Drawing.Point(686, 285);
            this.messageBoxButton100.Name = "messageBoxButton100";
            this.messageBoxButton100.Size = new System.Drawing.Size(94, 77);
            this.messageBoxButton100.TabIndex = 4;
            this.messageBoxButton100.Text = "SEND";
            this.messageBoxButton100.UseVisualStyleBackColor = true;
            this.messageBoxButton100.Click += new System.EventHandler(this.messageBoxButton100_Click);
            // 
            // messageBox100
            // 
            this.messageBox100.Location = new System.Drawing.Point(407, 285);
            this.messageBox100.Name = "messageBox100";
            this.messageBox100.Size = new System.Drawing.Size(273, 77);
            this.messageBox100.TabIndex = 3;
            this.messageBox100.Text = "";
            // 
            // subscribeButton100
            // 
            this.subscribeButton100.Location = new System.Drawing.Point(57, 285);
            this.subscribeButton100.Name = "subscribeButton100";
            this.subscribeButton100.Size = new System.Drawing.Size(227, 77);
            this.subscribeButton100.TabIndex = 2;
            this.subscribeButton100.Text = "SUBSCRIBE";
            this.subscribeButton100.UseVisualStyleBackColor = true;
            this.subscribeButton100.Click += new System.EventHandler(this.subscribeButton100_Click);
            // 
            // chatBoxLabel100
            // 
            this.chatBoxLabel100.AutoSize = true;
            this.chatBoxLabel100.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chatBoxLabel100.Location = new System.Drawing.Point(57, 11);
            this.chatBoxLabel100.Name = "chatBoxLabel100";
            this.chatBoxLabel100.Size = new System.Drawing.Size(103, 28);
            this.chatBoxLabel100.TabIndex = 1;
            this.chatBoxLabel100.Text = "CHAT BOX";
            // 
            // chatBox100
            // 
            this.chatBox100.Location = new System.Drawing.Point(57, 44);
            this.chatBox100.Name = "chatBox100";
            this.chatBox100.Size = new System.Drawing.Size(672, 177);
            this.chatBox100.TabIndex = 0;
            this.chatBox100.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.warningLabel101);
            this.tabPage3.Controls.Add(this.messageBoxButton101);
            this.tabPage3.Controls.Add(this.messageBox101);
            this.tabPage3.Controls.Add(this.subscribeButton101);
            this.tabPage3.Controls.Add(this.chatBoxLabel101);
            this.tabPage3.Controls.Add(this.chatBox101);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(792, 419);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SPS101";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // warningLabel101
            // 
            this.warningLabel101.AutoSize = true;
            this.warningLabel101.Location = new System.Drawing.Point(395, 262);
            this.warningLabel101.Name = "warningLabel101";
            this.warningLabel101.Size = new System.Drawing.Size(50, 20);
            this.warningLabel101.TabIndex = 7;
            this.warningLabel101.Text = "label1";
            // 
            // messageBoxButton101
            // 
            this.messageBoxButton101.Location = new System.Drawing.Point(674, 285);
            this.messageBoxButton101.Name = "messageBoxButton101";
            this.messageBoxButton101.Size = new System.Drawing.Size(94, 77);
            this.messageBoxButton101.TabIndex = 6;
            this.messageBoxButton101.Text = "SEND";
            this.messageBoxButton101.UseVisualStyleBackColor = true;
            this.messageBoxButton101.Click += new System.EventHandler(this.messageBoxButton101_Click);
            // 
            // messageBox101
            // 
            this.messageBox101.Location = new System.Drawing.Point(395, 285);
            this.messageBox101.Name = "messageBox101";
            this.messageBox101.Size = new System.Drawing.Size(273, 77);
            this.messageBox101.TabIndex = 5;
            this.messageBox101.Text = "";
            // 
            // subscribeButton101
            // 
            this.subscribeButton101.Location = new System.Drawing.Point(57, 285);
            this.subscribeButton101.Name = "subscribeButton101";
            this.subscribeButton101.Size = new System.Drawing.Size(227, 77);
            this.subscribeButton101.TabIndex = 4;
            this.subscribeButton101.Text = "SUBSCRIBE";
            this.subscribeButton101.UseVisualStyleBackColor = true;
            this.subscribeButton101.Click += new System.EventHandler(this.subscribeButton101_Click);
            // 
            // chatBoxLabel101
            // 
            this.chatBoxLabel101.AutoSize = true;
            this.chatBoxLabel101.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chatBoxLabel101.Location = new System.Drawing.Point(57, 13);
            this.chatBoxLabel101.Name = "chatBoxLabel101";
            this.chatBoxLabel101.Size = new System.Drawing.Size(103, 28);
            this.chatBoxLabel101.TabIndex = 3;
            this.chatBoxLabel101.Text = "CHAT BOX";
            // 
            // chatBox101
            // 
            this.chatBox101.Location = new System.Drawing.Point(57, 46);
            this.chatBox101.Name = "chatBox101";
            this.chatBox101.Size = new System.Drawing.Size(672, 177);
            this.chatBox101.TabIndex = 2;
            this.chatBox101.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabPage tabPage1;
        private TextBox nameText;
        private TextBox portText;
        private TextBox ipText;
        private Label nameLabel;
        private Label portLabel;
        private Label ipLabel;
        private TabControl tabControl;
        private Button connectButton;
        private RichTextBox richBox;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button subscribeButton100;
        private Label chatBoxLabel100;
        private RichTextBox chatBox100;
        private Label chatBoxLabel101;
        private RichTextBox chatBox101;
        private Button subscribeButton101;
        private RichTextBox messageBox100;
        private Button messageBoxButton100;
        private Button messageBoxButton101;
        private RichTextBox messageBox101;
        private Label warningLabel100;
        private Label warningLabel101;
        private Label brandLabel;
        private PictureBox pictureBox1;
    }
}