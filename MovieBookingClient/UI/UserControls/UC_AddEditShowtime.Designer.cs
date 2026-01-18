namespace MovieBookingClient.UI.UserControls.Admin
{
    partial class UC_AddEditShowtime
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlMain = new Guna.UI2.WinForms.Guna2ShadowPanel();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            btnSave = new Guna.UI2.WinForms.Guna2Button();
            txtBasePrice = new Guna.UI2.WinForms.Guna2TextBox();
            lblBasePrice = new Label();
            dtpDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            dtpTime = new DateTimePicker();
            lblStartTime = new Label();
            cboRoom = new Guna.UI2.WinForms.Guna2ComboBox();
            lblRoom = new Label();
            cboCinema = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCinema = new Label();
            cboMovie = new Guna.UI2.WinForms.Guna2ComboBox();
            lblMovie = new Label();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.Transparent;
            pnlMain.Controls.Add(btnCancel);
            pnlMain.Controls.Add(btnSave);
            pnlMain.Controls.Add(txtBasePrice);
            pnlMain.Controls.Add(lblBasePrice);
            pnlMain.Controls.Add(dtpDate);
            pnlMain.Controls.Add(dtpTime);
            pnlMain.Controls.Add(lblStartTime);
            pnlMain.Controls.Add(cboRoom);
            pnlMain.Controls.Add(lblRoom);
            pnlMain.Controls.Add(cboCinema);
            pnlMain.Controls.Add(lblCinema);
            pnlMain.Controls.Add(cboMovie);
            pnlMain.Controls.Add(lblMovie);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.FillColor = Color.White;
            pnlMain.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlMain.Location = new Point(103, 25);
            pnlMain.Name = "pnlMain";
            pnlMain.Radius = 6;
            pnlMain.ShadowColor = Color.Black;
            pnlMain.ShadowDepth = 50;
            pnlMain.Size = new Size(600, 642);
            pnlMain.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 6;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.FillColor = Color.Transparent;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Location = new Point(336, 578);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "HỦY";
            // 
            // btnSave
            // 
            btnSave.BorderRadius = 6;
            btnSave.CustomizableEdges = customizableEdges3;
            btnSave.FillColor = Color.Red;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(462, 578);
            btnSave.Name = "btnSave";
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 11;
            btnSave.Text = "LƯU";
            // 
            // txtBasePrice
            // 
            txtBasePrice.BorderRadius = 4;
            txtBasePrice.CustomizableEdges = customizableEdges5;
            txtBasePrice.DefaultText = "";
            txtBasePrice.Font = new Font("Segoe UI", 9F);
            txtBasePrice.Location = new Point(32, 517);
            txtBasePrice.Margin = new Padding(4, 5, 4, 5);
            txtBasePrice.Name = "txtBasePrice";
            txtBasePrice.PlaceholderText = "Ví dụ: 100000";
            txtBasePrice.SelectedText = "";
            txtBasePrice.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtBasePrice.Size = new Size(530, 36);
            txtBasePrice.TabIndex = 10;
            // 
            // lblBasePrice
            // 
            lblBasePrice.AutoSize = true;
            lblBasePrice.Font = new Font("Segoe UI", 9.75F);
            lblBasePrice.Location = new Point(30, 484);
            lblBasePrice.Name = "lblBasePrice";
            lblBasePrice.Size = new Size(128, 28);
            lblBasePrice.TabIndex = 9;
            lblBasePrice.Text = "Giá vé (VND):";
            // 
            // dtpDate
            // 
            dtpDate.BorderRadius = 4;
            dtpDate.Checked = true;
            dtpDate.CustomizableEdges = customizableEdges7;
            dtpDate.FillColor = Color.White;
            dtpDate.Font = new Font("Segoe UI", 9F);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(32, 422);
            dtpDate.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtpDate.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpDate.Name = "dtpDate";
            dtpDate.ShadowDecoration.CustomizableEdges = customizableEdges8;
            dtpDate.Size = new Size(300, 36);
            dtpDate.TabIndex = 8;
            dtpDate.Value = new DateTime(2026, 1, 18, 22, 14, 35, 741);
            // 
            // dtpTime
            // 
            dtpTime.CalendarFont = new Font("Segoe UI", 10F);
            dtpTime.CustomFormat = "HH:mm";
            dtpTime.Font = new Font("Segoe UI", 11F);
            dtpTime.Format = DateTimePickerFormat.Custom;
            dtpTime.Location = new Point(342, 422);
            dtpTime.Name = "dtpTime";
            dtpTime.ShowUpDown = true;
            dtpTime.Size = new Size(220, 37);
            dtpTime.TabIndex = 9;
            dtpTime.Value = new DateTime(2026, 1, 18, 22, 14, 35, 744);
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Font = new Font("Segoe UI", 9.75F);
            lblStartTime.Location = new Point(30, 379);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(261, 28);
            lblStartTime.TabIndex = 7;
            lblStartTime.Text = "Thời gian chiếu (Ngày - Giờ):";
            // 
            // cboRoom
            // 
            cboRoom.BackColor = Color.Transparent;
            cboRoom.BorderRadius = 4;
            cboRoom.CustomizableEdges = customizableEdges9;
            cboRoom.DrawMode = DrawMode.OwnerDrawFixed;
            cboRoom.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRoom.FocusedColor = Color.Empty;
            cboRoom.Font = new Font("Segoe UI", 10F);
            cboRoom.ForeColor = Color.FromArgb(68, 88, 112);
            cboRoom.ItemHeight = 30;
            cboRoom.Location = new Point(32, 340);
            cboRoom.Name = "cboRoom";
            cboRoom.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cboRoom.Size = new Size(530, 36);
            cboRoom.TabIndex = 6;
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Font = new Font("Segoe UI", 9.75F);
            lblRoom.Location = new Point(30, 300);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(125, 28);
            lblRoom.TabIndex = 5;
            lblRoom.Text = "Chọn phòng:";
            // 
            // cboCinema
            // 
            cboCinema.BackColor = Color.Transparent;
            cboCinema.BorderRadius = 4;
            cboCinema.CustomizableEdges = customizableEdges11;
            cboCinema.DrawMode = DrawMode.OwnerDrawFixed;
            cboCinema.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCinema.FocusedColor = Color.Empty;
            cboCinema.Font = new Font("Segoe UI", 10F);
            cboCinema.ForeColor = Color.FromArgb(68, 88, 112);
            cboCinema.ItemHeight = 30;
            cboCinema.Location = new Point(30, 241);
            cboCinema.Name = "cboCinema";
            cboCinema.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cboCinema.Size = new Size(530, 36);
            cboCinema.TabIndex = 4;
            // 
            // lblCinema
            // 
            lblCinema.AutoSize = true;
            lblCinema.Font = new Font("Segoe UI", 9.75F);
            lblCinema.Location = new Point(32, 201);
            lblCinema.Name = "lblCinema";
            lblCinema.Size = new Size(96, 28);
            lblCinema.TabIndex = 3;
            lblCinema.Text = "Chọn rạp:";
            // 
            // cboMovie
            // 
            cboMovie.BackColor = Color.Transparent;
            cboMovie.BorderRadius = 4;
            cboMovie.CustomizableEdges = customizableEdges13;
            cboMovie.DrawMode = DrawMode.OwnerDrawFixed;
            cboMovie.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMovie.FocusedColor = Color.FromArgb(94, 148, 255);
            cboMovie.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cboMovie.Font = new Font("Segoe UI", 10F);
            cboMovie.ForeColor = Color.FromArgb(68, 88, 112);
            cboMovie.ItemHeight = 30;
            cboMovie.Location = new Point(32, 139);
            cboMovie.Name = "cboMovie";
            cboMovie.ShadowDecoration.CustomizableEdges = customizableEdges14;
            cboMovie.Size = new Size(530, 36);
            cboMovie.TabIndex = 2;
            // 
            // lblMovie
            // 
            lblMovie.AutoSize = true;
            lblMovie.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMovie.Location = new Point(30, 99);
            lblMovie.Name = "lblMovie";
            lblMovie.Size = new Size(112, 28);
            lblMovie.TabIndex = 1;
            lblMovie.Text = "Chọn phim:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.OrangeRed;
            lblTitle.Location = new Point(30, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(371, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÊM MỚI LỊCH CHIẾU";
            // 
            // UC_AddEditShowtime
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.Info;
            Controls.Add(pnlMain);
            Name = "UC_AddEditShowtime";
            Size = new Size(861, 727);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMovie;
        private Guna.UI2.WinForms.Guna2ComboBox cboMovie;
        private System.Windows.Forms.Label lblCinema;
        private Guna.UI2.WinForms.Guna2ComboBox cboCinema;
        private System.Windows.Forms.Label lblRoom;
        private Guna.UI2.WinForms.Guna2ComboBox cboRoom;
        private System.Windows.Forms.Label lblStartTime;

        // 2 Control riêng biệt:
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDate;
        private System.Windows.Forms.DateTimePicker dtpTime; // Control chuẩn để hỗ trợ typing tốt nhất

        private System.Windows.Forms.Label lblBasePrice;
        private Guna.UI2.WinForms.Guna2TextBox txtBasePrice;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}