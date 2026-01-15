namespace MovieBookingClient.Global
{
    public static class UserSession
    {
        // Biến toàn cục lưu Token và thông tin User
        public static string AccessToken { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty; // "Admin" hoặc "Customer"

        // Kiểm tra xem đã đăng nhập chưa
        public static bool IsLoggedIn => !string.IsNullOrEmpty(AccessToken);

        // Hàm đăng xuất
        public static void Logout()
        {
            AccessToken = "";
            CurrentUsername = "";
            CurrentRole = "";
        }
    }
}