using MovieBooking.Domain.DTOs;
using MovieBookingAPI.Models.DTOs;
using MovieBookingClient.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_AddRoom : UserControl
    {
        public event EventHandler OnSaved;
        public event EventHandler OnCancelled;

        private readonly AdminRoomService _adminRoomService;
        private readonly CinemaService _cinemaService;

        // [SỬA LỖI]: Không cần lưu chuỗi JSON nữa, ta sẽ dùng List<SeatDefinitionDto>
        private List<SeatDefinitionDTO> _selectedLayout = new List<SeatDefinitionDTO>();

        public UC_AddRoom()
        {
            InitializeComponent();
            _adminRoomService = new AdminRoomService();
            _cinemaService = new CinemaService();

            this.Load += async (s, e) => await InitializeForm();
            btnSave.Click += async (s, e) => await SaveRoom();
            btnCancel.Click += (s, e) => OnCancelled?.Invoke(this, EventArgs.Empty);
            cboTemplate.SelectedIndexChanged += (s, e) => GenerateSeatPreview();
        }


        private async Task InitializeForm()
        {
            await LoadCinemas();
            await LoadRoomTemplates();
        }
        private async Task LoadCinemas()
        {
            try
            {
                var cinemas = await _cinemaService.GetAllCinemasAsync();
                cboCinema.DataSource = cinemas;
                cboCinema.DisplayMember = "Name";
                cboCinema.ValueMember = "CinemaId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách rạp: " + ex.Message);
            }
        }
        private async Task LoadRoomTemplates()
        {
            try
            {
                var templates = await _adminRoomService.GetRoomTemplatesAsync();
                cboTemplate.DataSource = templates;
                cboTemplate.DisplayMember = "TemplateName";
                cboTemplate.ValueMember = "TemplateId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách mẫu phòng: " + ex.Message);
            }
        }

        private void GenerateSeatPreview()
        {
            pnlSeatPreview.Controls.Clear();
            _selectedLayout.Clear(); // Dọn dẹp layout cũ

            var selectedTemplate = cboTemplate.SelectedItem as RoomTemplateDTO;
            // [SỬA LỖI]: Kiểm tra thuộc tính 'Seats' thay vì 'LayoutJson'
            if (selectedTemplate == null || selectedTemplate.Seats == null || !selectedTemplate.Seats.Any())
            {
                return;
            }

            // [SỬA LỖI]: Gán trực tiếp danh sách ghế vào biến state
            _selectedLayout = selectedTemplate.Seats;

            // Parse chuỗi JSON thành danh sách các đối tượng Seat
            try
            {
                foreach (var seat in _selectedLayout)
                {
                    var btnSeat = new Guna.UI2.WinForms.Guna2Button();
                    btnSeat.Size = new Size(30, 30); // Kích thước nhỏ để preview

                    // Tính toán vị trí
                    int x = 10 + (seat.GridColumn - 1) * 35;
                    int y = 10 + (seat.GridRow - 1) * 35;
                    btnSeat.Location = new Point(x, y);

                    char rowChar = (char)(64 + seat.GridRow);
                    btnSeat.Text = $"{rowChar}{seat.Number}";

                    // Gán màu theo TypeId
                    if (seat.TypeId == 2) btnSeat.FillColor = Color.MistyRose; // VIP
                    else if (seat.TypeId == 3) btnSeat.FillColor = Color.Pink; // Sweetbox
                    else btnSeat.FillColor = Color.White;

                    pnlSeatPreview.Controls.Add(btnSeat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc dữ liệu layout ghế: " + ex.Message);
            }
        }

        private async Task SaveRoom()
        {
            if (cboCinema.SelectedValue == null || string.IsNullOrWhiteSpace(txtRoomName.Text) || !_selectedLayout.Any())
            {
                MessageBox.Show("Vui lòng điền đủ thông tin và chọn một mẫu phòng.", "Thiếu thông tin");
                return;
            }

            var dto = new CreateRoomRequestDTO
            {
                CinemaId = (int)cboCinema.SelectedValue,
                Name = txtRoomName.Text.Trim(),

                // [SỬA LỖI]: Gán danh sách Seats thay vì SeatsJson
                Seats = _selectedLayout
            };

            try
            {
                await _adminRoomService.CreateRoomAsync(dto);
                MessageBox.Show("Thêm phòng chiếu thành công!");
                OnSaved?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phòng: " + ex.Message);
            }
        }
    }
}