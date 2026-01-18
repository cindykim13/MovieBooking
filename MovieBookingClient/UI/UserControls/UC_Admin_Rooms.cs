using MovieBooking.Domain.DTOs;
using MovieBookingClient.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieBookingClient.UI.UserControls.Admin
{
    public partial class UC_Admin_Rooms : UserControl
    {
        private readonly CinemaService _cinemaService;
        private readonly AdminRoomService _adminRoomService;

        public UC_Admin_Rooms()
        {
            InitializeComponent();
            _cinemaService = new CinemaService();
            _adminRoomService = new AdminRoomService();

            this.Load += async (s, e) => await InitForm();
            cboCinemaFilter.SelectedIndexChanged += async (s, e) => await LoadRooms();

            // Gán sự kiện cho các nút chức năng
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += async (s, e) => await BtnDelete_Click();
        }

        private async Task InitForm()
        {
            await LoadCinemasFilter();
            await LoadRooms();
        }

        private async Task LoadCinemasFilter()
        {
            try
            {
                var cinemas = await _cinemaService.GetAllCinemasAsync();
                cinemas.Insert(0, new CinemaDTO { CinemaId = 0, Name = "Tất cả rạp" });

                cboCinemaFilter.DataSource = cinemas;
                cboCinemaFilter.DisplayMember = "Name";
                cboCinemaFilter.ValueMember = "CinemaId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách rạp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadRooms()
        {
            try
            {
                int? selectedCinemaId = null;
                if (cboCinemaFilter.SelectedValue is int id && id > 0)
                {
                    selectedCinemaId = id;
                }

                var rooms = await _adminRoomService.GetRoomsAsync(selectedCinemaId);

                dgvRooms.DataSource = rooms;
                FormatColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatColumns()
        {
            if (dgvRooms.Columns.Count == 0) return;

            dgvRooms.Columns["RoomId"].HeaderText = "ID Phòng";
            dgvRooms.Columns["Name"].HeaderText = "Tên Phòng";
            dgvRooms.Columns["TotalSeats"].HeaderText = "Tổng số ghế";

            // Có thể thêm cột Tên Rạp nếu cần, bằng cách JOIN ở Backend
        }

        // --- HÀM XỬ LÝ SỰ KIỆN NÚT BẤM ---

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ShowAddEditForm();
        }

        private async Task BtnDelete_Click()
        {
            if (dgvRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int roomId = Convert.ToInt32(dgvRooms.SelectedRows[0].Cells["RoomId"].Value);
            string roomName = dgvRooms.SelectedRows[0].Cells["Name"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa phòng '{roomName}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool success = await _adminRoomService.DeleteRoomAsync(roomId);
                    if (success)
                    {
                        MessageBox.Show("Xóa phòng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadRooms(); // Tải lại danh sách
                    }
                }
                catch (Exception ex)
                {
                    // Lỗi từ BaseApiService (404, 409...) sẽ được hiển thị
                }
            }
        }

        // --- HÀM QUẢN LÝ HIỂN THỊ ---

        private void ShowAddEditForm()
        {
            var ucAddRoom = new UC_AddRoom();
            ucAddRoom.Dock = DockStyle.Fill;

            // Đăng ký sự kiện
            ucAddRoom.OnSaved += async (s, e) => {
                this.Controls.Remove(ucAddRoom);
                ShowGridAndToolbar(true);
                await LoadRooms();
            };

            ucAddRoom.OnCancelled += (s, e) => {
                this.Controls.Remove(ucAddRoom);
                ShowGridAndToolbar(true);
            };

            // Ẩn lưới và hiện form
            ShowGridAndToolbar(false);
            this.Controls.Add(ucAddRoom);
            ucAddRoom.BringToFront();
        }

        private void ShowGridAndToolbar(bool show)
        {
            pnlHeader.Visible = show;
            dgvRooms.Visible = show;
        }
    }
}