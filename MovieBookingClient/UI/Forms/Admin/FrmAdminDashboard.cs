using MovieBookingClient.UI.Forms.Admin; // 👇 Quan trọng: Phải có dòng này mới gọi được các Form con
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MovieBookingClient.Forms.Admin
{
    public partial class FrmAdminDashboard : Form
    {
        public FrmAdminDashboard()
        {
            InitializeComponent();

            // 👇 THAY ĐỔI 1: Vừa mở Form lên là hiện Trang Chủ Thống Kê ngay
            HienThiUserControl(new FrmDashboardHome());
        }

        // --- HÀM DÙNG CHUNG ĐỂ HIỂN THỊ USER CONTROL ---
        private void HienThiUserControl(UserControl uc)
        {
            // 1. Xóa nội dung cũ
            pnMain.Controls.Clear();

            // 2. Cấu hình tràn màn hình
            uc.Dock = DockStyle.Fill;

            // 3. Thêm vào Panel chính
            pnMain.Controls.Add(uc);
            uc.BringToFront();
        }

        // --- SỰ KIỆN MENU ---

        // 1. Nút TRANG CHỦ
        private void button4_Click(object sender, EventArgs e)
        {
            // 👇 THAY ĐỔI 2: Gọi lại Trang Chủ Thống Kê thay vì để trống
            HienThiUserControl(new FrmDashboardHome());

            lblTitle.Text = "TỔNG QUAN";
        }

        // 2. Nút QUẢN LÝ PHIM
        private void btnPhim_Click(object sender, EventArgs e)
        {
            HienThiUserControl(new FrmQuanLyPhim());
            lblTitle.Text = "QUẢN LÝ PHIM";
        }

        // 3. Nút QUẢN LÝ LỊCH CHIẾU
        private void btnLichChieu_Click(object sender, EventArgs e)
        {
            HienThiUserControl(new FrmQuanLyLichChieu());
            lblTitle.Text = "QUẢN LÝ LỊCH CHIẾU";
        }

        // 4. Nút QUẢN LÝ PHÒNG
        private void btnPhong_Click(object sender, EventArgs e)
        {
            HienThiUserControl(new FrmQuanLyPhong());
            lblTitle.Text = "QUẢN LÝ PHÒNG CHIẾU";
        }

        // 5. Nút ĐĂNG XUẤT
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Đóng form Admin -> Quay về Login
            }
        }

        // Sự kiện vẽ (để trống tránh lỗi)
        private void pnlBody_Paint(object sender, PaintEventArgs e) { }
    }
}