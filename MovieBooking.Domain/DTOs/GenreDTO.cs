namespace MovieBooking.Domain.DTOs
{
    public class GenreDTO
    {
        public int GenreId { get; set; }  // Có thể là Id hoặc GenreId, hãy kiểm tra kỹ
        public required string GenreName { get; set; }

        // --- THÊM ĐOẠN NÀY VÀO ---
        // Hàm này bắt buộc đối tượng phải hiển thị Tên khi đưa vào ListBox
        public override string ToString()
        {
            return GenreName; // Nếu thuộc tính của bạn là "Name" thì đổi thành "return Name;"
        }
        // --------------------------
    }
}