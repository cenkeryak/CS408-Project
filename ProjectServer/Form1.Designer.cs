namespace ProjectServer
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
            richTextBoxEventLog = new RichTextBox();
            richTextBoxSps = new RichTextBox();
            richTextBoxIf = new RichTextBox();
            richTextBoxConnected = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            textBoxPort = new TextBox();
            buttonListen = new Button();
            SuspendLayout();
            // 
            // richTextBoxEventLog
            // 
            richTextBoxEventLog.Location = new Point(308, 48);
            richTextBoxEventLog.Name = "richTextBoxEventLog";
            richTextBoxEventLog.Size = new Size(543, 274);
            richTextBoxEventLog.TabIndex = 0;
            richTextBoxEventLog.Text = "";
            // 
            // richTextBoxSps
            // 
            richTextBoxSps.Location = new Point(606, 387);
            richTextBoxSps.Name = "richTextBoxSps";
            richTextBoxSps.Size = new Size(260, 395);
            richTextBoxSps.TabIndex = 3;
            richTextBoxSps.Text = "";
            // 
            // richTextBoxIf
            // 
            richTextBoxIf.Location = new Point(308, 387);
            richTextBoxIf.Name = "richTextBoxIf";
            richTextBoxIf.Size = new Size(260, 395);
            richTextBoxIf.TabIndex = 4;
            richTextBoxIf.Text = "";
            // 
            // richTextBoxConnected
            // 
            richTextBoxConnected.Location = new Point(12, 387);
            richTextBoxConnected.Name = "richTextBoxConnected";
            richTextBoxConnected.Size = new Size(260, 395);
            richTextBoxConnected.TabIndex = 5;
            richTextBoxConnected.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(22, 346);
            label1.Name = "label1";
            label1.Size = new Size(241, 38);
            label1.TabIndex = 6;
            label1.Text = "Connected Clients";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(394, 346);
            label2.Name = "label2";
            label2.Size = new Size(91, 38);
            label2.TabIndex = 7;
            label2.Text = "IF 100";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(686, 346);
            label3.Name = "label3";
            label3.Size = new Size(116, 38);
            label3.TabIndex = 8;
            label3.Text = "SPS 101";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(516, 7);
            label4.Name = "label4";
            label4.Size = new Size(149, 38);
            label4.TabIndex = 9;
            label4.Text = "Event Logs";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(22, 93);
            label5.Name = "label5";
            label5.Size = new Size(114, 25);
            label5.TabIndex = 10;
            label5.Text = "Port Number";
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(22, 136);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(142, 31);
            textBoxPort.TabIndex = 11;
            // 
            // buttonListen
            // 
            buttonListen.BackColor = SystemColors.ButtonHighlight;
            buttonListen.Enabled = false;
            buttonListen.Location = new Point(22, 216);
            buttonListen.Name = "buttonListen";
            buttonListen.Size = new Size(250, 34);
            buttonListen.TabIndex = 15;
            buttonListen.Text = "Listen";
            buttonListen.UseVisualStyleBackColor = false;
            buttonListen.Click += buttonListen_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 794);
            Controls.Add(buttonListen);
            Controls.Add(textBoxPort);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBoxConnected);
            Controls.Add(richTextBoxIf);
            Controls.Add(richTextBoxSps);
            Controls.Add(richTextBoxEventLog);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxEventLog;
        private RichTextBox richTextBoxSps;
        private RichTextBox richTextBoxIf;
        private RichTextBox richTextBoxConnected;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBoxPort;
        private Button buttonListen;
    }
}