namespace csgo_tts_ui
{
    partial class csgo_tts_settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(csgo_tts_settings));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContainerName = new System.Windows.Forms.Button();
            this.ContainerFiller = new System.Windows.Forms.Button();
            this.ContainerSpot = new System.Windows.Forms.Button();
            this.ContainerMessage = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelInfoDragDrop = new System.Windows.Forms.Label();
            this.btnPreviewTTS = new System.Windows.Forms.Button();
            this.btnSaveMessageOrder = new System.Windows.Forms.Button();
            this.egg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ContainerName
            // 
            this.ContainerName.Location = new System.Drawing.Point(24, 52);
            this.ContainerName.Name = "ContainerName";
            this.ContainerName.Size = new System.Drawing.Size(80, 23);
            this.ContainerName.TabIndex = 1;
            this.ContainerName.Text = "Player Name";
            this.ContainerName.UseVisualStyleBackColor = true;
            this.ContainerName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Container_MouseDown);
            this.ContainerName.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Container_MouseMove);
            this.ContainerName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Container_MouseUp);
            // 
            // ContainerFiller
            // 
            this.ContainerFiller.Location = new System.Drawing.Point(128, 52);
            this.ContainerFiller.Name = "ContainerFiller";
            this.ContainerFiller.Size = new System.Drawing.Size(80, 23);
            this.ContainerFiller.TabIndex = 1;
            this.ContainerFiller.Text = "Filler: Wrote";
            this.ContainerFiller.UseVisualStyleBackColor = true;
            this.ContainerFiller.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Container_MouseDown);
            this.ContainerFiller.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Container_MouseMove);
            this.ContainerFiller.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Container_MouseUp);
            // 
            // ContainerSpot
            // 
            this.ContainerSpot.Location = new System.Drawing.Point(227, 52);
            this.ContainerSpot.Name = "ContainerSpot";
            this.ContainerSpot.Size = new System.Drawing.Size(80, 23);
            this.ContainerSpot.TabIndex = 1;
            this.ContainerSpot.Text = "Location";
            this.ContainerSpot.UseVisualStyleBackColor = true;
            this.ContainerSpot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Container_MouseDown);
            this.ContainerSpot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Container_MouseMove);
            this.ContainerSpot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Container_MouseUp);
            // 
            // ContainerMessage
            // 
            this.ContainerMessage.Location = new System.Drawing.Point(332, 52);
            this.ContainerMessage.Name = "ContainerMessage";
            this.ContainerMessage.Size = new System.Drawing.Size(80, 23);
            this.ContainerMessage.TabIndex = 1;
            this.ContainerMessage.Text = "Message";
            this.ContainerMessage.UseVisualStyleBackColor = true;
            this.ContainerMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Container_MouseDown);
            this.ContainerMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Container_MouseMove);
            this.ContainerMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Container_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.pictureBox1.Location = new System.Drawing.Point(19, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(404, 27);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // labelInfoDragDrop
            // 
            this.labelInfoDragDrop.AutoSize = true;
            this.labelInfoDragDrop.Location = new System.Drawing.Point(24, 25);
            this.labelInfoDragDrop.Name = "labelInfoDragDrop";
            this.labelInfoDragDrop.Size = new System.Drawing.Size(304, 13);
            this.labelInfoDragDrop.TabIndex = 4;
            this.labelInfoDragDrop.Text = "Drag the items in the order you want your messages to be read.";
            // 
            // btnPreviewTTS
            // 
            this.btnPreviewTTS.Location = new System.Drawing.Point(232, 116);
            this.btnPreviewTTS.Name = "btnPreviewTTS";
            this.btnPreviewTTS.Size = new System.Drawing.Size(75, 23);
            this.btnPreviewTTS.TabIndex = 5;
            this.btnPreviewTTS.Text = "▶ Preview";
            this.btnPreviewTTS.UseVisualStyleBackColor = true;
            this.btnPreviewTTS.Click += new System.EventHandler(this.BtnPreviewTTS_Click);
            // 
            // btnSaveMessageOrder
            // 
            this.btnSaveMessageOrder.Location = new System.Drawing.Point(337, 116);
            this.btnSaveMessageOrder.Name = "btnSaveMessageOrder";
            this.btnSaveMessageOrder.Size = new System.Drawing.Size(75, 23);
            this.btnSaveMessageOrder.TabIndex = 6;
            this.btnSaveMessageOrder.Text = "Save";
            this.btnSaveMessageOrder.UseVisualStyleBackColor = true;
            this.btnSaveMessageOrder.Click += new System.EventHandler(this.BtnSaveMessageOrder_Click);
            // 
            // egg
            // 
            this.egg.AutoSize = true;
            this.egg.Location = new System.Drawing.Point(800, 600);
            this.egg.Name = "egg";
            this.egg.Size = new System.Drawing.Size(476, 13);
            this.egg.TabIndex = 7;
            this.egg.Text = "Hi, glad you found me. I hope you like this script. If you do, you can press me t" +
    "o donate something :)";
            this.egg.Click += new System.EventHandler(this.Egg_Click);
            // 
            // csgo_tts_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 153);
            this.Controls.Add(this.egg);
            this.Controls.Add(this.btnSaveMessageOrder);
            this.Controls.Add(this.btnPreviewTTS);
            this.Controls.Add(this.labelInfoDragDrop);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ContainerMessage);
            this.Controls.Add(this.ContainerSpot);
            this.Controls.Add(this.ContainerFiller);
            this.Controls.Add(this.ContainerName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "csgo_tts_settings";
            this.Text = "Custom Message Order";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button ContainerName;
        private System.Windows.Forms.Button ContainerFiller;
        private System.Windows.Forms.Button ContainerSpot;
        private System.Windows.Forms.Button ContainerMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelInfoDragDrop;
        private System.Windows.Forms.Button btnPreviewTTS;
        private System.Windows.Forms.Button btnSaveMessageOrder;
        private System.Windows.Forms.Label egg;
    }
}