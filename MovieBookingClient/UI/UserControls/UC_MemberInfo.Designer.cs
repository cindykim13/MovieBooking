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
            lblTitle.Location = new Point(40, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(293, 34);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "<b style='color:#D42121; font-size:24px'>THÔNG TIN THÀNH VIÊN</b>";
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
            pnlContainer.FillColor = Color.WhiteSmoke;
            pnlContainer.Location = new Point(40, 80);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pnlContainer.Size = new Size(600, 400);
            pnlContainer.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 10;
            txtUsername.CustomizableEdges = customizableEdges1;
            txtUsername.DefaultText = "";
            txtUsername.Font = new Font("Segoe UI", 9F);
            txtUsername.Location = new Point(30, 30);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Tên đăng nhập";
            txtUsername.ReadOnly = true;
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtUsername.Size = new Size(540, 40);
            txtUsername.TabIndex = 0;
            // 
            // txtFullName
            // 
            txtFullName.BorderRadius = 10;
            txtFullName.CustomizableEdges = customizableEdges3;
            txtFullName.DefaultText = "";
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.Location = new Point(30, 90);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Họ và tên";
            txtFullName.SelectedText = "";
            txtFullName.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtFullName.Size = new Size(540, 40);
            txtFullName.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 10;
            txtEmail.CustomizableEdges = customizableEdges5;
            txtEmail.DefaultText = "";
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(30, 150);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.ReadOnly = true;
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtEmail.Size = new Size(540, 40);
            txtEmail.TabIndex = 2;
            // 
            // txtPhone
            // 
            txtPhone.BorderRadius = 10;
            txtPhone.CustomizableEdges = customizableEdges7;
            txtPhone.DefaultText = "";
            txtPhone.Font = new Font("Segoe UI", 9F);
            txtPhone.Location = new Point(30, 210);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "Số điện thoại";
            txtPhone.SelectedText = "";
            txtPhone.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPhone.Size = new Size(540, 40);
            txtPhone.TabIndex = 3;
            // 
            // lblRole
            // 
            lblRole.BackColor = Color.Transparent;
            lblRole.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRole.Location = new Point(35, 270);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(126, 23);
            lblRole.TabIndex = 4;
            lblRole.Text = "Vai trò: Member";
            // 
            // UC_MemberInfo
            // 
            BackColor = Color.White;
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
