using MovieBookingClient.Forms.Customer;

namespace MovieBookingClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Khởi chạy FrmMain
            Application.Run(new FrmMain());
        }
    }
}