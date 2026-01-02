namespace MovieBookingClient.Configs
{
    public static class AppSettings
    {
        // --- CHẾ ĐỘ PHÁT TRIỂN (DEVELOPMENT) ---
        // Bỏ comment dòng dưới khi chạy Local trên máy tính
        public const string BaseApiUrl = "https://localhost:7034";

        // --- CHẾ ĐỘ SẢN PHẨM (PRODUCTION) ---
        // Bỏ comment dòng dưới khi muốn kết nối tới Server thật trên Render
        // public const string BaseApiUrl = "https://movie-booking-api.onrender.com";
    }
}