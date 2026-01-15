using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using MovieBookingClient.UI.Modules;

namespace MovieBookingClient.UI.Forms.Admin
{
    public partial class UC_QuanLyPhong : UserControl
    {
        private readonly RoomService _roomService;

        // UI Components
        private Guna2Panel pnlContainer;
        private Guna2Panel pnlOverlay;
        private UC_AddRoom ucAddRoom;

        public UC_QuanLyPhong()
        {
            _roomService = new RoomService();
            InitializeComponent();
            SetupEvents();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1100, 700);
            this.BackColor = Color.FromArgb(242, 245, 250);

            // --- Tiêu đề ---
            var lblTitle = new Label
            {
                Text = "TRUNG TÂM QUẢN LÝ PHÒNG CHIẾU",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(50, 40)
            };
            var lblSubTitle = new Label
            {
                Text = "Chọn chức năng bên dưới để thao tác với hệ thống phòng chiếu",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(52, 80)
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSubTitle);

            // --- Panel chứa 2 nút chức năng ---
            pnlContainer = new Guna2Panel
            {
                Size = new Size(800, 300),
                Location = new Point((this.Width - 800) / 2, 150),
                BackColor = Color.Transparent
            };

            // Nút THÊM PHÒNG (Thiết kế dạng Card lớn)
            var btnAddCard = CreateCardButton(
                "THÊM PHÒNG MỚI",
                "Tạo phòng chiếu, thiết lập sơ đồ ghế và gán vào rạp.",
                Color.FromArgb(46, 204, 113),
                new Point(0, 0)
            );
            btnAddCard.Tag = "ADD"; // Đánh dấu để xử lý sự kiện

            // Nút XÓA PHÒNG
            var btnDeleteCard = CreateCardButton(
                "XÓA PHÒNG",
                "Xóa phòng chiếu khỏi hệ thống thông qua Mã số (ID).",
                Color.FromArgb(231, 76, 60),
                new Point(420, 0)
            );
            btnDeleteCard.Tag = "DELETE";

            pnlContainer.Controls.Add(btnAddCard);
            pnlContainer.Controls.Add(btnDeleteCard);
            this.Controls.Add(pnlContainer);

            // --- Overlay & Popup (Giữ nguyên logic cũ) ---
            pnlOverlay = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Visible = false,
                FillColor = Color.FromArgb(150, Color.Black) // Đậm hơn chút cho đẹp
            };

            ucAddRoom = new UC_AddRoom
            {
                Visible = false,
                BackColor = Color.White,
                Location = new Point((this.Width - 400) / 2, (this.Height - 350) / 2)
            };

            pnlOverlay.Controls.Add(ucAddRoom);
            this.Controls.Add(pnlOverlay);

            // Đảm bảo Overlay luôn trên cùng
            pnlOverlay.BringToFront();
        }

        // Hàm tạo nút dạng Card đẹp
        private Guna2Button CreateCardButton(string title, string desc, Color color, Point loc)
        {
            var btn = new Guna2Button
            {
                Size = new Size(380, 250),
                Location = loc,
                FillColor = Color.White,
                BorderRadius = 15,
                Cursor = Cursors.Hand,
                ShadowDecoration = { Enabled = true, Color = Color.Silver, Depth = 40, BorderRadius = 15 },
                Text = string.Empty // Xóa text mặc định để tự vẽ label
            };

            // Icon màu (Dùng Panel làm khối màu trang trí)
            var colorBlock = new Guna2Panel
            {
                Size = new Size(380, 10),
                Dock = DockStyle.Top,
                FillColor = color,
                BorderRadius = 15,
                CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges { BottomLeft = false, BottomRight = false }
            };

            var lblHeader = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(20, 40),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            var lblDesc = new Label
            {
                Text = desc,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(20, 80),
                Size = new Size(340, 100),
                BackColor = Color.Transparent
            };

            // Biểu tượng nút bấm giả ở dưới
            var lblAction = new Label
            {
                Text = "Nhấn để thực hiện →",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(20, 210),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            btn.Controls.Add(colorBlock);
            btn.Controls.Add(lblHeader);
            btn.Controls.Add(lblDesc);
            btn.Controls.Add(lblAction);

            // Truyền sự kiện click từ label con sang button cha
            foreach (Control c in btn.Controls)
            {
                c.Click += (s, e) => btn.PerformClick();
            }

            return btn;
        }

        private void SetupEvents()
        {
            // Tìm nút trong panel container
            foreach (Control c in pnlContainer.Controls)
            {
                if (c is Guna2Button btn)
                {
                    btn.Click += (s, e) =>
                    {
                        if (btn.Tag.ToString() == "ADD") OpenAddPopup();
                        else if (btn.Tag.ToString() == "DELETE") DeleteRoomProcess();
                    };
                }
            }

            // Logic Popup
            ucAddRoom.OnSave += async (req) => {
                this.Cursor = Cursors.WaitCursor;
                bool success = await _roomService.CreateRoomAsync(req);
                this.Cursor = Cursors.Default;

                if (success)
                {
                    MessageBox.Show("Thêm phòng thành công!", "Thông báo");
                    ClosePopup();
                }
                else
                {
                    MessageBox.Show("Lỗi: Không thể thêm phòng.", "Lỗi");
                }
            };

            ucAddRoom.OnCancel += ClosePopup;

            // Resize
            this.Resize += (s, e) => {
                if (pnlContainer != null)
                    pnlContainer.Location = new Point((this.Width - pnlContainer.Width) / 2, (this.Height - pnlContainer.Height) / 2);
                if (ucAddRoom != null)
                    ucAddRoom.Location = new Point((this.Width - ucAddRoom.Width) / 2, (this.Height - ucAddRoom.Height) / 2);
            };
        }

        private void OpenAddPopup()
        {
            ucAddRoom.ResetData();
            pnlOverlay.Visible = true;
            ucAddRoom.Visible = true;
            pnlOverlay.BringToFront();
        }

        private async void DeleteRoomProcess()
        {
            string input = ShowInputDialog("Nhập Mã số (ID) phòng cần xóa:", "Xóa Phòng");
            if (int.TryParse(input, out int id))
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng ID: {id}?\nToàn bộ ghế và lịch chiếu liên quan sẽ bị xóa!", "Cảnh báo nguy hiểm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    bool ok = await _roomService.DeleteRoomAsync(id);
                    this.Cursor = Cursors.Default;

                    if (ok) MessageBox.Show("Đã xóa phòng thành công!", "Thông báo");
                    else MessageBox.Show("Xóa thất bại! (ID không tồn tại)", "Lỗi");
                }
            }
        }

        private void ClosePopup()
        {
            pnlOverlay.Visible = false;
            ucAddRoom.Visible = false;
        }

        // Hộp thoại nhập ID đẹp hơn chút
        private string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            Label textLabel = new Label() { Left = 30, Top = 20, Text = text, AutoSize = true, Font = new Font("Segoe UI", 11) };
            Guna2TextBox textBox = new Guna2TextBox() { Left = 30, Top = 60, Width = 320, Height = 40, BorderRadius = 5, Font = new Font("Segoe UI", 11) };
            Guna2Button confirmation = new Guna2Button() { Text = "Xác nhận", Left = 230, Width = 120, Top = 110, Height = 40, BorderRadius = 5, FillColor = Color.FromArgb(231, 76, 60), DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}