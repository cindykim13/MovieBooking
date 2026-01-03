# 1. Giai đoạn Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj của API và Domain vào đúng cấu trúc thư mục
# Cấu trúc: COPY [Đường dẫn trên máy tính] [Đường dẫn trong Docker]
COPY ["MovieBookingAPI/MovieBookingAPI.csproj", "MovieBookingAPI/"]
COPY ["MovieBooking.Domain/MovieBooking.Domain.csproj", "MovieBooking.Domain/"]

# Khôi phục các thư viện (Restore)
# Chỉ cần restore project API, nó sẽ tự động restore project Domain đi kèm
RUN dotnet restore "MovieBookingAPI/MovieBookingAPI.csproj"

# Copy toàn bộ mã nguồn còn lại vào Docker
COPY . .

# Chuyển thư mục làm việc vào trong folder API để Build
WORKDIR "/src/MovieBookingAPI"
RUN dotnet build "MovieBookingAPI.csproj" -c Release -o /app/build

# Publish ứng dụng (Tạo ra các file DLL để chạy)
FROM build AS publish
RUN dotnet publish "MovieBookingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Giai đoạn Runtime (Chạy ứng dụng)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieBookingAPI.dll"]