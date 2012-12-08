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
            this.btnHome = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnGetRecommends = new System.Windows.Forms.Button();
            this.btnNavRate = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlMenus = new System.Windows.Forms.Panel();
            this.breadcrumbsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLocation = new System.Windows.Forms.Label();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.menuLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMenus.SuspendLayout();
            this.pnlTopBar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(-6, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(215, 88);
            this.picLogo.TabIndex = 4;
            this.picLogo.TabStop = false;
            // 
            // menuLayout
            // 
            this.menuLayout.Controls.Add(this.btnHome);
            this.menuLayout.Controls.Add(this.btnProfile);
            this.menuLayout.Controls.Add(this.btnGetRecommends);
            this.menuLayout.Controls.Add(this.btnNavRate);
            this.menuLayout.Controls.Add(this.btnLogout);
            this.menuLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.menuLayout.Location = new System.Drawing.Point(3, 159);
            this.menuLayout.Name = "menuLayout";
            this.menuLayout.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.menuLayout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuLayout.Size = new System.Drawing.Size(200, 349);
            this.menuLayout.TabIndex = 1;
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHome.Location = new System.Drawing.Point(23, 3);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(141, 23);
            this.btnHome.TabIndex = 1;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnProfile.Location = new System.Drawing.Point(23, 32);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(141, 23);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = false;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnGetRecommends
            // 
            this.btnGetRecommends.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGetRecommends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetRecommends.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGetRecommends.Location = new System.Drawing.Point(23, 61);
            this.btnGetRecommends.Name = "btnGetRecommends";
            this.btnGetRecommends.Size = new System.Drawing.Size(141, 23);
            this.btnGetRecommends.TabIndex = 3;
            this.btnGetRecommends.Text = "Recommend Movies";
            this.btnGetRecommends.UseVisualStyleBackColor = false;
            this.btnGetRecommends.Click += new System.EventHandler(this.btnGetRecommends_Click);
            // 
            // btnNavRate
            // 
            this.btnNavRate.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNavRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavRate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnNavRate.Location = new System.Drawing.Point(23, 90);
            this.btnNavRate.Name = "btnNavRate";
            this.btnNavRate.Size = new System.Drawing.Size(141, 23);
            this.btnNavRate.TabIndex = 4;
            this.btnNavRate.Text = "Rate Movies";
            this.btnNavRate.UseVisualStyleBackColor = false;
            this.btnNavRate.Click += new System.EventHandler(this.btnNavRate_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogout.Location = new System.Drawing.Point(23, 119);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(141, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Location = new System.Drawing.Point(3, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 75);
            this.panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.AutoEllipsis = true;
            this.btnSearch.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnSearch.BackgroundImage = global::MovieOrganizer.Properties.Resources._06_magnify;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.LightSlateGray;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(128, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(66, 20);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(3, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(119, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Text = "Search";
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // pnlMenus
            // 
            this.pnlMenus.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlMenus.Controls.Add(this.panel1);
            this.pnlMenus.Controls.Add(this.menuLayout);
            this.pnlMenus.Controls.Add(this.picLogo);
            this.pnlMenus.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenus.Location = new System.Drawing.Point(0, 0);
            this.pnlMenus.Name = "pnlMenus";
            this.pnlMenus.Size = new System.Drawing.Size(209, 648);
            this.pnlMenus.TabIndex = 8;
            // 
            // breadcrumbsLayout
            // 
            this.breadcrumbsLayout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.breadcrumbsLayout.Location = new System.Drawing.Point(0, 43);
            this.breadcrumbsLayout.Name = "breadcrumbsLayout";
            this.breadcrumbsLayout.Size = new System.Drawing.Size(891, 38);
            this.breadcrumbsLayout.TabIndex = 5;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoEllipsis = true;
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblLocation.Location = new System.Drawing.Point(269, 9);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(78, 26);
            this.lblLocation.TabIndex = 6;
            this.lblLocation.Text = "HOME";
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlTopBar.Controls.Add(this.lblLocation);
            this.pnlTopBar.Controls.Add(this.breadcrumbsLayout);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.Location = new System.Drawing.Point(209, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(891, 81);
            this.pnlTopBar.TabIndex = 9;
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlContent.Controls.Add(this.button6);
            this.pnlContent.Controls.Add(this.button5);
            this.pnlContent.Controls.Add(this.button4);
            this.pnlContent.Controls.Add(this.button3);
            this.pnlContent.Controls.Add(this.button2);
            this.pnlContent.Controls.Add(this.button1);
            this.pnlContent.Location = new System.Drawing.Point(207, 78);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(893, 570);
            this.pnlContent.TabIndex = 6;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(358, 283);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(276, 100);
            this.button6.TabIndex = 9;
            this.button6.Text = "TEST: SHOW MOVIE PAGE";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(73, 283);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(276, 100);
            this.button5.TabIndex = 8;
            this.button5.Text = "TEST: FIND A MOVIE LIKE AMAZON";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(358, 174);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(276, 100);
            this.button4.TabIndex = 7;
            this.button4.Text = "TEST: Retrieve all genres";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(358, 65);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(276, 100);
            this.button3.TabIndex = 6;
            this.button3.Text = "Test: Add diff Breadcrumbs";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(73, 174);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(276, 100);
            this.button2.TabIndex = 5;
            this.button2.Text = "Test: remove Breadcrumbs";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(73, 65);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 100);
            this.button1.TabIndex = 4;
            this.button1.Text = "Test: Add Breadcrumbs";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1100, 648);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlMenus);
            this.Controls.Add(this.pnlContent);
            this.Name = "HomeForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.menuLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMenus.ResumeLayout(false);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.FlowLayoutPanel menuLayout;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnGetRecommends;
        private System.Windows.Forms.Button btnNavRate;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlMenus;
        private System.Windows.Forms.FlowLayoutPanel breadcrumbsLayout;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;


    }
}