# Sử dụng hình ảnh .NET 8 SDK để build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy file csproj và restore các thư viện
COPY ["MovieBookingAPI.csproj", "./"]
RUN dotnet restore "MovieBookingAPI.csproj"

# Copy toàn bộ mã nguồn còn lại
COPY . .

# Build ứng dụng
RUN dotnet build "MovieBookingAPI.csproj" -c Release -o /app/build

# Publish ứng dụng ra thư mục /app/publish
FROM build AS publish
RUN dotnet publish "MovieBookingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Sử dụng hình ảnh ASP.NET Core Runtime để chạy (nhẹ hơn SDK)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieBookingAPI.dll"]