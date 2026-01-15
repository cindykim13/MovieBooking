using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.Forms.Admin
{
    public class FrmThemPhim : Form
    {
        private readonly MovieService _movieService;
        private readonly int? _movieId; // Biến lưu ID phim (nếu đang sửa)

        // Controls
        private TextBox txtTieuDe, txtDaoDien, txtThoiLuong, txtNamSX, txtMoTa;
        private ComboBox cboTrangThai;
        private CheckedListBox clbGenres;
        private Button btnLuu, btnHuy;

        // Constructor nhận ID (mặc định null = Thêm mới)
        public FrmThemPhim(int? movieId = null)
        {
            InitializeComponent();
            _movieService = new MovieService();
            _movieId = movieId;

            // Đổi tiêu đề Form tùy theo chế độ
            if (_movieId.HasValue)
            {
                this.Text = "Cập Nhật Phim";
                // Tìm Label header và đổi text (dựa vào vị trí đã add trong InitializeComponent)
                foreach (Control c in this.Controls)
                {
                    if (c is Label lbl && lbl.Font.Size == 16)
                    {
                        lbl.Text = "CẬP NHẬT PHIM";
                        break;
                    }
                }
            }

            this.Load += FrmThemPhim_Load;
        }

        private async void FrmThemPhim_Load(object sender, EventArgs e)
        {
            // 1. Tải danh sách thể loại trước
            await LoadGenresAsync();

            // 2. Nếu đang sửa, tải thông tin phim lên form
            if (_movieId.HasValue)
            {
                await LoadMovieDataAsync(_movieId.Value);
            }
        }

        private async Task LoadGenresAsync()
        {
            try
            {
                var genres = await _movieService.GetAllGenresAsync();
                if (genres != null && genres.Count > 0)
                {
                    clbGenres.Items.Clear();
                    foreach (var genre in genres)
                    {
                        clbGenres.Items.Add(genre); // genre.ToString() sẽ hiển thị tên
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thể loại: " + ex.Message);
            }
        }

        // Hàm mới: Tải dữ liệu phim để sửa
        private async Task LoadMovieDataAsync(int id)
        {
            try
            {
                var movie = await _movieService.GetMovieDetailAsync(id);
                if (movie != null)
                {
                    txtTieuDe.Text = movie.Title;
                    txtDaoDien.Text = movie.Director;
                    txtThoiLuong.Text = movie.Duration.ToString();
                    txtNamSX.Text = movie.ReleaseYear.ToString();
                    txtMoTa.Text = movie.StoryLine; // Mapping StoryLine -> Mô tả
                    cboTrangThai.SelectedItem = movie.Status;

                    // Tự động check các thể loại
                    if (movie.Genres != null)
                    {
                        for (int i = 0; i < clbGenres.Items.Count; i++)
                        {
                            var item = (GenreDTO)clbGenres.Items[i];
                            // So sánh tên thể loại (Case insensitive)
                            if (movie.Genres.Any(g => g.Equals(item.GenreName, StringComparison.OrdinalIgnoreCase)))
                            {
                                clbGenres.SetItemChecked(i, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải thông tin phim: " + ex.Message);
                this.Close();
            }
        }

        private async void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Validate
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTieuDe.Focus();
                return;
            }

            if (clbGenres.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một thể loại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // UI Feedback
                btnLuu.Enabled = false;
                btnLuu.Text = "Đang xử lý...";

                // 2. Chuẩn bị dữ liệu chung
                var selectedGenresObjects = clbGenres.CheckedItems.Cast<GenreDTO>().ToList();
                List<string> genreNames = selectedGenresObjects.Select(g => g.GenreName).ToList();

                int duration = int.TryParse(txtThoiLuong.Text, out int d) ? d : 0;
                int year = int.TryParse(txtNamSX.Text, out int y) ? y : DateTime.Now.Year;
                string status = cboTrangThai.SelectedItem?.ToString() ?? "Available";

                bool isSuccess = false;

                // 3. Phân nhánh: CẬP NHẬT hay THÊM MỚI
                if (_movieId.HasValue)
                {
                    // --- LOGIC CẬP NHẬT ---
                    var updateDto = new UpdateMovieRequestDTO
                    {
                        Title = txtTieuDe.Text.Trim(),
                        StoryLine = txtMoTa.Text.Trim(),
                        Duration = duration,
                        ReleaseYear = year,
                        Rating = 0, // Giữ nguyên hoặc xử lý logic rating riêng
                        PosterUrl = "", // Có thể thêm TextBox Poster nếu cần
                        // Lưu ý: Nếu API Update của bạn cần Genre/Status thì thêm vào DTO
                    };

                    // Nếu DTO Update của bạn chưa có Status/Genres, bạn cần bổ sung vào Class DTO bên Backend
                    // Ở đây tôi giả định UpdateMovieRequestDTO khớp với code Backend đã gửi trước đó.

                    isSuccess = await _movieService.UpdateMovieAsync(_movieId.Value, updateDto);
                }
                else
                {
                    // --- LOGIC THÊM MỚI ---
                    var newMovie = new MovieDTO
                    {
                        Title = txtTieuDe.Text.Trim(),
                        Director = txtDaoDien.Text.Trim(),
                        Description = txtMoTa.Text.Trim(),
                        Duration = duration,
                        ReleaseYear = year,
                        Status = status,
                        GenreId = selectedGenresObjects.FirstOrDefault()?.GenreId,
                        Genres = genreNames
                    };

                    isSuccess = await _movieService.CreateMovieAsync(newMovie);
                }

                // 4. Xử lý kết quả
                if (isSuccess)
                {
                    string msg = _movieId.HasValue ? "Cập nhật thành công!" : "Thêm phim thành công!";
                    MessageBox.Show("✅ " + msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi hệ thống: {ex.Message}", "Error");
            }
            finally
            {
                if (!this.IsDisposed)
                {
                    btnLuu.Enabled = true;
                    btnLuu.Text = _movieId.HasValue ? "Cập Nhật" : "Lưu Phim";
                }
            }
        }

        // (Phần InitializeComponent giữ nguyên như cũ, chỉ sửa Text nút btnLuu trong logic click)
        private void InitializeComponent()
        {
            this.Size = new Size(550, 750);
            this.Text = "Thêm Phim Mới";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            int y = 20;

            // Header
            Label lblHeader = new Label
            {
                Text = "THÊM PHIM MỚI",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(550, 40),
                Location = new Point(0, y)
            };
            this.Controls.Add(lblHeader);
            y += 50;

            // Inputs
            AddInput("Tên Phim (*):", ref txtTieuDe, ref y);
            AddInput("Đạo Diễn:", ref txtDaoDien, ref y);
            AddInput("Thời Lượng (Phút):", ref txtThoiLuong, ref y);
            AddInput("Năm Sản Xuất:", ref txtNamSX, ref y);

            // Genres CheckBoxList
            Label lblGenres = new Label { Text = "Chọn Thể Loại (*):", Location = new Point(40, y), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            clbGenres = new CheckedListBox
            {
                Location = new Point(40, y + 25),
                Width = 450,
                Height = 80,
                BorderStyle = BorderStyle.FixedSingle,
                CheckOnClick = true,
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(lblGenres);
            this.Controls.Add(clbGenres);
            y += 115;

            // Mô tả
            Label lblMoTa = new Label { Text = "Mô Tả:", Location = new Point(40, y), AutoSize = true, Font = new Font("Segoe UI", 10) };
            txtMoTa = new TextBox
            {
                Location = new Point(40, y + 25),
                Width = 450,
                Height = 60,
                Multiline = true,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(lblMoTa);
            this.Controls.Add(txtMoTa);
            y += 100;

            // Trạng thái
            Label lblStatus = new Label { Text = "Trạng Thái:", Location = new Point(40, y), AutoSize = true, Font = new Font("Segoe UI", 10) };
            cboTrangThai = new ComboBox
            {
                Location = new Point(40, y + 25),
                Width = 450,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            cboTrangThai.Items.AddRange(new object[] { "Available", "Coming Soon", "Stopped" });
            cboTrangThai.SelectedIndex = 0;
            this.Controls.Add(lblStatus);
            this.Controls.Add(cboTrangThai);
            y += 80;

            // Buttons
            btnLuu = new Button { Text = "Lưu Phim", BackColor = Color.SeaGreen, ForeColor = Color.White, Size = new Size(130, 45), Location = new Point(120, y), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnLuu.Click += btnLuu_Click;

            btnHuy = new Button { Text = "Hủy Bỏ", BackColor = Color.IndianRed, ForeColor = Color.White, Size = new Size(130, 45), Location = new Point(280, y), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            btnHuy.Click += (s, e) => this.Close();

            this.Controls.Add(btnLuu);
            this.Controls.Add(btnHuy);
        }

        private void AddInput(string label, ref TextBox txt, ref int y)
        {
            Label lbl = new Label { Text = label, Location = new Point(40, y), AutoSize = true, Font = new Font("Segoe UI", 10) };
            txt = new TextBox { Location = new Point(40, y + 25), Width = 450, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(lbl);
            this.Controls.Add(txt);
            y += 65;
        }
    }
}