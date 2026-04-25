# Báo cáo Khoa học: Hệ thống Quản lý và Đặt vé Xem phim (MovieBooking System)

## Tóm tắt (Abstract)
Dự án **MovieBooking System** là một hệ thống phần mềm toàn diện phục vụ công tác quản lý rạp chiếu phim và đặt vé trực tuyến. Hệ thống được thiết kế dựa trên kiến trúc hướng dịch vụ (Service-Oriented Architecture - SOA) kết hợp với mô hình ứng dụng phân tán, đảm bảo tính mở rộng, bảo mật và hiệu năng cao. Hệ thống bao gồm một Backend API mạnh mẽ xây dựng trên nền tảng .NET 8 (C#) và cơ sở dữ liệu PostgreSQL, cùng với Frontend là ứng dụng Desktop (Windows Forms) dành cho quản trị viên và nhân viên phòng vé.

## 1. Giới thiệu tổng quan (Introduction)
Trong bối cảnh chuyển đổi số mạnh mẽ, việc tối ưu hóa quy trình quản lý rạp chiếu phim là vô cùng cấp thiết. Dự án MovieBooking giải quyết bài toán cốt lõi trong việc số hóa dữ liệu phim, quản lý suất chiếu, phòng chiếu, quản lý rạp phim và đặc biệt là quy trình bán vé (Booking). 

Hệ thống phân quyền rõ ràng với cơ chế bảo mật xác thực bằng JWT (JSON Web Token), cho phép các vai trò khác nhau (Khách hàng, Nhân viên, Quản trị viên) tương tác với hệ thống một cách an toàn và minh bạch.

## 2. Kiến trúc Hệ thống (System Architecture)
Dự án được xây dựng dựa trên kiến trúc 3 lớp (3-Tier Architecture) kết hợp với các nguyên lý mã sạch (Clean Architecture & Clean Code).

### 2.1. Các thành phần chính (Components)
- **MovieBookingAPI**: Thành phần Web API đóng vai trò là Backend Server, giao tiếp thông qua RESTful API. Ứng dụng được triển khai các middleware về CORS, JWT Authentication và cung cấp tài liệu tự động qua Swagger UI.
- **MovieBookingClient**: Giao diện người dùng dưới dạng Desktop Application (Windows Forms .NET 8), giao tiếp với Backend thông qua các luồng gọi HTTP API an toàn.
- **MovieBooking.Domain**: Lớp thư viện (Class Library) dùng chung (Shared Domain), chứa toàn bộ các Entities, Models và DTOs. Việc tách biệt lớp này giúp đồng bộ hóa logic và cấu trúc dữ liệu giữa cả API và Client.

### 2.2. Tổ chức mã nguồn API (Code Structure)
Mã nguồn phía API được tổ chức tuân thủ mẫu thiết kế Controller - BUS - DAO nhằm phân tách rõ ràng trách nhiệm của từng lớp (Separation of Concerns):
- **Controllers**: Lớp giao tiếp với bên ngoài, nhận HTTP Request từ Client, gọi xuống lớp BUS và định dạng kết quả trả về HTTP Response.
- **BUS (Business Logic Layer)**: Lớp dịch vụ chứa đựng toàn bộ quy tắc nghiệp vụ (Business Rules), chịu trách nhiệm kiểm tra tính hợp lệ của dữ liệu trước khi lưu trữ hoặc cập nhật.
- **DAO (Data Access Object)**: Lớp giao tiếp trực tiếp với cơ sở dữ liệu sử dụng ORM Entity Framework Core, đảm nhận vai trò thực thi các câu lệnh truy vấn.
- **Data**: Chứa `AppDbContext`, thực hiện cấu hình kết nối và ánh xạ các thực thể (Entities) xuống cơ sở dữ liệu PostgreSQL.

## 3. Tổ chức Dữ liệu (Database Design)
Cơ sở dữ liệu được vận hành trên nền tảng **PostgreSQL**. Toàn bộ cấu trúc bảng và ràng buộc dữ liệu được sao lưu trong tệp tin `postgresql.sql`. Các module quản trị dữ liệu cốt lõi bao gồm:

- **Quản lý Người dùng & Phân quyền (Auth & Users)**: Quản trị tài khoản, mã hóa mật khẩu, và quyền hạn truy cập hệ thống.
- **Quản lý Rạp & Phòng chiếu (Cinemas & Rooms)**: Quản lý chi nhánh rạp chiếu, sơ đồ từng phòng chiếu cùng cấu hình loại ghế (Ghế thường, VIP, Couple).
- **Quản lý Phim & Suất chiếu (Movies & Showtimes)**: Lưu trữ metadata của phim (Tên, Thể loại, Đạo diễn, Diễn viên, Thời lượng, Nhãn phân loại độ tuổi) và điều phối lịch chiếu theo không gian, thời gian.
- **Quản lý Đặt vé (Bookings)**: Ghi nhận quy trình bán vé khép kín, bao gồm đặt chỗ, thanh toán và phát hành vé điện tử.

> **Lưu ý**: Hệ thống có đính kèm một số tài nguyên dữ liệu mẫu để hỗ trợ kiểm thử và khởi tạo ban đầu, được tổ chức sạch sẽ trong thư mục `SeedData/` (bao gồm tệp JSON mẫu và dữ liệu CSV tổng hợp).

## 4. Hướng dẫn Cài đặt & Triển khai (Deployment Guide)
Hệ thống được thiết kế sẵn sàng cho môi trường chứa hóa (Containerization) với Docker, đảm bảo tính nhất quán trên mọi môi trường phát triển và triển khai thực tế.

### Yêu cầu hệ thống:
- .NET 8.0 SDK
- Hệ quản trị CSDL PostgreSQL
- Docker & Docker Compose (Tùy chọn cho triển khai Production)

### Các bước triển khai chi tiết:
1. **Thiết lập Cơ sở dữ liệu**: Chạy file script `postgresql.sql` trên PostgreSQL để khởi tạo hệ thống bảng và ràng buộc dữ liệu.
2. **Cấu hình môi trường**: 
   - Truy cập vào file `appsettings.json` trong dự án `MovieBookingAPI`.
   - Cập nhật chuỗi kết nối (`ConnectionStrings:DefaultConnection`) trỏ đến máy chủ PostgreSQL.
   - Kiểm tra và thiết lập các biến số bảo mật cho `JwtSettings`.
3. **Triển khai qua Docker**:
   ```bash
   docker build -t moviebooking-api -f Dockerfile .
   docker run -d -p 8080:8080 --name moviebooking-api moviebooking-api
   ```
4. **Kiểm tra hoạt động**:
   - Truy cập Swagger UI qua URL: `http://localhost:8080/swagger/index.html` để kiểm thử toàn bộ API.
   - Build và khởi động ứng dụng `MovieBookingClient` để thao tác trực tiếp trên giao diện Desktop.

## 5. Kết luận (Conclusion)
Dự án MovieBooking System đã hoàn thành mục tiêu xây dựng một nền tảng quản lý rạp phim chuyên nghiệp. Sự kết hợp giữa ASP.NET Core 8 mạnh mẽ, cấu trúc CSDL PostgreSQL tối ưu, cùng kiến trúc Clean Architecture giúp sản phẩm có khả năng duy trì, nâng cấp linh hoạt và mở rộng ổn định trong tương lai, đáp ứng tốt các yêu cầu khắt khe của mô hình nghiệp vụ thực tế.
