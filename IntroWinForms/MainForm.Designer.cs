namespace IntroWinForms
{
    partial class MainForm
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.pbcSource = new System.Windows.Forms.PictureBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.pbxDest = new System.Windows.Forms.PictureBox();
            this.lstConverts = new System.Windows.Forms.ListBox();
            this.pnlControls = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbcSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDest)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(29, 28);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = ",Открыть";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pbcSource
            // 
            this.pbcSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbcSource.Location = new System.Drawing.Point(147, 28);
            this.pbcSource.Name = "pbcSource";
            this.pbcSource.Size = new System.Drawing.Size(397, 447);
            this.pbcSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbcSource.TabIndex = 2;
            this.pbcSource.TabStop = false;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(29, 57);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(100, 23);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Преобразовать";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // pbxDest
            // 
            this.pbxDest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxDest.Location = new System.Drawing.Point(550, 28);
            this.pbxDest.Name = "pbxDest";
            this.pbxDest.Size = new System.Drawing.Size(421, 447);
            this.pbxDest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxDest.TabIndex = 4;
            this.pbxDest.TabStop = false;
            // 
            // lstConverts
            // 
            this.lstConverts.FormattingEnabled = true;
            this.lstConverts.Location = new System.Drawing.Point(26, 94);
            this.lstConverts.Name = "lstConverts";
            this.lstConverts.Size = new System.Drawing.Size(103, 160);
            this.lstConverts.TabIndex = 5;
            this.lstConverts.SelectedIndexChanged += new System.EventHandler(this.lstConverts_SelectedIndexChanged);
            // 
            // pnlControls
            // 
            this.pnlControls.Location = new System.Drawing.Point(26, 275);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(102, 200);
            this.pnlControls.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 488);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.lstConverts);
            this.Controls.Add(this.pbxDest);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.pbcSource);
            this.Controls.Add(this.btnOpen);
            this.Name = "MainForm";
            this.Text = "Image Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pbcSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.PictureBox pbcSource;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.PictureBox pbxDest;
        private System.Windows.Forms.ListBox lstConverts;
        private System.Windows.Forms.Panel pnlControls;
    }
}

