namespace WiiBalanceWalker
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_rwWT = new System.Windows.Forms.Label();
            this.button_Connect = new System.Windows.Forms.Button();
            this.groupBox_RawWeight = new System.Windows.Forms.GroupBox();
            this.label_rwBR = new System.Windows.Forms.Label();
            this.label_rwBL = new System.Windows.Forms.Label();
            this.label_rwTR = new System.Windows.Forms.Label();
            this.label_rwTL = new System.Windows.Forms.Label();
            this.groupBox_OffsetWeight = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_General = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_BluetoothAddDevice = new System.Windows.Forms.Button();
            this.label_Status = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.copScatter1 = new WiiBalanceWalker.COPScatter();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox_RawWeight.SuspendLayout();
            this.groupBox_OffsetWeight.SuspendLayout();
            this.groupBox_General.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_rwWT
            // 
            this.label_rwWT.AutoSize = true;
            this.label_rwWT.Location = new System.Drawing.Point(73, 104);
            this.label_rwWT.Name = "label_rwWT";
            this.label_rwWT.Size = new System.Drawing.Size(23, 12);
            this.label_rwWT.TabIndex = 0;
            this.label_rwWT.Text = "WT";
            // 
            // button_Connect
            // 
            this.button_Connect.Location = new System.Drawing.Point(203, 58);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(213, 25);
            this.button_Connect.TabIndex = 0;
            this.button_Connect.Text = "Connect to Wii balance board";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // groupBox_RawWeight
            // 
            this.groupBox_RawWeight.Controls.Add(this.label_rwBR);
            this.groupBox_RawWeight.Controls.Add(this.label_rwBL);
            this.groupBox_RawWeight.Controls.Add(this.label_rwTR);
            this.groupBox_RawWeight.Controls.Add(this.label_rwTL);
            this.groupBox_RawWeight.Controls.Add(this.label_rwWT);
            this.groupBox_RawWeight.Location = new System.Drawing.Point(14, 11);
            this.groupBox_RawWeight.Name = "groupBox_RawWeight";
            this.groupBox_RawWeight.Size = new System.Drawing.Size(175, 128);
            this.groupBox_RawWeight.TabIndex = 3;
            this.groupBox_RawWeight.TabStop = false;
            this.groupBox_RawWeight.Text = "Raw Weight";
            // 
            // label_rwBR
            // 
            this.label_rwBR.AutoSize = true;
            this.label_rwBR.Location = new System.Drawing.Point(118, 70);
            this.label_rwBR.Name = "label_rwBR";
            this.label_rwBR.Size = new System.Drawing.Size(21, 12);
            this.label_rwBR.TabIndex = 0;
            this.label_rwBR.Text = "BR";
            // 
            // label_rwBL
            // 
            this.label_rwBL.AutoSize = true;
            this.label_rwBL.Location = new System.Drawing.Point(29, 70);
            this.label_rwBL.Name = "label_rwBL";
            this.label_rwBL.Size = new System.Drawing.Size(20, 12);
            this.label_rwBL.TabIndex = 0;
            this.label_rwBL.Text = "BL";
            // 
            // label_rwTR
            // 
            this.label_rwTR.AutoSize = true;
            this.label_rwTR.Location = new System.Drawing.Point(118, 30);
            this.label_rwTR.Name = "label_rwTR";
            this.label_rwTR.Size = new System.Drawing.Size(21, 12);
            this.label_rwTR.TabIndex = 0;
            this.label_rwTR.Text = "TR";
            // 
            // label_rwTL
            // 
            this.label_rwTL.AutoSize = true;
            this.label_rwTL.Location = new System.Drawing.Point(29, 30);
            this.label_rwTL.Name = "label_rwTL";
            this.label_rwTL.Size = new System.Drawing.Size(20, 12);
            this.label_rwTL.TabIndex = 0;
            this.label_rwTL.Text = "TL";
            // 
            // groupBox_OffsetWeight
            // 
            this.groupBox_OffsetWeight.Controls.Add(this.button5);
            this.groupBox_OffsetWeight.Controls.Add(this.label2);
            this.groupBox_OffsetWeight.Controls.Add(this.label1);
            this.groupBox_OffsetWeight.Location = new System.Drawing.Point(196, 11);
            this.groupBox_OffsetWeight.Name = "groupBox_OffsetWeight";
            this.groupBox_OffsetWeight.Size = new System.Drawing.Size(98, 128);
            this.groupBox_OffsetWeight.TabIndex = 4;
            this.groupBox_OffsetWeight.TabStop = false;
            this.groupBox_OffsetWeight.Text = "COG Values";
            this.groupBox_OffsetWeight.Enter += new System.EventHandler(this.groupBox_OffsetWeight_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "COGy";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "COGx";
            // 
            // groupBox_General
            // 
            this.groupBox_General.Controls.Add(this.textBox1);
            this.groupBox_General.Controls.Add(this.button4);
            this.groupBox_General.Controls.Add(this.button3);
            this.groupBox_General.Controls.Add(this.button2);
            this.groupBox_General.Controls.Add(this.button1);
            this.groupBox_General.Controls.Add(this.button_BluetoothAddDevice);
            this.groupBox_General.Controls.Add(this.button_Connect);
            this.groupBox_General.Location = new System.Drawing.Point(300, 11);
            this.groupBox_General.Name = "groupBox_General";
            this.groupBox_General.Size = new System.Drawing.Size(435, 128);
            this.groupBox_General.TabIndex = 0;
            this.groupBox_General.TabStop = false;
            this.groupBox_General.Text = "General";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(203, 91);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(213, 25);
            this.button4.TabIndex = 7;
            this.button4.Text = "Print";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 92);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 24);
            this.button3.TabIndex = 6;
            this.button3.Text = "Start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "Pause";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Zero";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_BluetoothAddDevice
            // 
            this.button_BluetoothAddDevice.Location = new System.Drawing.Point(203, 24);
            this.button_BluetoothAddDevice.Name = "button_BluetoothAddDevice";
            this.button_BluetoothAddDevice.Size = new System.Drawing.Size(213, 25);
            this.button_BluetoothAddDevice.TabIndex = 1;
            this.button_BluetoothAddDevice.Text = "Add bluetooth Wii device";
            this.button_BluetoothAddDevice.UseVisualStyleBackColor = true;
            this.button_BluetoothAddDevice.Click += new System.EventHandler(this.button_BluetoothAddDevice_Click);
            // 
            // label_Status
            // 
            this.label_Status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Status.Location = new System.Drawing.Point(12, 142);
            this.label_Status.Name = "label_Status";
            this.label_Status.Size = new System.Drawing.Size(721, 22);
            this.label_Status.TabIndex = 4;
            this.label_Status.Text = "STATUS";
            this.label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 94);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 21);
            this.textBox1.TabIndex = 8;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(11, 167);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(722, 372);
            this.elementHost1.TabIndex = 6;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.copScatter1;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 92);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(67, 24);
            this.button5.TabIndex = 7;
            this.button5.Text = "Trace";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 553);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.label_Status);
            this.Controls.Add(this.groupBox_General);
            this.Controls.Add(this.groupBox_OffsetWeight);
            this.Controls.Add(this.groupBox_RawWeight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wii Balance Walker - Version 0.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox_RawWeight.ResumeLayout(false);
            this.groupBox_RawWeight.PerformLayout();
            this.groupBox_OffsetWeight.ResumeLayout(false);
            this.groupBox_OffsetWeight.PerformLayout();
            this.groupBox_General.ResumeLayout(false);
            this.groupBox_General.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_rwWT;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.GroupBox groupBox_RawWeight;
        private System.Windows.Forms.Label label_rwBR;
        private System.Windows.Forms.Label label_rwBL;
        private System.Windows.Forms.Label label_rwTR;
        private System.Windows.Forms.Label label_rwTL;
        private System.Windows.Forms.GroupBox groupBox_OffsetWeight;
        private System.Windows.Forms.GroupBox groupBox_General;
        private System.Windows.Forms.Label label_Status;
        private System.Windows.Forms.Button button_BluetoothAddDevice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private COPScatter copScatter1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
    }
}

