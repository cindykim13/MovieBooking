namespace MovieBookingClient.UI.UserControls
{
    partial class UCMovieList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelTop = new Guna.UI2.WinForms.Guna2Panel();
            btnSearch = new Guna.UI2.WinForms.Guna2Button();
            txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            cboGenre = new Guna.UI2.WinForms.Guna2ComboBox();
            flowLayoutPanelMovies = new FlowLayoutPanel();
            panelBottom = new Guna.UI2.WinForms.Guna2Panel();
            btnNext = new Guna.UI2.WinForms.Guna2Button();
            btnPrevious = new Guna.UI2.WinForms.Guna2Button();
            lblPageInfo = new Label();
            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(txtSearch);
            panelTop.Controls.Add(cboGenre);
            panelTop.CustomizableEdges = customizableEdges7;
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelTop.Size = new Size(1200, 60);
            panelTop.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BorderRadius = 6;
            btnSearch.CustomizableEdges = customizableEdges1;
            btnSearch.FillColor = Color.OrangeRed;
            btnSearch.Font = new Font("Segoe UI", 9F);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(450, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSearch.Size = new Size(100, 36);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            // 
            // txtSearch
            // 
            txtSearch.BorderRadius = 6;
            txtSearch.CustomizableEdges = customizableEdges3;
            txtSearch.DefaultText = "";
            txtSearch.Font = new Font("Segoe UI", 9F);
            txtSearch.Location = new Point(220, 12);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm tên phim...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtSearch.Size = new Size(220, 36);
            txtSearch.TabIndex = 1;
            // 
            // cboGenre
            // 
            cboGenre.BackColor = Color.Transparent;
            cboGenre.BorderRadius = 6;
            cboGenre.CustomizableEdges = customizableEdges5;
            cboGenre.DrawMode = DrawMode.OwnerDrawFixed;
            cboGenre.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGenre.FocusedColor = Color.Empty;
            cboGenre.Font = new Font("Segoe UI", 10F);
            cboGenre.ForeColor = Color.FromArgb(68, 88, 112);
            cboGenre.ItemHeight = 30;
            cboGenre.Location = new Point(20, 12);
            cboGenre.Name = "cboGenre";
            cboGenre.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cboGenre.Size = new Size(180, 36);
            cboGenre.TabIndex = 0;
            // 
            // flowLayoutPanelMovies
            // 
            flowLayoutPanelMovies.AutoScroll = true;
            flowLayoutPanelMovies.BackColor = SystemColors.Info;
            flowLayoutPanelMovies.Dock = DockStyle.Fill;
            flowLayoutPanelMovies.Location = new Point(0, 60);
            flowLayoutPanelMovies.Name = "flowLayoutPanelMovies";
            flowLayoutPanelMovies.Padding = new System.Windows.Forms.Padding(70, 20, 50, 20);
            flowLayoutPanelMovies.Size = new System.Drawing.Size(1280, 505);
            flowLayoutPanelMovies.TabIndex = 1;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnNext);
            panelBottom.Controls.Add(btnPrevious);
            panelBottom.Controls.Add(lblPageInfo);
            panelBottom.CustomizableEdges = customizableEdges13;
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 565);
            panelBottom.Name = "panelBottom";
            panelBottom.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelBottom.Size = new Size(1200, 60);
            panelBottom.TabIndex = 2;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Top;
            btnNext.CustomizableEdges = customizableEdges9;
            btnNext.FillColor = Color.OrangeRed;
            btnNext.Font = new Font("Segoe UI", 9F);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(630, 12);
            btnNext.Name = "btnNext";
            btnNext.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnNext.Size = new Size(100, 36);
            btnNext.TabIndex = 2;
            btnNext.Text = "Trang sau >";
            // 
            // btnPrevious
            // 
            btnPrevious.Anchor = AnchorStyles.Top;
            btnPrevious.CustomizableEdges = customizableEdges11;
            btnPrevious.FillColor = Color.OrangeRed;
            btnPrevious.Font = new Font("Segoe UI", 9F);
            btnPrevious.ForeColor = Color.White;
            btnPrevious.Location = new Point(470, 12);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnPrevious.Size = new Size(100, 36);
            btnPrevious.TabIndex = 1;
            btnPrevious.Text = "< Trang trước";
            // 
            // lblPageInfo
            // 
            lblPageInfo.Anchor = AnchorStyles.Top;
            lblPageInfo.Location = new Point(570, 12);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(60, 36);
            lblPageInfo.TabIndex = 0;
            lblPageInfo.Text = "1 / 10";
            lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UCMovieList
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(flowLayoutPanelMovies);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Name = "UCMovieList";
            Size = new Size(1200, 625);
            panelTop.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2ComboBox cboGenre;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMovies;
        private Guna.UI2.WinForms.Guna2Panel panelBottom;
        private Guna.UI2.WinForms.Guna2Button btnNext;
        private Guna.UI2.WinForms.Guna2Button btnPrevious;
        private System.Windows.Forms.Label lblPageInfo;
    }
}
