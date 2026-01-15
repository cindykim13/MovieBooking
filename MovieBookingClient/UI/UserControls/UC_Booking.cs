using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Forms.Customer;
using MovieBookingClient.Services;
using MovieBookingClient.Session;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls
{
    public partial class UC_Booking : UserControl
    {
        // --- CẤU HÌNH KÍCH THƯỚC GHẾ ---
        private const int SEAT_SIZE = 60;       // Tăng từ 40 lên 55
        private const int SEAT_GAP = 10;
        // START_X sẽ được tính toán động, không dùng hằng số này nữa
        // private const int START_X = 50;         
        private const int START_Y = 120;         // Lề trên (dưới màn hình)

        // --- CẤU HÌNH MÀU SẮC (THEO CHUẨN CGV) ---
        private readonly Color COLOR_STANDARD = Color.White;
        private readonly Color COLOR_VIP = Color.MistyRose;      // Đỏ nhạt
        private readonly Color COLOR_SWEETBOX = Color.Pink;      // Hồng
        private readonly Color COLOR_SOLD = Color.DimGray;       // Xám tối (Đã bán)
        private readonly Color COLOR_SELECTING = Color.FromArgb(212, 33, 33); // Đỏ CGV (Đang chọn)
        private readonly Color BORDER_COLOR = Color.Silver;
        
        // --- Dependencies & State ---
        private readonly FrmMain _mainForm;
        private readonly int _showtimeId;
        private readonly ShowtimeService _showtimeService;
        private readonly BookingService _bookingService;
        private readonly BookingContextDTO _contextInfo;
        // Danh sách ghế đang được người dùng chọn (lưu trong RAM để tính tiền)
        private List<SeatDTO> _selectedSeats = new List<SeatDTO>();


        public UC_Booking(FrmMain mainForm, BookingContextDTO contextInfo)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _contextInfo = contextInfo; // Lưu toàn bộ context
            _showtimeId = contextInfo.ShowtimeId; // Lấy showtimeId từ context

            // Khởi tạo các service
            _showtimeService = new ShowtimeService();
            _bookingService = new BookingService();

            // Gán sự kiện
            this.Load += async (s, e) => await InitDataAsync();
            btnBack.Click += (s, e) => _mainForm.NavigateToHome();
            btnPayment.Click += BtnPayment_Click;
        }
        private async Task InitDataAsync()
        {
            // 1. Hiển thị thông tin phim ngay lập tức (vì đã có data từ Context)
            await RenderBookingInfo(); // Chuyển thành async để tải ảnh

            // 2. Tải sơ đồ ghế từ API
            await LoadSeatMapAsync();
        }
        // --- PHẦN 1: TẢI DỮ LIỆU ---
        // [SỬA ĐỔI]: Hàm hiển thị thông tin & Tải ảnh
        private async Task RenderBookingInfo()
        {
            if (_contextInfo != null)
            {
                lblMovieTitle.Text = _contextInfo.MovieTitle;
                lblCinemaRoom.Text = $"{_contextInfo.CinemaName}\n{_contextInfo.RoomName}"; // Xuống dòng cho đẹp
                lblShowtime.Text = $"Suất: {_contextInfo.ShowTime:HH:mm} | {_contextInfo.ShowTime:dd/MM/yyyy}";

                lblSeats.Text = "---";
                lblTotalPrice.Text = "0 đ";

                // Tải Poster
                if (!string.IsNullOrEmpty(_contextInfo.PosterUrl))
                {
                    try
                    {
                        using (var httpClient = new HttpClient())
                        {
                            var imageBytes = await httpClient.GetByteArrayAsync(_contextInfo.PosterUrl);
                            using (var ms = new System.IO.MemoryStream(imageBytes))
                            {
                                picPoster.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    catch
                    {
                        // Nếu lỗi tải ảnh, giữ nguyên ảnh mặc định hoặc để trống
                    }
                }
            }
        }
        private async Task LoadSeatMapAsync()
        {
            try
            {
                // seatMap ở đây là kiểu SeatMapDTO
                var seatMap = await _showtimeService.GetSeatMapAsync(_showtimeId);

                pnlSeatMap.Controls.Clear();
                RenderScreenArea();

                // FIX LỖI CS1061: Kiểm tra .Seats.Count thay vì kiểm tra trực tiếp trên seatMap
                if (seatMap != null && seatMap.Seats != null && seatMap.Seats.Count > 0)
                {
                    // FIX LỖI CS1503: Truyền seatMap.Seats (kiểu List<SeatDTO>) vào hàm RenderSeats
                    RenderSeats(seatMap.Seats);
                    UpdateSummaryPanel();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu ghế cho suất chiếu này.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private void RenderScreenArea()
        {
            // Tính toán vị trí giữa dựa trên chiều rộng Panel
            int centerX = pnlSeatMap.Width / 2;
            int screenWidth = 600;

            // Vẽ thanh kẻ ngang
            Guna2Panel line = new Guna2Panel
            {
                Size = new Size(screenWidth, 8), // Dày hơn chút
                Location = new Point(centerX - (screenWidth / 2), 40), // Căn giữa
                FillColor = Color.Silver,
                BorderRadius = 4
            };

            // Vẽ chữ MÀN HÌNH
            Label lblScreen = new Label
            {
                Text = "MÀN HÌNH",
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.Gray,
                // Căn giữa chữ so với thanh kẻ
                Location = new Point(centerX - 40, 15)
            };

            pnlSeatMap.Controls.Add(lblScreen);
            pnlSeatMap.Controls.Add(line);
        }

        // --- PHẦN 2: THUẬT TOÁN VẼ GHẾ (CORE LOGIC) ---
        private void RenderSeats(List<SeatDTO> seats)
        {
            // Tạm dừng vẽ giao diện để tăng tốc độ (tránh giật hình)
            pnlSeatMap.SuspendLayout();

            // Tính toán chiều rộng tổng thể của lưới ghế để căn giữa
            int maxColumn = seats.Max(s => s.GridColumn);
            // Nếu có Sweetbox (chiếm 2 ô), max col thực tế có thể lớn hơn số lượng ghế ngang
            // Giả sử max GridColumn là 12 (như trong DB)
            // Tính toán căn giữa sơ đồ ghế
            // Giả sử max column là 12
            int totalGridWidth = 12 * (SEAT_SIZE + SEAT_GAP);
            int startX = (pnlSeatMap.Width - totalGridWidth) / 2;
            if (startX < 20) startX = 20;

            foreach (var seat in seats)
            {
                Guna2Button btnSeat = new Guna2Button();

                // 1. Tính toán vị trí (Location) dựa trên GridRow/GridColumn từ API
                // Công thức: MarginLeft + (Col - 1) * (Size + Gap)
                int x = startX + (seat.GridColumn - 1) * (SEAT_SIZE + SEAT_GAP);
                int y = START_Y + (seat.GridRow - 1) * (SEAT_SIZE + SEAT_GAP);

                btnSeat.Location = new Point(x, y);


                // 2. Tính toán kích thước (Size)
                // Logic Sweetbox (So sánh không phân biệt hoa thường)
                if (string.Equals(seat.SeatType, "Sweetbox", StringComparison.OrdinalIgnoreCase))
                {
                    btnSeat.Size = new Size(SEAT_SIZE * 2 + SEAT_GAP, SEAT_SIZE);
                }
                else
                {
                    btnSeat.Size = new Size(SEAT_SIZE, SEAT_SIZE);
                }
                // Style chữ để không rớt dòng
                btnSeat.BorderRadius = 8; // Bo tròn hơn
                btnSeat.BorderThickness = 1;
                btnSeat.BorderColor = Color.LightGray;
                btnSeat.Font = new Font("Segoe UI", 9, FontStyle.Bold); // Giảm font nếu cần
                btnSeat.Text = seat.Row + seat.Number;
                btnSeat.Tag = seat;
                // 3. Thiết lập hiển thị (Style)
                btnSeat.BorderRadius = 4;
                btnSeat.BorderThickness = 1;
                btnSeat.BorderColor = BORDER_COLOR;
                btnSeat.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                btnSeat.ForeColor = Color.Black;
                btnSeat.Text = seat.Row + seat.Number; // Ví dụ: A1
                btnSeat.Tag = seat; // Lưu đối tượng DTO vào Tag để dùng lại khi Click
                // [FIX QUAN TRỌNG] Tắt word wrap để số 10 không rớt
                // Guna2Button không có WordWrap, nhưng ta có thể chỉnh Padding
                btnSeat.Padding = new Padding(0);
                // 4. Thiết lập màu sắc dựa trên Trạng thái và Loại ghế
                if (seat.Status == 1 || seat.Status == 2) // 1: Holding, 2: Sold
                {
                    btnSeat.FillColor = COLOR_SOLD;
                    btnSeat.Enabled = false; // Không cho click
                    btnSeat.Text = "X"; // Đánh dấu đã bán
                    btnSeat.ForeColor = Color.White;
                }
                else // Available
                {
                    // Màu theo loại ghế
                    switch (seat.SeatType)
                    {
                        case "VIP": btnSeat.FillColor = COLOR_VIP; break;
                        case "Sweetbox": btnSeat.FillColor = COLOR_SWEETBOX; break;
                        default: btnSeat.FillColor = COLOR_STANDARD; break;
                    }

                    // Gán sự kiện Click chọn ghế
                    btnSeat.Click += Seat_Click;
                }

                pnlSeatMap.Controls.Add(btnSeat);
            }

            // Tiếp tục vẽ
            pnlSeatMap.ResumeLayout();
        }

        // --- PHẦN 3: XỬ LÝ CHỌN GHẾ ---
        private void Seat_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna2Button;
            var seat = btn.Tag as SeatDTO;

            if (_selectedSeats.Contains(seat))
            {
                // Nếu đang chọn -> Bỏ chọn (Revert màu cũ)
                _selectedSeats.Remove(seat);

                // Trả lại màu gốc theo loại
                switch (seat.SeatType)
                {
                    case "VIP": btn.FillColor = COLOR_VIP; break;
                    case "Sweetbox": btn.FillColor = COLOR_SWEETBOX; break;
                    default: btn.FillColor = COLOR_STANDARD; break;
                }
                btn.ForeColor = Color.Black;
            }
            else
            {
                // Nếu chưa chọn -> Chọn (Đổi màu đỏ)
                // Kiểm tra giới hạn (Ví dụ: Max 8 ghế)
                if (_selectedSeats.Count >= 8)
                {
                    MessageBox.Show("Bạn chỉ được chọn tối đa 8 ghế.", "Thông báo");
                    return;
                }

                _selectedSeats.Add(seat);
                btn.FillColor = COLOR_SELECTING;
                btn.ForeColor = Color.White;
            }

            // Cập nhật lại panel bên phải
            UpdateSummaryPanel();
        }

        private void UpdateSummaryPanel()
        {
            // Hiển thị danh sách ghế: "A1, A2, B5"
            lblSeats.Text = string.Join(", ", _selectedSeats.Select(s => s.Row + s.Number));

            // Tính tổng tiền
            decimal total = _selectedSeats.Sum(s => s.Price);
            lblTotalPrice.Text = string.Format("{0:N0} đ", total); // Format 100,000 đ

            // Enable nút thanh toán nếu có ghế được chọn
            btnPayment.Enabled = _selectedSeats.Count > 0;
        }

        // --- PHẦN 4: LOGIC ĐẶT VÉ (GỌI API) ---
        private async void BtnPayment_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đăng nhập
            if (!SessionManager.Instance.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để tiếp tục.", "Yêu cầu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _mainForm.NavigateToLogin();
                return;
            }

            if (_selectedSeats.Count == 0) return;

            // 2. Chuẩn bị dữ liệu
            var requestDto = new CreateBookingRequestDTO
            {
                ShowtimeId = _showtimeId,
                SeatIds = _selectedSeats.Select(s => s.SeatId).ToList()
            };

            btnPayment.Text = "Đang xử lý...";
            btnPayment.Enabled = false;

            try
            {
                // 3. Gọi API tạo Booking
                // BaseApiService trả về dynamic (JSON object), ta lấy property "bookingId"
                dynamic response = await _bookingService.CreateBookingAsync(requestDto);

                if (response != null)
                {
                    // Lấy BookingId từ phản hồi JSON (Newtonsoft.Json JObject)
                    // Lưu ý: Tùy vào cách deserialize của RestSharp, response có thể là JObject hoặc Dictionary
                    // Ở đây giả định BaseService trả về đúng object
                    int bookingId = (int)response.bookingId; // Case-sensitive khớp với API trả về

                    MessageBox.Show("Đặt chỗ thành công! Chuyển sang thanh toán.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Chuyển sang bước Thanh toán (Sẽ làm ở module sau)
                    // Tạm thời hiển thị message
                    MessageBox.Show($"Booking ID: {bookingId}. Giả lập thanh toán ngay...");

                    // Gọi tiếp API thanh toán giả lập
                    await ProcessPayment(bookingId);
                }
            }
            catch (Exception ex)
            {
                // Lỗi nghiệp vụ (ví dụ: Ghế đã có người đặt) sẽ được BaseService hiện MessageBox
                // Tại đây ta chỉ cần reset nút bấm
                Console.WriteLine(ex.Message);
            }
            finally
            {
                btnPayment.Text = "THANH TOÁN";
                btnPayment.Enabled = true;
            }
        }

        private async Task ProcessPayment(int bookingId)
        {
            bool success = await _bookingService.ConfirmPaymentAsync(bookingId, "Momo");
            if (success)
            {
                MessageBox.Show("Thanh toán thành công! Vé đã được gửi vào tài khoản.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _mainForm.NavigateToHome(); // Quay về trang chủ
            }
        }
    }
}