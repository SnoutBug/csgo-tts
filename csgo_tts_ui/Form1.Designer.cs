namespace csgo_tts_ui
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.checkName = new System.Windows.Forms.CheckBox();
            this.checkSpot = new System.Windows.Forms.CheckBox();
            this.checkFiller = new System.Windows.Forms.CheckBox();
            this.checkCombine = new System.Windows.Forms.CheckBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.textPath = new System.Windows.Forms.TextBox();
            this.labelSettings1 = new System.Windows.Forms.Label();
            this.labelSettings2 = new System.Windows.Forms.Label();
            this.labelSettings3 = new System.Windows.Forms.Label();
            this.dropGender = new System.Windows.Forms.ComboBox();
            this.labelGender = new System.Windows.Forms.Label();
            this.dropRegion = new System.Windows.Forms.ComboBox();
            this.labelRegion = new System.Windows.Forms.Label();
            this.dropPlayerName = new System.Windows.Forms.ComboBox();
            this.labelPlayerName = new System.Windows.Forms.Label();
            this.dropAlias = new System.Windows.Forms.ComboBox();
            this.labelAlias = new System.Windows.Forms.Label();
            this.checkMute = new System.Windows.Forms.CheckBox();
            this.checkAlias = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon2 = new System.Windows.Forms.NotifyIcon(this.components);
            this.labelSize = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(317, 300);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(220, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stay hydrated!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // checkName
            // 
            this.checkName.AutoSize = true;
            this.checkName.Location = new System.Drawing.Point(12, 42);
            this.checkName.Name = "checkName";
            this.checkName.Size = new System.Drawing.Size(88, 17);
            this.checkName.TabIndex = 1;
            this.checkName.Text = "Read Names";
            this.checkName.UseVisualStyleBackColor = true;
            this.checkName.CheckedChanged += new System.EventHandler(this.CheckName_CheckedChanged);
            // 
            // checkSpot
            // 
            this.checkSpot.AutoSize = true;
            this.checkSpot.Location = new System.Drawing.Point(12, 65);
            this.checkSpot.Name = "checkSpot";
            this.checkSpot.Size = new System.Drawing.Size(82, 17);
            this.checkSpot.TabIndex = 2;
            this.checkSpot.Text = "Read Spots";
            this.checkSpot.UseVisualStyleBackColor = true;
            this.checkSpot.CheckedChanged += new System.EventHandler(this.CheckSpot_CheckedChanged);
            // 
            // checkFiller
            // 
            this.checkFiller.AutoSize = true;
            this.checkFiller.Location = new System.Drawing.Point(12, 88);
            this.checkFiller.Name = "checkFiller";
            this.checkFiller.Size = new System.Drawing.Size(69, 17);
            this.checkFiller.TabIndex = 3;
            this.checkFiller.Text = "Use Filler";
            this.checkFiller.UseVisualStyleBackColor = true;
            this.checkFiller.CheckedChanged += new System.EventHandler(this.CheckFiller_CheckedChanged);
            // 
            // checkCombine
            // 
            this.checkCombine.AutoSize = true;
            this.checkCombine.Location = new System.Drawing.Point(12, 111);
            this.checkCombine.Name = "checkCombine";
            this.checkCombine.Size = new System.Drawing.Size(118, 17);
            this.checkCombine.TabIndex = 4;
            this.checkCombine.Text = "Combine Messages";
            this.checkCombine.UseVisualStyleBackColor = true;
            this.checkCombine.CheckedChanged += new System.EventHandler(this.CheckCombine_CheckedChanged);
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(9, 171);
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(37, 20);
            this.numTimeout.TabIndex = 5;
            this.numTimeout.ValueChanged += new System.EventHandler(this.NumTimeout_ValueChanged);
            // 
            // labelTimeout
            // 
            this.labelTimeout.AutoSize = true;
            this.labelTimeout.Location = new System.Drawing.Point(52, 175);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.Size = new System.Drawing.Size(97, 13);
            this.labelTimeout.TabIndex = 6;
            this.labelTimeout.Text = "Spam timeout (sec)";
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(3, 260);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(97, 13);
            this.labelPath.TabIndex = 7;
            this.labelPath.Text = "CS:GO Folder Path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(541, 255);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(541, 295);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 9;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // textPath
            // 
            this.textPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textPath.Location = new System.Drawing.Point(106, 257);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(429, 20);
            this.textPath.TabIndex = 10;
            this.textPath.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Counter-Strike Global Offensive";
            this.textPath.TextChanged += new System.EventHandler(this.TextPath_TextChanged);
            this.textPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // labelSettings1
            // 
            this.labelSettings1.AutoSize = true;
            this.labelSettings1.Location = new System.Drawing.Point(9, 9);
            this.labelSettings1.Name = "labelSettings1";
            this.labelSettings1.Size = new System.Drawing.Size(140, 13);
            this.labelSettings1.TabIndex = 11;
            this.labelSettings1.Text = "Message formatting Settings";
            // 
            // labelSettings2
            // 
            this.labelSettings2.AutoSize = true;
            this.labelSettings2.Location = new System.Drawing.Point(217, 9);
            this.labelSettings2.Name = "labelSettings2";
            this.labelSettings2.Size = new System.Drawing.Size(121, 13);
            this.labelSettings2.TabIndex = 11;
            this.labelSettings2.Text = "Text-to-Speech Settings";
            // 
            // labelSettings3
            // 
            this.labelSettings3.AutoSize = true;
            this.labelSettings3.Location = new System.Drawing.Point(9, 144);
            this.labelSettings3.Name = "labelSettings3";
            this.labelSettings3.Size = new System.Drawing.Size(84, 13);
            this.labelSettings3.TabIndex = 11;
            this.labelSettings3.Text = "Comfort Settings";
            // 
            // dropGender
            // 
            this.dropGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropGender.FormattingEnabled = true;
            this.dropGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.dropGender.Location = new System.Drawing.Point(220, 39);
            this.dropGender.Name = "dropGender";
            this.dropGender.Size = new System.Drawing.Size(57, 21);
            this.dropGender.TabIndex = 12;
            this.dropGender.SelectedIndexChanged += new System.EventHandler(this.DropGender_SelectedIndexChanged);
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Location = new System.Drawing.Point(283, 43);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(42, 13);
            this.labelGender.TabIndex = 6;
            this.labelGender.Text = "Gender";
            // 
            // dropRegion
            // 
            this.dropRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropRegion.FormattingEnabled = true;
            this.dropRegion.Location = new System.Drawing.Point(220, 84);
            this.dropRegion.Name = "dropRegion";
            this.dropRegion.Size = new System.Drawing.Size(57, 21);
            this.dropRegion.TabIndex = 12;
            this.dropRegion.SelectedIndexChanged += new System.EventHandler(this.DropRegion_SelectedIndexChanged);
            // 
            // labelRegion
            // 
            this.labelRegion.AutoSize = true;
            this.labelRegion.Location = new System.Drawing.Point(283, 88);
            this.labelRegion.Name = "labelRegion";
            this.labelRegion.Size = new System.Drawing.Size(41, 13);
            this.labelRegion.TabIndex = 6;
            this.labelRegion.Text = "Region";
            // 
            // dropPlayerName
            // 
            this.dropPlayerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropPlayerName.FormattingEnabled = true;
            this.dropPlayerName.Location = new System.Drawing.Point(209, 171);
            this.dropPlayerName.Name = "dropPlayerName";
            this.dropPlayerName.Size = new System.Drawing.Size(140, 21);
            this.dropPlayerName.TabIndex = 13;
            this.dropPlayerName.SelectedIndexChanged += new System.EventHandler(this.DropPlayerName_SelectedIndexChanged);
            // 
            // labelPlayerName
            // 
            this.labelPlayerName.AutoSize = true;
            this.labelPlayerName.Location = new System.Drawing.Point(206, 153);
            this.labelPlayerName.Name = "labelPlayerName";
            this.labelPlayerName.Size = new System.Drawing.Size(67, 13);
            this.labelPlayerName.TabIndex = 14;
            this.labelPlayerName.Text = "Player Name";
            // 
            // dropAlias
            // 
            this.dropAlias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropAlias.FormattingEnabled = true;
            this.dropAlias.Items.AddRange(new object[] {
            "None",
            "Blue",
            "Green",
            "Orange",
            "Purple",
            "Yellow"});
            this.dropAlias.Location = new System.Drawing.Point(369, 171);
            this.dropAlias.Name = "dropAlias";
            this.dropAlias.Size = new System.Drawing.Size(80, 21);
            this.dropAlias.TabIndex = 13;
            this.dropAlias.SelectedIndexChanged += new System.EventHandler(this.DropAlias_SelectedIndexChanged);
            // 
            // labelAlias
            // 
            this.labelAlias.AutoSize = true;
            this.labelAlias.Location = new System.Drawing.Point(366, 153);
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(29, 13);
            this.labelAlias.TabIndex = 14;
            this.labelAlias.Text = "Alias";
            // 
            // checkMute
            // 
            this.checkMute.AutoSize = true;
            this.checkMute.Location = new System.Drawing.Point(455, 174);
            this.checkMute.Name = "checkMute";
            this.checkMute.Size = new System.Drawing.Size(50, 17);
            this.checkMute.TabIndex = 16;
            this.checkMute.Text = "Mute";
            this.checkMute.UseVisualStyleBackColor = true;
            this.checkMute.CheckedChanged += new System.EventHandler(this.CheckMute_CheckedChanged);
            // 
            // checkAlias
            // 
            this.checkAlias.AutoSize = true;
            this.checkAlias.Location = new System.Drawing.Point(209, 199);
            this.checkAlias.Name = "checkAlias";
            this.checkAlias.Size = new System.Drawing.Size(70, 17);
            this.checkAlias.TabIndex = 17;
            this.checkAlias.Text = "Use Alias";
            this.checkAlias.UseVisualStyleBackColor = true;
            this.checkAlias.CheckedChanged += new System.EventHandler(this.CheckAlias_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // notifyIcon2
            // 
            this.notifyIcon2.Text = "notifyIcon2";
            this.notifyIcon2.Visible = true;
            // 
            // labelSize
            // 
            this.labelSize.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelSize.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelSize.Location = new System.Drawing.Point(427, 13);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(134, 13);
            this.labelSize.TabIndex = 18;
            this.labelSize.Text = "FILESIZE";
            this.labelSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(565, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(51, 21);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(596, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(18, 19);
            this.button1.TabIndex = 20;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 335);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.checkAlias);
            this.Controls.Add(this.checkMute);
            this.Controls.Add(this.labelAlias);
            this.Controls.Add(this.labelPlayerName);
            this.Controls.Add(this.dropAlias);
            this.Controls.Add(this.dropPlayerName);
            this.Controls.Add(this.dropRegion);
            this.Controls.Add(this.dropGender);
            this.Controls.Add(this.labelSettings3);
            this.Controls.Add(this.labelSettings2);
            this.Controls.Add(this.labelSettings1);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.labelRegion);
            this.Controls.Add(this.labelGender);
            this.Controls.Add(this.labelTimeout);
            this.Controls.Add(this.numTimeout);
            this.Controls.Add(this.checkCombine);
            this.Controls.Add(this.checkFiller);
            this.Controls.Add(this.checkSpot);
            this.Controls.Add(this.checkName);
            this.Controls.Add(this.label1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(642, 374);
            this.MinimumSize = new System.Drawing.Size(642, 374);
            this.Name = "Form1";
            this.Text = "Counter-Strike: Global Offensive Text-to-Speech";
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkName;
        private System.Windows.Forms.CheckBox checkSpot;
        private System.Windows.Forms.CheckBox checkFiller;
        private System.Windows.Forms.CheckBox checkCombine;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Label labelSettings1;
        private System.Windows.Forms.Label labelSettings2;
        private System.Windows.Forms.Label labelSettings3;
        private System.Windows.Forms.ComboBox dropGender;
        private System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.ComboBox dropRegion;
        private System.Windows.Forms.Label labelRegion;
        private System.Windows.Forms.ComboBox dropPlayerName;
        private System.Windows.Forms.Label labelPlayerName;
        private System.Windows.Forms.ComboBox dropAlias;
        private System.Windows.Forms.Label labelAlias;
        private System.Windows.Forms.CheckBox checkMute;
        private System.Windows.Forms.CheckBox checkAlias;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.NotifyIcon notifyIcon2;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button button1;
    }
}

