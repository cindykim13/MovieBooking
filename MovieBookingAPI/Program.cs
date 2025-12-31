using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieBookingAPI.Data;
using MovieBookingAPI.Repositories;
using MovieBookingAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình kết nối Database (SQL Server)
// Lấy chuỗi kết nối từ appsettings.json và đăng ký AppDbContext vào DI Container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Thêm Controllers và cấu hình JSON Serializer
// Sử dụng NewtonsoftJson để xử lý dữ liệu JSON phức tạp tốt hơn System.Text.Json mặc định
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Ngăn chặn lỗi vòng lặp tham chiếu khi serialize đối tượng quan hệ
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// 3. Cấu hình Swagger/OpenAPI (Tài liệu hóa API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieBookingAPI", Version = "v1" });

    // Định nghĩa bảo mật JWT Bearer
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Vui lòng nhập Token vào ô bên dưới (Không cần chữ 'Bearer').",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Yêu cầu bảo mật cho các Endpoint
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// 4. Cấu hình CORS (Cross-Origin Resource Sharing)
// Cho phép Frontend (React/Angular) chạy ở localhost khác port có thể gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:5173") // Port phổ biến của React/Vite
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// 5. Cấu hình Authentication (JWT Bearer)
// Đọc cấu hình từ JwtSettings trong appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

// Đăng ký Dependency Injection cho Repository và Service
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Đăng ký mới
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
// Đăng ký Module Movie
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
// Đăng ký Module Showtimes
builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
// Đăng ký Module Booking (Mới)
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();
// Đăng ký Module Admin Movie (Mới)
builder.Services.AddScoped<IAdminMovieRepository, AdminMovieRepository>();
builder.Services.AddScoped<IAdminMovieService, AdminMovieService>();
// Đăng ký Module Admin Showtimes (Mới)
builder.Services.AddScoped<IAdminShowtimeRepository, AdminShowtimeRepository>();
builder.Services.AddScoped<IAdminShowtimeService, AdminShowtimeService>();
// Đăng ký Module Admin Rooms (Mới)
builder.Services.AddScoped<IAdminRoomRepository, AdminRoomRepository>();
builder.Services.AddScoped<IAdminRoomService, AdminRoomService>();


var app = builder.Build();



// --- Cấu hình HTTP Request Pipeline (Middleware) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Kích hoạt chính sách CORS
app.UseCors("AllowClientApp");

// Kích hoạt xác thực và phân quyền
app.UseAuthentication();
app.UseAuthorization();

// Ánh xạ các Controller vào đường dẫn URL
app.MapControllers();

app.Run();