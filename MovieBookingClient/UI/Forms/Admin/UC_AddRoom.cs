using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;

namespace MovieBookingClient.UI.Modules
{
    public class UC_AddRoom : UserControl
    {
        public event Action<CreateRoomRequestDTO> OnSave;
        public event Action OnCancel;

        private readonly CinemaService _cinemaService;
        private TextBox txtTenPhong;
        private ComboBox cboRap;
        private NumericUpDown numHang, numGhe;
        private Button btnLuu, btnHuy;

        public UC_AddRoom()
        {
            InitializeCustomComponent(); // Vẽ giao diện
            _cinemaService = new CinemaService();
            this.Load += async (s, e) => await LoadCinemas();
        }

        private async Task LoadCinemas()
        {
            try
            {
                var list = await _cinemaService.GetAllCinemasAsync();
                if (list != null)
                {
                    cboRap.DataSource = list;
                    cboRap.DisplayMember = "Name";
                    cboRap.ValueMember = "Id";
                }
            }
            catch { /* Bỏ qua lỗi kết nối */ }
        }

        public void ResetData()
        {
            txtTenPhong.Text = "";
            numHang.Value = 10;
            numGhe.Value = 12;
            if (cboRap.Items.Count > 0) cboRap.SelectedIndex = 0;
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(400, 350);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            int x = 30, y = 30, gap = 50;
            this.Controls.Add(new Label { Text = "THÊM PHÒNG MỚI", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(100, 10), AutoSize = true, ForeColor = Color.Navy });

            y += 40;
            this.Controls.Add(new Label { Text = "Tên phòng:", Location = new Point(x, y), AutoSize = true });
            txtTenPhong = new TextBox { Location = new Point(x + 100, y - 3), Width = 200 };
            this.Controls.Add(txtTenPhong);

            y += gap;
            this.Controls.Add(new Label { Text = "Rạp chiếu:", Location = new Point(x, y), AutoSize = true });
            cboRap = new ComboBox { Location = new Point(x + 100, y - 3), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            this.Controls.Add(cboRap);

            y += gap;
            this.Controls.Add(new Label { Text = "Số hàng:", Location = new Point(x, y), AutoSize = true });
            numHang = new NumericUpDown { Location = new Point(x + 100, y - 3), Width = 60, Minimum = 1, Value = 10 };
            this.Controls.Add(numHang);

            this.Controls.Add(new Label { Text = "Ghế/Hàng:", Location = new Point(x + 180, y), AutoSize = true });
            numGhe = new NumericUpDown { Location = new Point(x + 250, y - 3), Width = 60, Minimum = 1, Value = 12 };
            this.Controls.Add(numGhe);

            y += gap + 20;
            btnLuu = new Button { Text = "LƯU", Location = new Point(80, y), Size = new Size(100, 35), BackColor = Color.Green, ForeColor = Color.White };
            btnLuu.Click += (s, e) => {
                if (cboRap.SelectedValue == null) return;
                var req = new CreateRoomRequestDTO
                {
                    Name = txtTenPhong.Text,
                    CinemaId = (int)cboRap.SelectedValue,
                    NumberOfRows = (int)numHang.Value,
                    SeatsPerRow = (int)numGhe.Value
                };
                OnSave?.Invoke(req);
            };
            this.Controls.Add(btnLuu);

            btnHuy = new Button { Text = "HỦY", Location = new Point(200, y), Size = new Size(100, 35), BackColor = Color.Red, ForeColor = Color.White };
            btnHuy.Click += (s, e) => OnCancel?.Invoke();
            this.Controls.Add(btnHuy);
        }
    }
}