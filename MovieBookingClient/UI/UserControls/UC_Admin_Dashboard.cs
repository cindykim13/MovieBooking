using System;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_Admin_Dashboard : UserControl
    {
        public UC_Admin_Dashboard()
        {
            InitializeComponent();

            // Gọi hàm tải dữ liệu mẫu ngay khi khởi tạo
            // Để giao diện không bị trống trơn khi chạy thử
            LoadDummyData();
        }

        private void LoadDummyData()
        {
            // 1. Giả lập số liệu thống kê trên các Card
            lblRevenueVal.Text = "15,450,000 đ"; // Giả sử doanh thu hôm nay
            lblTicketVal.Text = "128 Vé";        // Giả sử số vé bán ra
            lblMovieVal.Text = "6 Phim";         // Giả sử số phim đang chiếu

            // 2. Cấu hình cột cho DataGridView 
            // (Vì trong file Designer chúng ta chưa add cột, nên phải add bằng code để bảng hiện ra đẹp)
            dgvRecentTransactions.Columns.Clear();

            dgvRecentTransactions.Columns.Add("TransactionId", "Mã GD");
            dgvRecentTransactions.Columns.Add("CustomerName", "Khách Hàng");
            dgvRecentTransactions.Columns.Add("MovieTitle", "Phim");
            dgvRecentTransactions.Columns.Add("ShowTime", "Suất Chiếu");
            dgvRecentTransactions.Columns.Add("TotalAmount", "Tổng Tiền");
            dgvRecentTransactions.Columns.Add("Status", "Trạng Thái");

            // Căn chỉnh độ rộng cột cho đẹp (Tùy chọn)
            dgvRecentTransactions.Columns["TransactionId"].Width = 80;
            dgvRecentTransactions.Columns["TotalAmount"].Width = 100;
            dgvRecentTransactions.Columns["Status"].Width = 100;

            // 3. Thêm các dòng dữ liệu mẫu vào bảng
            dgvRecentTransactions.Rows.Add("GD001", "Nguyễn Văn A", "Mai", "18/01/2026 18:00", "180,000 đ", "Đã thanh toán");
            dgvRecentTransactions.Rows.Add("GD002", "Trần Thị B", "Đào, Phở và Piano", "18/01/2026 19:30", "90,000 đ", "Đã thanh toán");
            dgvRecentTransactions.Rows.Add("GD003", "Lê Văn C", "Kung Fu Panda 4", "18/01/2026 20:00", "250,000 đ", "Đã thanh toán");
            dgvRecentTransactions.Rows.Add("GD004", "Phạm Thị D", "Exhuma: Quật Mộ", "18/01/2026 21:15", "110,000 đ", "Chờ thanh toán");
            dgvRecentTransactions.Rows.Add("GD005", "Hoàng Văn E", "Dune: Part Two", "18/01/2026 18:45", "320,000 đ", "Đã hủy");
            dgvRecentTransactions.Rows.Add("GD006", "Ngô Thị F", "Mai", "18/01/2026 22:00", "90,000 đ", "Đã thanh toán");
        }
    }
}