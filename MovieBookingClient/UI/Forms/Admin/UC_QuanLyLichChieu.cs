using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using MovieBookingClient.UI.Modules; // Đảm bảo dòng này có

namespace MovieBookingClient.UI.Forms.Admin
{
    public partial class FrmQuanLyLichChieu : UserControl
    {
        private readonly ShowtimeService _showtimeService;

        // Khai báo Controls
        private Guna2Panel pnlHeader;
        private Guna2DataGridView dgvLichChieu;
        private Guna2Button btnThem, btnSua, btnXoa, btnXem;
        private Guna2DateTimePicker dtpNgayChieu;
        private Label lblTitle, lblChonNgay;

        public FrmQuanLyLichChieu()
        {
            _showtimeService = new ShowtimeService();
            InitializeComponent();
            dtpNgayChieu.Value = DateTime.Now;
            SetupEvents();
            this.Load += async (s, e) => await LoadData();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1100, 700);
            this.BackColor = Color.FromArgb(242, 245, 250);

            // --- Header ---
            pnlHeader = new Guna2Panel { Dock = DockStyle.Top, Height = 80, FillColor = Color.White };

            lblTitle = new Label
            {
                Text = "QUẢN LÝ LỊCH CHIẾU",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 25),
                AutoSize = true
            };

            lblChonNgay = new Label { Text = "Ngày chiếu:", Location = new Point(300, 30), AutoSize = true, Font = new Font("Segoe UI", 10) };

            dtpNgayChieu = new Guna2DateTimePicker
            {
                Location = new Point(390, 20),
                Width = 200,
                Height = 36,
                Format = DateTimePickerFormat.Short,
                FillColor = Color.White,
                BorderRadius = 5,
                BorderThickness = 1,
                BorderColor = Color.Gray
            };

            btnXem = new Guna2Button { Text = "XEM", FillColor = Color.FromArgb(100, 88, 255), Location = new Point(600, 20), Size = new Size(80, 36), BorderRadius = 5 };
            btnThem = new Guna2Button { Text = "THÊM", FillColor = Color.FromArgb(46, 204, 113), Location = new Point(720, 20), Size = new Size(100, 36), BorderRadius = 5 };
            btnSua = new Guna2Button { Text = "SỬA", FillColor = Color.FromArgb(52, 152, 219), Location = new Point(830, 20), Size = new Size(100, 36), BorderRadius = 5 };
            btnXoa = new Guna2Button { Text = "XÓA", FillColor = Color.FromArgb(231, 76, 60), Location = new Point(940, 20), Size = new Size(100, 36), BorderRadius = 5 };

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblChonNgay, dtpNgayChieu, btnXem, btnThem, btnSua, btnXoa });

            // --- Grid ---
            dgvLichChieu = new Guna2DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                ThemeStyle = {
                    HeaderStyle = { BackColor = Color.FromArgb(100, 88, 255), ForeColor = Color.White, Height = 40, Font = new Font("Segoe UI", 10, FontStyle.Bold) },
                    RowsStyle = { Font = new Font("Segoe UI", 10), Height = 30 }
                },
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };

            this.Controls.Add(dgvLichChieu);
            this.Controls.Add(pnlHeader);
        }

        private void SetupEvents()
        {
            btnXem.Click += async (s, e) => await LoadData();
            dtpNgayChieu.ValueChanged += async (s, e) => await LoadData();
            btnThem.Click += (s, e) => ShowAddEdit(null);

            btnSua.Click += (s, e) => {
                if (dgvLichChieu.CurrentRow == null) return;
                int id = Convert.ToInt32(dgvLichChieu.CurrentRow.Cells["ShowtimeId"].Value);
                ShowAddEdit(id);
            };

            btnXoa.Click += async (s, e) => {
                if (dgvLichChieu.CurrentRow == null) return;
                int id = Convert.ToInt32(dgvLichChieu.CurrentRow.Cells["ShowtimeId"].Value);
                // Dùng MovieTitle khớp với ShowtimeDTO.cs
                string title = dgvLichChieu.CurrentRow.Cells["MovieTitle"].Value?.ToString();

                if (MessageBox.Show($"Xóa lịch chiếu phim '{title}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool ok = await _showtimeService.DeleteShowtimeAsync(id);
                    if (ok) await LoadData(); else MessageBox.Show("Xóa thất bại!");
                }
            };
        }

        private async Task LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var list = await _showtimeService.GetShowtimesByDateAsync(dtpNgayChieu.Value);
                dgvLichChieu.DataSource = null;
                dgvLichChieu.DataSource = list;
                FormatColumns();
            }
            catch { /* Ignore */ }
            finally { this.Cursor = Cursors.Default; }
        }

        private void FormatColumns()
        {
            if (dgvLichChieu.Columns.Count == 0) return;
            string[] hide = { "ShowtimeId", "MovieId", "RoomId", "CinemaId", "Status", "EndTime" };
            foreach (var col in hide) if (dgvLichChieu.Columns.Contains(col)) dgvLichChieu.Columns[col].Visible = false;

            // Khớp với ShowtimeDTO.cs bạn gửi
            if (dgvLichChieu.Columns.Contains("MovieTitle")) dgvLichChieu.Columns["MovieTitle"].HeaderText = "Tên Phim";
            if (dgvLichChieu.Columns.Contains("RoomName")) dgvLichChieu.Columns["RoomName"].HeaderText = "Phòng";
            if (dgvLichChieu.Columns.Contains("CinemaName")) dgvLichChieu.Columns["CinemaName"].HeaderText = "Rạp";
            if (dgvLichChieu.Columns.Contains("StartTime"))
            {
                dgvLichChieu.Columns["StartTime"].HeaderText = "Giờ Chiếu";
                dgvLichChieu.Columns["StartTime"].DefaultCellStyle.Format = "HH:mm";
            }
            if (dgvLichChieu.Columns.Contains("Price"))
            {
                dgvLichChieu.Columns["Price"].HeaderText = "Giá Vé";
                dgvLichChieu.Columns["Price"].DefaultCellStyle.Format = "N0";
            }
        }

        private void ShowAddEdit(int? id)
        {
            var uc = new UcThemSuaLichChieu(id);
            uc.Dock = DockStyle.Fill;
            uc.OnSaved += async () => { this.Controls.Remove(uc); ShowGrid(true); await LoadData(); };
            uc.OnHuy += () => { this.Controls.Remove(uc); ShowGrid(true); };

            ShowGrid(false);
            this.Controls.Add(uc);
            uc.BringToFront();
        }

        private void ShowGrid(bool show)
        {
            pnlHeader.Visible = show;
            dgvLichChieu.Visible = show;
        }
    }
}