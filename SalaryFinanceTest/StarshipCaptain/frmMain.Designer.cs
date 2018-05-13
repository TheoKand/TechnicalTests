namespace StarshipCaptain
{
    partial class frmMain
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnExplore = new System.Windows.Forms.Button();
            this.btnGenerateCoordinates = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Courier New", 7.841584F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.txtLog.Location = new System.Drawing.Point(12, 96);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(952, 396);
            this.txtLog.TabIndex = 1;
            this.txtLog.WordWrap = false;
            // 
            // btnExplore
            // 
            this.btnExplore.Location = new System.Drawing.Point(296, 12);
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(261, 69);
            this.btnExplore.TabIndex = 2;
            this.btnExplore.Text = "Explore Universe";
            this.btnExplore.UseVisualStyleBackColor = true;
            this.btnExplore.Click += new System.EventHandler(this.btnExplore_Click);
            // 
            // btnGenerateCoordinates
            // 
            this.btnGenerateCoordinates.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnGenerateCoordinates.Location = new System.Drawing.Point(12, 12);
            this.btnGenerateCoordinates.Name = "btnGenerateCoordinates";
            this.btnGenerateCoordinates.Size = new System.Drawing.Size(269, 69);
            this.btnGenerateCoordinates.TabIndex = 0;
            this.btnGenerateCoordinates.Text = "Generate Coordinates";
            this.btnGenerateCoordinates.UseVisualStyleBackColor = true;
            this.btnGenerateCoordinates.Click += new System.EventHandler(this.btnGenerateCoordinates_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 504);
            this.Controls.Add(this.btnExplore);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnGenerateCoordinates);
            this.MinimumSize = new System.Drawing.Size(983, 532);
            this.Name = "frmMain";
            this.Text = "Starship Captain Challenge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnExplore;
        private System.Windows.Forms.Button btnGenerateCoordinates;
    }
}

