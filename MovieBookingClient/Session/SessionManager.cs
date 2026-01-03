namespace MovieBookingClient.Session
{
    public class SessionManager
    {
        private static SessionManager? _instance;
        // Cơ chế Singleton: Đảm bảo chỉ có 1 instance duy nhất tồn tại
        public static SessionManager Instance => _instance ??= new SessionManager();

        // [FIX CẢNH BÁO]: Cho phép nullable (string?) vì khi chưa đăng nhập, các giá trị này là null
        public string? AccessToken { get; private set; }
        public string? Username { get; private set; }
        public string? Role { get; private set; }

        private SessionManager() { }

        // Hàm khởi tạo phiên làm việc sau khi đăng nhập thành công
        public void StartSession(string token, string username, string role)
        {
            AccessToken = token;
            Username = username;
            Role = role;
        }

        // Hàm xóa phiên làm việc (Đăng xuất)
        public void EndSession()
        {
            AccessToken = null;
            Username = null;
            Role = null;
        }

        // Kiểm tra xem người dùng đã đăng nhập chưa
        public bool IsLoggedIn => !string.IsNullOrEmpty(AccessToken);
    }
}