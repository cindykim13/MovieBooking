namespace MovieBookingClient.UI.UserControls
{
    partial class UC_MemberInfo
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtFullName;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtPhone;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblRole;
        private Guna.UI2.WinForms.Guna2Panel pnlContainer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlContainer = new Guna.UI2.WinForms.Guna2Panel();
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtFullName = new Guna.UI2.WinForms.Guna2TextBox();
            txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            txtPhone = new Guna.UI2.WinForms.Guna2TextBox();
            lblRole = new Guna.UI2.WinForms.Guna2HtmlLabel();
            pnlContainer.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Red;
            lblTitle.Location = new Point(40, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(339, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN THÀNH VIÊN\r\n";
            // 
            // pnlContainer
            // 
            pnlContainer.BorderRadius = 10;
            pnlContainer.Controls.Add(txtUsername);
            pnlContainer.Controls.Add(txtFullName);
            pnlContainer.Controls.Add(txtEmail);
            pnlContainer.Controls.Add(txtPhone);
            pnlContainer.Controls.Add(lblRole);
            pnlContainer.CustomizableEdges = customizableEdges9;
            pnlContainer.FillColor = Color.White;
            pnlContainer.ForeColor = Color.Red;
            pnlContainer.Location = new Point(40, 80);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlContainer.Size = new Size(921, 579);
            pnlContainer.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 10;
            txtUsername.CustomizableEdges = customizableEdges1;
            txtUsername.DefaultText = "";
            txtUsername.Font = new Font("Segoe UI", 9F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.Location = new Point(30, 30);
            txtUsername.Margin = new Padding(4, 5, 4, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.ReadOnly = true;
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtUsername.Size = new Size(864, 102);
            txtUsername.TabIndex = 0;
            // 
            // txtFullName
            // 
            txtFullName.BorderRadius = 10;
            txtFullName.CustomizableEdges = customizableEdges3;
            txtFullName.DefaultText = "";
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.ForeColor = Color.Black;
            txtFullName.Location = new Point(30, 142);
            txtFullName.Margin = new Padding(4, 5, 4, 5);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Họ và tên";
            txtFullName.SelectedText = "";
            txtFullName.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtFullName.Size = new Size(864, 115);
            txtFullName.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 10;
            txtEmail.CustomizableEdges = customizableEdges5;
            txtEmail.DefaultText = "";
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.ForeColor = Color.Black;
            txtEmail.Location = new Point(30, 267);
            txtEmail.Margin = new Padding(4, 5, 4, 5);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.ReadOnly = true;
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtEmail.Size = new Size(864, 121);
            txtEmail.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.BorderRadius = 10;
            txtPhone.CustomizableEdges = customizableEdges7;
            txtPhone.DefaultText = "";
            txtPhone.Font = new Font("Segoe UI", 9F);
            txtPhone.ForeColor = Color.Black;
            txtPhone.Location = new Point(30, 398);
            txtPhone.Margin = new Padding(4, 5, 4, 5);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "Số điện thoại";
            txtPhone.SelectedText = "";
            txtPhone.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPhone.Size = new Size(864, 109);
            txtPhone.TabIndex = 3;
            // 
            // lblRole
            // 
            lblRole.BackColor = Color.Transparent;
            lblRole.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRole.ForeColor = Color.Red;
            lblRole.Location = new Point(30, 525);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(219, 40);
            lblRole.TabIndex = 4;
            lblRole.Text = "Vai trò: Member";
            // 
            // UC_MemberInfo
            // 
            BackColor = SystemColors.Info;
            Controls.Add(lblTitle);
            Controls.Add(pnlContainer);
            Name = "UC_MemberInfo";
            Size = new Size(1000, 700);
            Load += UC_MemberInfo_Load;
            pnlContainer.ResumeLayout(false);
            pnlContainer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}