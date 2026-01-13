namespace MovieBookingClient.UI.UserControls
{
    partial class UC_Login
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelLeft = new Guna.UI2.WinForms.Guna2Panel();
            lblForgotPass = new Label();
            btnLogin = new Guna.UI2.WinForms.Guna2Button();
            lblPassword = new Label();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            lblUsername = new Label();
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            panelTabs = new Guna.UI2.WinForms.Guna2Panel();
            btnTabRegister = new Guna.UI2.WinForms.Guna2Button();
            btnTabLogin = new Guna.UI2.WinForms.Guna2Button();
            logoContainer = new Label();
            panelRight = new Guna.UI2.WinForms.Guna2Panel();
            lblSlogan = new Label();
            panelLeft.SuspendLayout();
            panelTabs.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = SystemColors.Info;
            panelLeft.Controls.Add(lblForgotPass);
            panelLeft.Controls.Add(btnLogin);
            panelLeft.Controls.Add(lblPassword);
            panelLeft.Controls.Add(txtPassword);
            panelLeft.Controls.Add(lblUsername);
            panelLeft.Controls.Add(txtUsername);
            panelLeft.Controls.Add(panelTabs);
            panelLeft.Controls.Add(logoContainer);
            panelLeft.CustomizableEdges = customizableEdges13;
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelLeft.Size = new Size(420, 550);
            panelLeft.TabIndex = 0;
            // 
            // lblForgotPass
            // 
            lblForgotPass.AutoSize = true;
            lblForgotPass.Cursor = Cursors.Hand;
            lblForgotPass.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            lblForgotPass.ForeColor = Color.FromArgb(100, 100, 100);
            lblForgotPass.Location = new Point(95, 457);
            lblForgotPass.Name = "lblForgotPass";
            lblForgotPass.Size = new Size(156, 15);
            lblForgotPass.TabIndex = 7;
            lblForgotPass.Text = "Bạn muốn tìm lại mật khẩu?";
            // 
            // btnLogin
            // 
            btnLogin.BorderRadius = 4;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.CustomizableEdges = customizableEdges1;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.FillColor = Color.FromArgb(212, 33, 33);
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 390);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnLogin.Size = new Size(340, 45);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "ĐĂNG NHẬP";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(64, 64, 64);
            lblPassword.Location = new Point(40, 300);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(59, 15);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            txtPassword.BorderColor = Color.Silver;
            txtPassword.BorderRadius = 4;
            txtPassword.Cursor = Cursors.IBeam;
            txtPassword.CustomizableEdges = customizableEdges3;
            txtPassword.DefaultText = "";
            txtPassword.FocusedState.BorderColor = Color.FromArgb(212, 33, 33);
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.Location = new Point(40, 320);
            txtPassword.Margin = new Padding(4, 5, 4, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "Mật khẩu";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtPassword.Size = new Size(340, 40);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(64, 64, 64);
            lblUsername.Location = new Point(40, 210);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(148, 15);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Email hoặc tên đăng nhập";
            // 
            // txtUsername
            // 
            txtUsername.BorderColor = Color.Silver;
            txtUsername.BorderRadius = 4;
            txtUsername.Cursor = Cursors.IBeam;
            txtUsername.CustomizableEdges = customizableEdges5;
            txtUsername.DefaultText = "";
            txtUsername.FocusedState.BorderColor = Color.FromArgb(212, 33, 33);
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.Location = new Point(40, 230);
            txtUsername.Margin = new Padding(4, 5, 4, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Email hoặc tên đăng nhập";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtUsername.Size = new Size(340, 40);
            txtUsername.TabIndex = 2;
            // 
            // panelTabs
            // 
            panelTabs.Controls.Add(btnTabRegister);
            panelTabs.Controls.Add(btnTabLogin);
            panelTabs.CustomizableEdges = customizableEdges11;
            panelTabs.Location = new Point(40, 130);
            panelTabs.Name = "panelTabs";
            panelTabs.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelTabs.Size = new Size(340, 50);
            panelTabs.TabIndex = 1;
            // 
            // btnTabRegister
            // 
            btnTabRegister.BorderColor = Color.FromArgb(224, 224, 224);
            btnTabRegister.BorderThickness = 1;
            btnTabRegister.Cursor = Cursors.Hand;
            btnTabRegister.CustomizableEdges = customizableEdges7;
            btnTabRegister.Dock = DockStyle.Right;
            btnTabRegister.FillColor = Color.WhiteSmoke;
            btnTabRegister.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTabRegister.ForeColor = Color.Gray;
            btnTabRegister.Location = new Point(170, 0);
            btnTabRegister.Name = "btnTabRegister";
            btnTabRegister.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnTabRegister.Size = new Size(170, 50);
            btnTabRegister.TabIndex = 1;
            btnTabRegister.Text = "ĐĂNG KÝ";
            // 
            // btnTabLogin
            // 
            btnTabLogin.BorderColor = Color.FromArgb(212, 33, 33);
            btnTabLogin.BorderThickness = 1;
            btnTabLogin.CustomizableEdges = customizableEdges9;
            btnTabLogin.Dock = DockStyle.Left;
            btnTabLogin.FillColor = Color.FromArgb(212, 33, 33);
            btnTabLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTabLogin.ForeColor = Color.White;
            btnTabLogin.Location = new Point(0, 0);
            btnTabLogin.Name = "btnTabLogin";
            btnTabLogin.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnTabLogin.Size = new Size(170, 50);
            btnTabLogin.TabIndex = 0;
            btnTabLogin.Text = "ĐĂNG NHẬP";
            // 
            // logoContainer
            // 
            logoContainer.Font = new Font("Impact", 28F, FontStyle.Regular, GraphicsUnit.Point, 0);
            logoContainer.ForeColor = Color.FromArgb(212, 33, 33);
            logoContainer.Location = new Point(0, 30);
            logoContainer.Name = "logoContainer";
            logoContainer.Size = new Size(420, 80);
            logoContainer.TabIndex = 0;
            logoContainer.Text = "VIP CINEMAS";
            logoContainer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(254, 245, 237);
            panelRight.Controls.Add(lblSlogan);
            panelRight.CustomizableEdges = customizableEdges15;
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(420, 0);
            panelRight.Name = "panelRight";
            panelRight.ShadowDecoration.CustomizableEdges = customizableEdges16;
            panelRight.Size = new Size(460, 550);
            panelRight.TabIndex = 1;
            // 
            // lblSlogan
            // 
            lblSlogan.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblSlogan.ForeColor = Color.FromArgb(212, 33, 33);
            lblSlogan.Location = new Point(0, 220);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(460, 100);
            lblSlogan.TabIndex = 0;
            lblSlogan.Text = "Trải nghiệm điện ảnh \r\nđẳng cấp thế giới";
            lblSlogan.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UC_Login
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Name = "UC_Login";
            Size = new Size(880, 550);
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelTabs.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelLeft;
        private Guna.UI2.WinForms.Guna2Panel panelRight;
        private System.Windows.Forms.Label logoContainer;
        private Guna.UI2.WinForms.Guna2Panel panelTabs;
        private Guna.UI2.WinForms.Guna2Button btnTabRegister;
        private Guna.UI2.WinForms.Guna2Button btnTabLogin;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private System.Windows.Forms.Label lblForgotPass;
        private System.Windows.Forms.Label lblSlogan;
    }
}