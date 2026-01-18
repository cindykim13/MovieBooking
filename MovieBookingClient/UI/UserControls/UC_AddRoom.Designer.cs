namespace MovieBookingClient.UI.UserControls.Admin
{
    partial class UC_AddRoom
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            lblName = new Label();
            txtRoomName = new Guna.UI2.WinForms.Guna2TextBox();
            lblCinema = new Label();
            cboCinema = new Guna.UI2.WinForms.Guna2ComboBox();
            lblTemplate = new Label();
            cboTemplate = new Guna.UI2.WinForms.Guna2ComboBox();
            pnlSeatPreview = new Panel();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.BorderColor = Color.Silver;
            pnlMain.BorderRadius = 10;
            pnlMain.BorderThickness = 1;
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Controls.Add(lblName);
            pnlMain.Controls.Add(txtRoomName);
            pnlMain.Controls.Add(lblCinema);
            pnlMain.Controls.Add(cboCinema);
            pnlMain.Controls.Add(lblTemplate);
            pnlMain.Controls.Add(cboTemplate);
            pnlMain.Controls.Add(pnlSeatPreview);
            pnlMain.Controls.Add(btnSave);
            pnlMain.Controls.Add(btnCancel);
            pnlMain.CustomizableEdges = customizableEdges11;
            pnlMain.Location = new Point(30, 20);
            pnlMain.Margin = new Padding(4, 5, 4, 5);
            pnlMain.Name = "pnlMain";
            pnlMain.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pnlMain.Size = new Size(940, 600);
            pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.OrangeRed;
            lblTitle.Location = new Point(30, 20);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(417, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÊM PHÒNG CHIẾU MỚI";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.Location = new Point(35, 75);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(108, 28);
            lblName.TabIndex = 1;
            lblName.Text = "Tên phòng:";
            // 
            // txtRoomName
            // 
            txtRoomName.BorderRadius = 5;
            txtRoomName.CustomizableEdges = customizableEdges1;
            txtRoomName.DefaultText = "";
            txtRoomName.Font = new Font("Segoe UI", 9F);
            txtRoomName.Location = new Point(153, 75);
            txtRoomName.Margin = new Padding(6, 8, 6, 8);
            txtRoomName.Name = "txtRoomName";
            txtRoomName.PlaceholderText = "Ví dụ: Phòng 01";
            txtRoomName.SelectedText = "";
            txtRoomName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtRoomName.Size = new Size(280, 36);
            txtRoomName.TabIndex = 1;
            // 
            // lblCinema
            // 
            lblCinema.AutoSize = true;
            lblCinema.Font = new Font("Segoe UI", 10F);
            lblCinema.Location = new Point(443, 75);
            lblCinema.Margin = new Padding(4, 0, 4, 0);
            lblCinema.Name = "lblCinema";
            lblCinema.Size = new Size(103, 28);
            lblCinema.TabIndex = 2;
            lblCinema.Text = "Thuộc rạp:";
            // 
            // cboCinema
            // 
            cboCinema.BackColor = Color.Transparent;
            cboCinema.BorderRadius = 5;
            cboCinema.CustomizableEdges = customizableEdges3;
            cboCinema.DrawMode = DrawMode.OwnerDrawFixed;
            cboCinema.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCinema.FocusedColor = Color.Empty;
            cboCinema.Font = new Font("Segoe UI", 10F);
            cboCinema.ForeColor = Color.FromArgb(68, 88, 112);
            cboCinema.ItemHeight = 30;
            cboCinema.Location = new Point(554, 75);
            cboCinema.Margin = new Padding(4, 5, 4, 5);
            cboCinema.Name = "cboCinema";
            cboCinema.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cboCinema.Size = new Size(350, 36);
            cboCinema.TabIndex = 2;
            // 
            // lblTemplate
            // 
            lblTemplate.AutoSize = true;
            lblTemplate.Font = new Font("Segoe UI", 10F);
            lblTemplate.Location = new Point(35, 130);
            lblTemplate.Margin = new Padding(4, 0, 4, 0);
            lblTemplate.Name = "lblTemplate";
            lblTemplate.Size = new Size(168, 28);
            lblTemplate.TabIndex = 3;
            lblTemplate.Text = "Chọn mẫu phòng:";
            // 
            // cboTemplate
            // 
            cboTemplate.BackColor = Color.Transparent;
            cboTemplate.BorderRadius = 5;
            cboTemplate.CustomizableEdges = customizableEdges5;
            cboTemplate.DrawMode = DrawMode.OwnerDrawFixed;
            cboTemplate.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTemplate.FocusedColor = Color.Empty;
            cboTemplate.Font = new Font("Segoe UI", 10F);
            cboTemplate.ForeColor = Color.FromArgb(68, 88, 112);
            cboTemplate.ItemHeight = 30;
            cboTemplate.Items.AddRange(new object[] { "Nhỏ (10x10)", "Vừa (12x16)", "Lớn (15x20)", "IMAX (20x25)" });
            cboTemplate.Location = new Point(211, 130);
            cboTemplate.Margin = new Padding(4, 5, 4, 5);
            cboTemplate.Name = "cboTemplate";
            cboTemplate.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cboTemplate.Size = new Size(300, 36);
            cboTemplate.TabIndex = 4;
            // 
            // pnlSeatPreview
            // 
            pnlSeatPreview.AutoScroll = true;
            pnlSeatPreview.BackColor = Color.WhiteSmoke;
            pnlSeatPreview.BorderStyle = BorderStyle.FixedSingle;
            pnlSeatPreview.Location = new Point(35, 180);
            pnlSeatPreview.Margin = new Padding(4, 5, 4, 5);
            pnlSeatPreview.Name = "pnlSeatPreview";
            pnlSeatPreview.Size = new Size(860, 340);
            pnlSeatPreview.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 5;
            btnSave.CustomizableEdges = customizableEdges7;
            btnSave.FillColor = Color.Red;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(650, 540);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnSave.Size = new Size(120, 45);
            btnSave.TabIndex = 6;
            btnSave.Text = "LƯU";
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 5;
            btnCancel.CustomizableEdges = customizableEdges9;
            btnCancel.FillColor = Color.Gray;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(780, 540);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnCancel.Size = new Size(120, 45);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "HỦY";
            // 
            // UC_AddRoom
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            Controls.Add(pnlMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "UC_AddRoom";
            Size = new Size(1000, 660);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // Khai báo biến
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private Guna.UI2.WinForms.Guna2TextBox txtRoomName;
        private System.Windows.Forms.Label lblCinema;
        private Guna.UI2.WinForms.Guna2ComboBox cboCinema;
        private System.Windows.Forms.Label lblTemplate;
        private Guna.UI2.WinForms.Guna2ComboBox cboTemplate;
        private System.Windows.Forms.Panel pnlSeatPreview;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}