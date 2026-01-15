using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;

namespace MovieBookingClient.UI.Modules
{
    public class UcThemSuaLichChieu : UserControl
    {
        public event Action OnSaved;
        public event Action OnHuy;

        private readonly ShowtimeService _showtimeService;
        private readonly MovieService _movieService;
        private readonly RoomService _roomService;
        private int? _showtimeId;

        // --- KHAI BÁO CONTROLS ---
        private Guna2ComboBox cboPhim, cboPhong;
        private Guna2DateTimePicker dtpNgay;
        private Guna2TextBox txtGio;
        private Guna2NumericUpDown numGiaVe;
        private Guna2Button btnLuu, btnHuy;
        private Label lblTitle;

        // SỬA LỖI: Dùng Guna2ShadowPanel thay vì Guna2Panel
        private Guna2ShadowPanel shadowPanel;

        public UcThemSuaLichChieu(int? id = null)
        {
            _showtimeService = new ShowtimeService();
            _movieService = new MovieService();
            _roomService = new RoomService();
            _showtimeId = id;

            InitializeComponent();
            this.Load += async (s, e) => await LoadData();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1100, 700);
            this.BackColor = Color.WhiteSmoke;

            // SỬA LỖI: Khởi tạo Guna2ShadowPanel
            shadowPanel = new Guna2ShadowPanel
            {
                Size = new Size(500, 480),
                Location = new Point((this.Width - 500) / 2, (this.Height - 480) / 2),
                FillColor = Color.White,

                // Thuộc tính đổ bóng chuẩn của Guna2ShadowPanel
                Radius = 10,
                ShadowColor = Color.Gray,
                ShadowDepth = 50,
                ShadowShift = 5,
                BackColor = Color.Transparent // Quan trọng để thấy bóng
            };
            this.Controls.Add(shadowPanel);

            lblTitle = new Label
            {
                Text = _showtimeId.HasValue ? "CẬP NHẬT LỊCH CHIẾU" : "THÊM LỊCH CHIẾU MỚI",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(80, 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };
            shadowPanel.Controls.Add(lblTitle);

            int x = 40, y = 80, w = 420, gap = 70;

            // 1. Chọn Phim
            shadowPanel.Controls.Add(new Label { Text = "Chọn phim:", Location = new Point(x, y - 25), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            cboPhim = new Guna2ComboBox { Location = new Point(x, y), Width = w, BorderRadius = 5 };
            shadowPanel.Controls.Add(cboPhim);

            y += gap;
            // 2. Chọn Phòng
            shadowPanel.Controls.Add(new Label { Text = "Phòng chiếu:", Location = new Point(x, y - 25), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            cboPhong = new Guna2ComboBox { Location = new Point(x, y), Width = w, BorderRadius = 5 };
            shadowPanel.Controls.Add(cboPhong);

            y += gap;
            // 3. Ngày & Giờ
            shadowPanel.Controls.Add(new Label { Text = "Ngày chiếu:", Location = new Point(x, y - 25), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            dtpNgay = new Guna2DateTimePicker { Location = new Point(x, y), Width = 180, BorderRadius = 5, Format = DateTimePickerFormat.Short };
            shadowPanel.Controls.Add(dtpNgay);

            shadowPanel.Controls.Add(new Label { Text = "Giờ (HH:mm):", Location = new Point(x + 200, y - 25), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            txtGio = new Guna2TextBox { Location = new Point(x + 200, y), Width = 100, BorderRadius = 5, PlaceholderText = "19:00" };
            shadowPanel.Controls.Add(txtGio);

            shadowPanel.Controls.Add(new Label { Text = "Giá vé:", Location = new Point(x + 320, y - 25), Font = new Font("Segoe UI", 9, FontStyle.Bold) });
            numGiaVe = new Guna2NumericUpDown { Location = new Point(x + 320, y), Width = 100, BorderRadius = 5, Maximum = 1000000, Value = 75000 };
            shadowPanel.Controls.Add(numGiaVe);

            y += gap + 20;
            // 4. Buttons
            btnLuu = new Guna2Button { Text = "LƯU LẠI", Location = new Point(x + 50, y), Size = new Size(130, 45), BorderRadius = 5, FillColor = Color.SeaGreen };
            btnLuu.Click += BtnLuu_Click;
            shadowPanel.Controls.Add(btnLuu);

            btnHuy = new Guna2Button { Text = "QUAY LẠI", Location = new Point(x + 200, y), Size = new Size(130, 45), BorderRadius = 5, FillColor = Color.Gray };
            btnHuy.Click += (s, e) => OnHuy?.Invoke();
            shadowPanel.Controls.Add(btnHuy);
        }

        private async Task LoadData()
        {
            try
            {
                // Load Phim
                var movies = await _movieService.GetAllMoviesAsync();
                if (movies != null)
                {
                    cboPhim.DataSource = movies;
                    cboPhim.DisplayMember = "Title";
                    cboPhim.ValueMember = "Id";
                }

                // Load Phòng - Dùng RoomName/RoomId khớp với RoomDTO của bạn
                var rooms = await _roomService.GetAllRoomsAsync();
                if (rooms != null)
                {
                    cboPhong.DataSource = rooms;
                    cboPhong.DisplayMember = "RoomName"; // Sửa thành RoomName
                    cboPhong.ValueMember = "RoomId";     // Sửa thành RoomId
                }
            }
            catch { /* Ignore */ }
        }

        private async void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cboPhim.SelectedValue == null || cboPhong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!"); return;
            }
            if (!TimeSpan.TryParse(txtGio.Text, out TimeSpan time))
            {
                MessageBox.Show("Giờ chiếu không hợp lệ (VD: 19:30)"); return;
            }

            var req = new CreateShowtimeRequest
            {
                MovieId = (int)cboPhim.SelectedValue,
                RoomId = (int)cboPhong.SelectedValue,
                StartTime = dtpNgay.Value.Date + time,
                Price = (decimal)numGiaVe.Value
            };

            btnLuu.Enabled = false;
            bool ok = _showtimeId.HasValue
                ? await _showtimeService.UpdateShowtimeAsync(_showtimeId.Value, req)
                : await _showtimeService.CreateShowtimeAsync(req);
            btnLuu.Enabled = true;

            if (ok) { MessageBox.Show("Thành công!"); OnSaved?.Invoke(); }
            else MessageBox.Show("Thất bại!");
        }
    }
}