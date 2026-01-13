namespace MovieBookingClient.Forms.Admin
{
    partial class FrmAdminDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdminDashboard));
            splitContainer1 = new SplitContainer();
            btnPhong = new Button();
            button4 = new Button();
            btnLichChieu = new Button();
            btnPhim = new Button();
            pictureBox1 = new PictureBox();
            pnMain = new Panel();
            panel1 = new Panel();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.DarkSlateGray;
            splitContainer1.Panel1.Controls.Add(btnPhong);
            splitContainer1.Panel1.Controls.Add(button4);
            splitContainer1.Panel1.Controls.Add(btnLichChieu);
            splitContainer1.Panel1.Controls.Add(btnPhim);
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackgroundImage = Properties.Resources.logo4;
            splitContainer1.Panel2.Controls.Add(pnMain);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(1613, 707);
            splitContainer1.SplitterDistance = 413;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // btnPhong
            // 
            btnPhong.FlatAppearance.BorderSize = 0;
            btnPhong.FlatStyle = FlatStyle.Flat;
            btnPhong.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPhong.ForeColor = Color.White;
            btnPhong.Location = new Point(14, 456);
            btnPhong.Margin = new Padding(3, 4, 3, 4);
            btnPhong.Name = "btnPhong";
            btnPhong.Size = new Size(341, 51);
            btnPhong.TabIndex = 7;
            btnPhong.Text = "Quản lý Phòng";
            btnPhong.TextAlign = ContentAlignment.MiddleLeft;
            btnPhong.UseVisualStyleBackColor = true;
            btnPhong.Click += btnPhong_Click;
            // 
            // button4
            // 
            button4.FlatAppearance.BorderColor = Color.Black;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button4.ForeColor = Color.White;
            button4.Location = new Point(0, 205);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(413, 51);
            button4.TabIndex = 5;
            button4.Text = "Trang chủ";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // btnLichChieu
            // 
            btnLichChieu.Cursor = Cursors.IBeam;
            btnLichChieu.FlatAppearance.BorderColor = Color.Black;
            btnLichChieu.FlatAppearance.BorderSize = 0;
            btnLichChieu.FlatStyle = FlatStyle.Flat;
            btnLichChieu.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLichChieu.ForeColor = Color.White;
            btnLichChieu.Location = new Point(14, 300);
            btnLichChieu.Margin = new Padding(3, 4, 3, 4);
            btnLichChieu.Name = "btnLichChieu";
            btnLichChieu.Size = new Size(321, 51);
            btnLichChieu.TabIndex = 2;
            btnLichChieu.Text = "Quản lý lịch chiếu";
            btnLichChieu.TextAlign = ContentAlignment.MiddleLeft;
            btnLichChieu.UseVisualStyleBackColor = true;
            btnLichChieu.Click += btnLichChieu_Click;
            // 
            // btnPhim
            // 
            btnPhim.FlatAppearance.BorderSize = 0;
            btnPhim.FlatStyle = FlatStyle.Flat;
            btnPhim.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPhim.ForeColor = Color.White;
            btnPhim.Location = new Point(14, 381);
            btnPhim.Margin = new Padding(3, 4, 3, 4);
            btnPhim.Name = "btnPhim";
            btnPhim.Size = new Size(341, 51);
            btnPhim.TabIndex = 1;
            btnPhim.Text = "Quản lý Phim";
            btnPhim.TextAlign = ContentAlignment.MiddleLeft;
            btnPhim.UseVisualStyleBackColor = true;
            btnPhim.Click += btnPhim_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(425, 203);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pnMain
            // 
            pnMain.BackgroundImage = Properties.Resources.logo4;
            pnMain.Dock = DockStyle.Fill;
            pnMain.Location = new Point(0, 61);
            pnMain.Margin = new Padding(3, 4, 3, 4);
            pnMain.Name = "pnMain";
            pnMain.Size = new Size(1195, 646);
            pnMain.TabIndex = 1;
            pnMain.Paint += pnlBody_Paint;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1195, 61);
            panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.IndianRed;
            lblTitle.Location = new Point(461, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(201, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TỔNG QUAN";
            // 
            // FrmAdminDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(1613, 707);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmAdminDashboard";
            Text = "FrmAdminDashboard";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private PictureBox pictureBox1;
        private Button btnPhim;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button btnLichChieu;
        private Panel panel1;
        private Label lblTitle;
        private Button btnPhong;
        private Panel pnMain;
    }
}