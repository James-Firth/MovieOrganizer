namespace MovieOrganizer
{
    partial class HomeForm
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
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.menuLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.breadcrumbsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 86);
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // menuLayout
            // 
            this.menuLayout.Location = new System.Drawing.Point(0, 82);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.Size = new System.Drawing.Size(200, 431);
            this.menuLayout.TabIndex = 1;
            // 
            // breadcrumbsLayout
            // 
            this.breadcrumbsLayout.Location = new System.Drawing.Point(198, 41);
            this.breadcrumbsLayout.Name = "breadcrumbsLayout";
            this.breadcrumbsLayout.Size = new System.Drawing.Size(771, 44);
            this.breadcrumbsLayout.TabIndex = 2;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(472, 9);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(35, 13);
            this.lblLocation.TabIndex = 3;
            this.lblLocation.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(401, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 100);
            this.button1.TabIndex = 4;
            this.button1.Text = "Test: Add Breadcrumbs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 511);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.breadcrumbsLayout);
            this.Controls.Add(this.menuLayout);
            this.Controls.Add(this.picLogo);
            this.Name = "HomeForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.FlowLayoutPanel menuLayout;
        private System.Windows.Forms.FlowLayoutPanel breadcrumbsLayout;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Button button1;

    }
}