-- Xóa bảng cũ nếu tồn tại để làm sạch
DROP TABLE IF EXISTS Ticket, Booking, Showtime, AppUser, Seat, SeatType, ScreenRoom, Cinema, MovieCast, MovieGenre, Genre, Movie, Actor CASCADE;

-- 1. Bảng Master Data
CREATE TABLE Genre (
    GenreId SERIAL PRIMARY KEY,
    GenreName VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Actor (
    ActorId SERIAL PRIMARY KEY,
    ActorName VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Movie (
    MovieId SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    StoryLine TEXT,
    Director VARCHAR(100),
    Duration INT NOT NULL CHECK (Duration > 0),
    ReleaseYear INT CHECK (ReleaseYear >= 1900),
    AgeRating VARCHAR(10),
    Rating REAL CHECK (Rating >= 0 AND Rating <= 10),
    PosterUrl VARCHAR(500),
    Status VARCHAR(20) DEFAULT 'Coming Soon'
);

CREATE TABLE MovieGenre (
    MovieId INT NOT NULL REFERENCES Movie(MovieId) ON DELETE CASCADE,
    GenreId INT NOT NULL REFERENCES Genre(GenreId) ON DELETE CASCADE,
    PRIMARY KEY (MovieId, GenreId)
);

CREATE TABLE MovieCast (
    MovieId INT NOT NULL REFERENCES Movie(MovieId) ON DELETE CASCADE,
    ActorId INT NOT NULL REFERENCES Actor(ActorId) ON DELETE CASCADE,
    PRIMARY KEY (MovieId, ActorId)
);

CREATE TABLE Cinema (
    CinemaId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(255),
    City VARCHAR(50),
    PhoneNumber VARCHAR(20)
);

CREATE TABLE ScreenRoom (
    RoomId SERIAL PRIMARY KEY,
    CinemaId INT NOT NULL REFERENCES Cinema(CinemaId) ON DELETE CASCADE,
    Name VARCHAR(50) NOT NULL,
    TotalSeats INT DEFAULT 0
);

CREATE TABLE SeatType (
    TypeId SERIAL PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL,
    Surcharge NUMERIC(18, 2) DEFAULT 0
);

CREATE TABLE Seat (
    SeatId SERIAL PRIMARY KEY,
    RoomId INT NOT NULL REFERENCES ScreenRoom(RoomId) ON DELETE CASCADE,
    TypeId INT NOT NULL REFERENCES SeatType(TypeId),
    "Row" CHAR(2) NOT NULL,
    "Number" INT NOT NULL,
    GridRow INT,
    GridColumn INT,
    CONSTRAINT UQ_Seat_Position UNIQUE (RoomId, "Row", "Number")
);

CREATE TABLE AppUser (
    UserId SERIAL PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    FullName VARCHAR(100),
    PhoneNumber VARCHAR(20),
    "Role" VARCHAR(20) DEFAULT 'Customer',
    CreatedAt TIMESTAMP DEFAULT NOW()
);

CREATE TABLE Showtime (
    ShowtimeId SERIAL PRIMARY KEY,
    MovieId INT NOT NULL REFERENCES Movie(MovieId),
    RoomId INT NOT NULL REFERENCES ScreenRoom(RoomId),
    StartTime TIMESTAMP NOT NULL,
    EndTime TIMESTAMP,
    BasePrice NUMERIC(18, 2) NOT NULL CHECK (BasePrice >= 0),
    Status SMALLINT DEFAULT 1
);

CREATE TABLE Booking (
    BookingId SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(UserId),
    ShowtimeId INT NOT NULL REFERENCES Showtime(ShowtimeId),
    BookingDate TIMESTAMP DEFAULT NOW(),
    TotalAmount NUMERIC(18, 2) CHECK (TotalAmount >= 0),
    PaymentMethod VARCHAR(50),
    Status SMALLINT DEFAULT 0
);

CREATE TABLE Ticket (
    TicketId SERIAL PRIMARY KEY,
    BookingId INT NOT NULL REFERENCES Booking(BookingId) ON DELETE CASCADE,
    ShowtimeId INT NOT NULL REFERENCES Showtime(ShowtimeId),
    SeatId INT NOT NULL REFERENCES Seat(SeatId),
    Price NUMERIC(18, 2) CHECK (Price >= 0),
    CONSTRAINT UQ_Ticket_Seat_Showtime UNIQUE (ShowtimeId, SeatId)
);

-- Tạo bảng tạm để chứa dữ liệu thô từ CSV
-- Dùng kiểu TEXT cho tất cả các cột để đảm bảo import không bị lỗi do kiểu dữ liệu
-- Tạo bảng tạm để chứa dữ liệu thô từ CSV
DROP TABLE IF EXISTS stage_movies_raw;

CREATE TABLE stage_movies_raw (
    movie_name TEXT,
    original_title TEXT,
    category TEXT,
    year TEXT,
    age_rating TEXT,
    duration TEXT,
    story_line TEXT,
    rating TEXT,
    casts TEXT,
    director TEXT,
    user_reviews TEXT,
    genres TEXT,
    poster_url TEXT,
    status TEXT
);
SELECT * FROM stage_movies_raw LIMIT 10;

-- SCRIPT CHUYỂN ĐỔI DỮ LIỆU TỪ BẢNG TẠM SANG BẢNG CHÍNH
-- Dọn dẹp dữ liệu cũ trong các bảng chính
TRUNCATE TABLE Movie, Genre, Actor, MovieGenre, MovieCast RESTART IDENTITY CASCADE;

-- BƯỚC A: NẠP DỮ LIỆU VÀO BẢNG MOVIE (Giữ nguyên)
INSERT INTO Movie (Title, StoryLine, Director, ReleaseYear, AgeRating, Rating, Duration, PosterUrl, Status)
SELECT
    TRIM(movie_name),
    TRIM(story_line),
    TRIM(director),
    to_number(year, '9999'), 
    TRIM(age_rating),
    CASE 
        WHEN rating ~ '^[0-9\.]+$' THEN
            CASE 
                WHEN CAST(rating AS REAL) > 10 THEN 10
                WHEN CAST(rating AS REAL) < 0 THEN 0
                ELSE CAST(rating AS REAL)
            END
        ELSE 0
    END,
    CASE
        WHEN duration LIKE '%h %m' THEN 
            (CAST(split_part(duration, 'h', 1) AS INT) * 60) + CAST(trim(leading ' ' from split_part(split_part(duration, 'h', 2), 'm', 1)) AS INT)
        WHEN duration LIKE '%h' THEN 
            CAST(replace(duration, 'h', '') AS INT) * 60
        WHEN duration LIKE '%m' THEN 
            CAST(replace(duration, 'm', '') AS INT)
        ELSE 90
    END,
    TRIM(poster_url),
    TRIM(status)
FROM stage_movies_raw
WHERE movie_name IS NOT NULL AND TRIM(movie_name) <> '';

-- BƯỚC B: CHUẨN HÓA GENRE (Giữ nguyên)
INSERT INTO Genre (GenreName)
SELECT DISTINCT TRIM(unnest(string_to_array(regexp_replace(genres, '[\[\]'']', '', 'g'), ',')))
FROM stage_movies_raw
WHERE genres IS NOT NULL AND genres <> ''
ON CONFLICT (GenreName) DO NOTHING;

-- [FIX LỖI TẠI ĐÂY] Thêm DISTINCT để loại bỏ các cặp (MovieId, GenreId) trùng lặp
INSERT INTO MovieGenre (MovieId, GenreId)
SELECT DISTINCT m.MovieId, g.GenreId
FROM stage_movies_raw raw
CROSS JOIN LATERAL unnest(string_to_array(regexp_replace(raw.genres, '[\[\]'']', '', 'g'), ',')) AS s(genre_name)
JOIN Movie m ON m.Title = TRIM(raw.movie_name)
JOIN Genre g ON g.GenreName = TRIM(s.genre_name);

-- BƯỚC C: CHUẨN HÓA CAST (DIỄN VIÊN)
INSERT INTO Actor (ActorName)
SELECT DISTINCT TRIM(unnest(string_to_array(regexp_replace(casts, '[\[\]'']', '', 'g'), ',')))
FROM stage_movies_raw
WHERE casts IS NOT NULL AND casts <> ''
ON CONFLICT (ActorName) DO NOTHING;

-- [FIX DỰ PHÒNG] Thêm DISTINCT để loại bỏ các cặp (MovieId, ActorId) trùng lặp
INSERT INTO MovieCast (MovieId, ActorId)
SELECT DISTINCT m.MovieId, a.ActorId
FROM stage_movies_raw raw
CROSS JOIN LATERAL unnest(string_to_array(regexp_replace(raw.casts, '[\[\]'']', '', 'g'), ',')) AS s(actor_name)
JOIN Movie m ON m.Title = TRIM(raw.movie_name)
JOIN Actor a ON a.ActorName = TRIM(s.actor_name);

SELECT * FROM Movie LIMIT 10;
SELECT * FROM Genre LIMIT 10;
SELECT * FROM MovieGenre LIMIT 10;
SELECT * FROM Actor LIMIT 10;
SELECT * FROM MovieCast LIMIT 10;
SELECT * FROM Cinema LIMIT 10;
SELECT * FROM ScreenRoom LIMIT 10;
SELECT * FROM SeatType LIMIT 10;
SELECT * FROM Seat LIMIT 10;
SELECT * FROM AppUser LIMIT 10;
SELECT * FROM Showtime LIMIT 10;
SELECT * FROM Ticket LIMIT 10;

-- SCRIPT TẠO DỮ LIỆU GIẢ LẬP CHO POSTGRESQL
-- PHIÊN BẢN ĐÃ CHUYỂN ĐỔI TỪ T-SQL
-- ================================================================
-- BƯỚC A: DỌN DẸP DỮ LIỆU CŨ (CLEANUP)
-- ================================================================
-- Sử dụng TRUNCATE để xóa nhanh dữ liệu và RESTART IDENTITY để reset ID tự tăng.
-- CASCADE để tự động xóa các dữ liệu phụ thuộc ở các bảng khác.
TRUNCATE TABLE Cinema, AppUser RESTART IDENTITY CASCADE;

-- ================================================================
-- BƯỚC B: TẠO DỮ LIỆU HẠ TẦNG (INFRASTRUCTURE DATA)
-- ================================================================

-- 1. Thêm Cụm rạp (Cinema)
INSERT INTO Cinema (Name, Address, City, PhoneNumber) VALUES 
(N'CGV Vincom Đồng Khởi', N'Tầng 3, Vincom Center Đồng Khởi, 72 Lê Thánh Tôn, Q.1', N'Ho Chi Minh', '19006017'),
(N'CGV Pandora City', N'Tầng 3, Pandora City, 1/1 Trường Chinh, Q.Tân Phú', N'Ho Chi Minh', '19006017'),
(N'CGV Thảo Điền Pearl', N'Tầng 2, Thảo Điền Pearl, 12 Quốc Hương, Q.2', N'Ho Chi Minh', '19006017'),
(N'CGV Aeon Bình Tân', N'Tầng 3, Aeon Mall Bình Tân, 1 Đường số 17A, Q.Bình Tân', N'Ho Chi Minh', '19006017'),
(N'CGV Satra Củ Chi', N'Tầng 3, Satra Centre Mall Củ Chi, 1239 Tỉnh Lộ 8, H.Củ Chi', N'Ho Chi Minh', '19006017'),
(N'CGV Menas Mall (CGV CT Plaza)', N'Tầng 10, Menas Mall, 60A Trường Sơn, Q.Tân Bình', N'Ho Chi Minh', '19006017'),
(N'CGV Vincom Mega Mall Grand Park', N'Tầng 5, Vincom Mega Mall Grand Park, Q.9', N'Ho Chi Minh', '19006017'),
(N'CGV Lý Chính Thắng', N'Lầu 3, Terra Royal, 83 Lý Chính Thắng, Q.3', N'Ho Chi Minh', '19006017'),
(N'CGV Hùng Vương Plaza', N'Tầng 7, Hùng Vương Plaza, 126 Hùng Vương, Q.5', N'Ho Chi Minh', '19006017'),
(N'CGV Aeon Tân Phú', N'Tầng 3, Aeon Mall Tân Phú, 30 Bờ Bao Tân Thắng, Q.Tân Phú', N'Ho Chi Minh', '19006017'),
(N'CGV Saigonres Nguyễn Xí', N'Tầng 4-5, Saigonres Plaza, 79/81 Nguyễn Xí, Q.Bình Thạnh', N'Ho Chi Minh', '19006017'),
(N'CGV Sư Vạn Hạnh', N'Tầng 6, Vạn Hạnh Mall, 11 Sư Vạn Hạnh, Q.10', N'Ho Chi Minh', '19006017'),
(N'CGV Crescent Mall', N'Lầu 5, Crescent Mall, 101 Tôn Dật Tiên, Q.7', N'Ho Chi Minh', '19006017'),
(N'CGV Liberty Citypoint', N'Tầng M-1, Khách sạn Liberty Central Saigon Citypoint, 59 Pasteur, Q.1', N'Ho Chi Minh', '19006017'),
(N'CGV Vincom Center Landmark 81', N'Tầng B1, Vincom Center Landmark 81, 720A Điện Biên Phủ, Q.Bình Thạnh', N'Ho Chi Minh', '19006017'),
(N'CGV Pearl Plaza', N'Tầng 5, Pearl Plaza, 561A Điện Biên Phủ, Q.Bình Thạnh', N'Ho Chi Minh', '19006017'),
(N'CGV Gigamall Thủ Đức', N'Tầng 6, Gigamall, 240-242 Phạm Văn Đồng, TP.Thủ Đức', N'Ho Chi Minh', '19006017'),
(N'CGV Vivo City', N'Lầu 5, SC VivoCity, 1058 Nguyễn Văn Linh, Q.7', N'Ho Chi Minh', '19006017'),
(N'CGV Vincom Thủ Đức', N'Tầng 5, Vincom Thủ Đức, 216 Võ Văn Ngân, TP.Thủ Đức', N'Ho Chi Minh', '19006017'),
(N'CGV Hoàng Văn Thụ', N'Tầng 1-2, Gala Center, 415 Hoàng Văn Thụ, Q.Tân Bình', N'Ho Chi Minh', '19006017'),
(N'CGV Vincom Gò Vấp', N'Tầng 5, Vincom Plaza Gò Vấp, 12 Phan Văn Trị, Q.Gò Vấp', N'Ho Chi Minh', '19006017');

-- 2. Thêm Phòng chiếu (ScreenRoom) - 7 phòng/rạp, 102 ghế/phòng
INSERT INTO ScreenRoom (CinemaId, Name, TotalSeats)
SELECT 
    c.CinemaId, 
    'Rạp ' || num,
    102 -- (8 hàng * 12 ghế) + (1 hàng * 6 ghế đôi)
FROM Cinema c
CROSS JOIN generate_series(1, 7) AS num;

-- 3. Thêm Loại ghế và Sơ đồ ghế vật lý
INSERT INTO SeatType (TypeName, Surcharge) VALUES 
('Standard', 0), ('VIP', 20000), ('Sweetbox', 50000);

-- 3.1. Tạo ghế đơn (Hàng A -> H)
INSERT INTO Seat (RoomId, TypeId, "Row", "Number", GridRow, GridColumn)
SELECT 
    r.RoomId,
    CASE 
        WHEN s.row_char IN ('G', 'H') THEN 2 -- VIP
        ELSE 1 -- Standard
    END,
    s.row_char,
    s.col_num,
    ASCII(s.row_char) - 64,
    s.col_num
FROM ScreenRoom r
CROSS JOIN LATERAL (
    SELECT row_char, col_num FROM (VALUES ('A'),('B'),('C'),('D'),('E'),('F'),('G'),('H')) AS rows(row_char), generate_series(1,12) AS col_num
) s;

-- 3.2. Tạo ghế đôi (Hàng I)
INSERT INTO Seat (RoomId, TypeId, "Row", "Number", GridRow, GridColumn)
SELECT 
    r.RoomId,
    3, -- Sweetbox
    'I',
    sb_num,
    9,
    (sb_num * 2) - 1
FROM ScreenRoom r
CROSS JOIN generate_series(1, 6) AS sb_num;

-- ================================================================
-- BƯỚC C: TẠO DỮ LIỆU NGƯỜI DÙNG (USER DATA)
-- ================================================================

-- 1. Tạo Admin mặc định
INSERT INTO AppUser (Username, PasswordHash, Email, FullName, PhoneNumber, "Role") VALUES 
('admin', 'HASH_ADMIN_123', 'admin@moviebooking.com', 'System Administrator', '0909000000', 'Admin');

-- 2. Tạo 50 Khách hàng ngẫu nhiên
INSERT INTO AppUser (Username, PasswordHash, Email, FullName, PhoneNumber, "Role")
SELECT
    'user' || i,
    'HASH_USER_PASS',
    'user' || i || '@gmail.com',
    'Customer ' || i,
    '09' || floor(random() * 90000000 + 10000000)::text,
    'Customer'
FROM generate_series(1, 50) AS i;

-- ================================================================
-- BƯỚC D: TẠO DỮ LIỆU GIAO DỊCH (TRANSACTIONAL DATA)
-- ================================================================

-- 1. Tạo Lịch chiếu (Showtimes)
INSERT INTO Showtime (MovieId, RoomId, StartTime, EndTime, BasePrice, Status)
SELECT 
    m.MovieId,
    r.RoomId,
    -- Random thời gian trong 7 ngày tới
    NOW() + (floor(random() * 7) || ' days')::interval + (floor(random() * (23-8+1) + 8) || ' hours')::interval,
    -- EndTime sẽ được tính toán lại ngay sau đó
    NOW(), 
    CASE WHEN floor(random() * 2) = 0 THEN 80000 ELSE 100000 END,
    1 -- Open
FROM Movie m
CROSS JOIN ScreenRoom r
WHERE m.Status = 'Now Showing'
ORDER BY random()
LIMIT 150; -- Tạo 150 suất chiếu

-- Cập nhật lại EndTime chính xác
UPDATE Showtime s
SET EndTime = s.StartTime + (m.Duration + 30) * INTERVAL '1 minute'
FROM Movie m
WHERE s.MovieId = m.MovieId;

-- 2. Tạo Giao dịch Đặt vé (Bookings & Tickets)
-- Sử dụng khối lệnh DO để thực hiện logic lặp
DO $$
DECLARE
    showtime_record RECORD;
    num_bookings INT;
    user_id INT;
    seats_to_book INT;
    new_booking_id INT;
    rand_hours_before INT;
    booking_date TIMESTAMP;
BEGIN
    FOR showtime_record IN SELECT ShowtimeId, StartTime FROM Showtime LOOP
        -- Với mỗi suất chiếu, tạo ngẫu nhiên từ 0 đến 8 đơn đặt vé
        num_bookings := floor(random() * 9);
        FOR k IN 1..num_bookings LOOP
            BEGIN
                -- Chọn User ngẫu nhiên
                SELECT UserId INTO user_id FROM AppUser WHERE "Role" = 'Customer' ORDER BY random() LIMIT 1;
                
                -- Chọn số lượng ghế ngẫu nhiên
                seats_to_book := floor(random() * 4) + 1;
                
                -- Giả lập ngày đặt vé
                rand_hours_before := floor(random() * 24);
                booking_date := showtime_record.StartTime - (rand_hours_before || ' hours')::interval;
                IF booking_date < NOW() THEN booking_date := NOW(); END IF;
                IF booking_date > showtime_record.StartTime THEN booking_date := showtime_record.StartTime - interval '30 minutes'; END IF;

                -- Tạo Booking Header
                INSERT INTO Booking (UserId, ShowtimeId, BookingDate, TotalAmount, Status, PaymentMethod)
                VALUES (user_id, showtime_record.ShowtimeId, booking_date, 0, 1, 'Momo')
                RETURNING BookingId INTO new_booking_id;
                
                -- Insert Ticket (Chọn ghế chưa bán)
                INSERT INTO Ticket (BookingId, ShowtimeId, SeatId, Price)
                SELECT 
                    new_booking_id,
                    showtime_record.ShowtimeId,
                    s.SeatId,
                    sht.BasePrice + st.Surcharge
                FROM Seat s
                JOIN Showtime sht ON sht.ShowtimeId = showtime_record.ShowtimeId
                JOIN SeatType st ON s.TypeId = st.TypeId
                WHERE s.RoomId = sht.RoomId
                  AND s.SeatId NOT IN (SELECT SeatId FROM Ticket WHERE ShowtimeId = showtime_record.ShowtimeId)
                ORDER BY random()
                LIMIT seats_to_book;

                -- Nếu không có ghế nào được chèn, rollback (bằng cách ném lỗi để khối EXCEPTION bắt)
                IF NOT FOUND THEN
                    RAISE EXCEPTION 'No seats available';
                END IF;

                -- Cập nhật tổng tiền
                UPDATE Booking
                SET TotalAmount = (SELECT SUM(Price) FROM Ticket WHERE BookingId = new_booking_id)
                WHERE BookingId = new_booking_id;
                
            EXCEPTION WHEN OTHERS THEN
                -- Bỏ qua lỗi (ví dụ hết ghế) và tiếp tục
                CONTINUE;
            END;
        END LOOP;
    END LOOP;
END $$;


-- ================================================================
-- NHÓM 1: CHỨC NĂNG CÔNG KHAI (MOVIES, SHOWTIMES)
-- ================================================================

-- 1.1. Lấy danh sách phim phân trang (usp_GetMoviesPaged)
CREATE OR REPLACE FUNCTION usp_getmoviespaged(
    p_pageindex INT DEFAULT 1,
    p_pagesize INT DEFAULT 20,
    p_sortby VARCHAR(50) DEFAULT 'releaseyear'
) RETURNS TABLE (totalrecords BIGINT, movieid INT, title VARCHAR(255), duration INT, releaseyear INT, rating REAL, posterurl VARCHAR(500), status VARCHAR(20), genres TEXT)
LANGUAGE plpgsql AS $$
DECLARE v_totalrecords BIGINT;
BEGIN
    SELECT COUNT(*) INTO v_totalrecords FROM movie;
    RETURN QUERY
    SELECT v_totalrecords, m.movieid, m.title, m.duration, m.releaseyear, m.rating, m.posterurl, m.status,
           (SELECT STRING_AGG(g.genrename, ', ') FROM moviegenre mg JOIN genre g ON mg.genreid = g.genreid WHERE mg.movieid = m.movieid)
    FROM movie m
    ORDER BY
        CASE WHEN lower(p_sortby) = 'title' THEN m.title END ASC,
        CASE WHEN lower(p_sortby) = 'rating' THEN CAST(m.rating AS TEXT) END DESC,
        CASE WHEN lower(p_sortby) = 'releaseyear' THEN CAST(m.releaseyear AS TEXT) END DESC,
        m.movieid DESC
    OFFSET (p_pageindex - 1) * p_pagesize
    LIMIT p_pagesize;
END;
$$;


-- 1.2. Tìm kiếm và Lọc phim (usp_SearchMovies)
CREATE OR REPLACE FUNCTION usp_searchmovies(
    p_keyword VARCHAR(100) DEFAULT NULL, p_genreid INT DEFAULT NULL, p_releaseyear INT DEFAULT NULL,
    p_pageindex INT DEFAULT 1, p_pagesize INT DEFAULT 20
) RETURNS TABLE (movieid INT, title VARCHAR(255), duration INT, releaseyear INT, rating REAL, posterurl VARCHAR(500), status VARCHAR(20), genres TEXT, totalcount BIGINT)
LANGUAGE plpgsql AS $$
DECLARE v_searchterm VARCHAR(110);
BEGIN
    IF p_keyword IS NOT NULL AND TRIM(p_keyword) <> '' THEN v_searchterm := '%' || TRIM(p_keyword) || '%'; END IF;
    RETURN QUERY
    WITH FilteredMovies AS (
        SELECT m.* FROM movie m
        WHERE (v_searchterm IS NULL OR m.title ILIKE v_searchterm)
          AND (p_releaseyear IS NULL OR m.releaseyear = p_releaseyear)
          AND (p_genreid IS NULL OR EXISTS (SELECT 1 FROM moviegenre mg WHERE mg.movieid = m.movieid AND mg.genreid = p_genreid))
    )
    SELECT fm.movieid, fm.title, fm.duration, fm.releaseyear, fm.rating, fm.posterurl, fm.status,
           (SELECT STRING_AGG(g.genrename, ', ') FROM moviegenre mg JOIN genre g ON mg.genreid = g.genreid WHERE mg.movieid = fm.movieid),
           (SELECT COUNT(*) FROM FilteredMovies)
    FROM FilteredMovies fm ORDER BY fm.releaseyear DESC, fm.title ASC OFFSET (p_pageindex - 1) * p_pagesize LIMIT p_pagesize;
END;
$$;

-- 1.3. Xem chi tiết phim (usp_GetMovieDetail)
CREATE OR REPLACE FUNCTION usp_getmoviedetail(p_movieid INT)
RETURNS TABLE (movieid INT, title VARCHAR(255), storyline TEXT, director VARCHAR(100), duration INT, releaseyear INT, agerating VARCHAR(10), rating REAL, posterurl VARCHAR(500), status VARCHAR(20), genres TEXT, casts TEXT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
    SELECT m.movieid, m.title, m.storyline, m.director, m.duration, m.releaseyear, m.agerating, m.rating, m.posterurl, m.status,
           (SELECT STRING_AGG(g.genrename, ', ' ORDER BY g.genrename) FROM moviegenre mg JOIN genre g ON mg.genreid = g.genreid WHERE mg.movieid = m.movieid),
           (SELECT STRING_AGG(a.actorname, ', ' ORDER BY a.actorname) FROM moviecast mc JOIN actor a ON mc.actorid = a.actorid WHERE mc.movieid = m.movieid)
    FROM movie m WHERE m.movieid = p_movieid;
END;
$$;



-- 1.4. Xem lịch chiếu theo phim (usp_GetShowtimesByMovie)
-- Sửa lại Function usp_getshowtimesbymovie
CREATE OR REPLACE FUNCTION usp_getshowtimesbymovie(
    p_movieid INT, 
    p_viewdate TIMESTAMP -- Sửa từ DATE sang TIMESTAMP
) 
RETURNS TABLE (
    cinemaid INT, cinemaname VARCHAR, cinemaaddress VARCHAR, roomid INT, roomname VARCHAR, 
    totalseats INT, showtimeid INT, starttime TIMESTAMP, endtime TIMESTAMP, baseprice NUMERIC
) 
LANGUAGE plpgsql 
AS $$
DECLARE 
    v_startrange TIMESTAMP := date_trunc('day', p_viewdate);
    v_endrange TIMESTAMP := v_startrange + INTERVAL '1 day';
BEGIN
    IF p_viewdate::DATE = CURRENT_DATE AND NOW() > v_startrange THEN 
        v_startrange := NOW(); 
    END IF;

    RETURN QUERY
    SELECT 
        c.cinemaid, c.name, c.address, 
        sr.roomid, sr.name, sr.totalseats, 
        s.showtimeid, s.starttime, s.endtime, s.baseprice
    FROM showtime s 
    JOIN screenroom sr ON s.roomid = sr.roomid 
    JOIN cinema c ON sr.cinemaid = c.cinemaid
    WHERE s.movieid = p_movieid 
      AND s.status = 1 
      AND s.starttime >= v_startrange AND s.starttime < v_endrange
    ORDER BY c.name ASC, s.starttime ASC;
END;
$$;


-- 1.5. Xem sơ đồ ghế (usp_GetShowtimeSeatMap)
CREATE OR REPLACE FUNCTION usp_getshowtimeseatmap(p_showtimeid INT)
RETURNS TABLE (seatid INT, "Row" CHAR(2), "Number" INT, gridrow INT, gridcolumn INT, seattype VARCHAR(50), price NUMERIC(18,2), status INT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
    SELECT s.seatid, s."Row", s."Number", s.gridrow, s.gridcolumn, st.typename, (sht.baseprice + st.surcharge),
           CASE WHEN t.ticketid IS NULL THEN 0 WHEN b.status = 1 THEN 2 WHEN b.status = 0 AND (NOW() - b.bookingdate) <= INTERVAL '10 minutes' THEN 1 ELSE 0 END
    FROM showtime sht
    JOIN seat s ON sht.roomid = s.roomid
    JOIN seattype st ON s.typeid = st.typeid
    LEFT JOIN ticket t ON s.seatid = t.seatid AND t.showtimeid = sht.showtimeid
    LEFT JOIN booking b ON t.bookingid = b.bookingid
    WHERE sht.showtimeid = p_showtimeid
    ORDER BY s.gridrow ASC, s.gridcolumn ASC;
END;
$$;


-- ================================================================
-- NHÓM 2: CHỨC NĂNG NGƯỜI DÙNG (AUTH & BOOKING)
-- ================================================================

-- 2.1. Đăng ký (usp_RegisterUser)
CREATE OR REPLACE FUNCTION usp_registeruser(
    p_username VARCHAR(50),
    p_passwordhash VARCHAR(255),
    p_email VARCHAR(100),
    p_fullname VARCHAR(100),
    p_phonenumber VARCHAR(20)
) RETURNS TABLE (newuserid INT)
LANGUAGE plpgsql
AS $$
DECLARE
    v_newuserid INT;
BEGIN
    IF EXISTS (SELECT 1 FROM appuser WHERE username = p_username) THEN
        RAISE EXCEPTION 'Username đã tồn tại.' USING ERRCODE = '23505';
    END IF;
    IF EXISTS (SELECT 1 FROM appuser WHERE email = p_email) THEN
        RAISE EXCEPTION 'Email đã tồn tại.' USING ERRCODE = '23505';
    END IF;

    INSERT INTO appuser (username, passwordhash, email, fullname, phonenumber, "Role", createdat)
    VALUES (p_username, p_passwordhash, p_email, p_fullname, p_phonenumber, 'Customer', NOW())
    RETURNING userid INTO v_newuserid;

    RETURN QUERY SELECT v_newuserid;
END;
$$;


-- 2.2. Lấy thông tin đăng nhập (usp_GetUserByUsername)
CREATE OR REPLACE FUNCTION usp_getuserbyusername(
    p_username VARCHAR(100)
) RETURNS TABLE (userid INT, username VARCHAR(50), passwordhash VARCHAR(255), email VARCHAR(100), fullname VARCHAR(100), "Role" VARCHAR(20))
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        u.userid, 
        u.username, 
        u.passwordhash, 
        u.email, 
        u.fullname,
        u."Role"
    FROM appuser u
    WHERE u.username = p_username OR u.email = p_username;
END;
$$;



-- 2.3. Đặt vé (usp_CreateBookingTransaction)
-- Sửa lại Function usp_createbookingtransaction
CREATE OR REPLACE FUNCTION usp_createbookingtransaction(
    in_userid INT, 
    in_showtimeid INT, 
    in_seatidsjson JSON
)
RETURNS INT LANGUAGE plpgsql AS $$
DECLARE 
    v_totalamount NUMERIC(18,2); 
    v_newbookingid INT; 
    v_seatid INT;
BEGIN
    -- Kiểm tra xung đột
    IF EXISTS (
        SELECT 1 FROM ticket t 
        WHERE t.showtimeid = in_showtimeid AND t.seatid IN (SELECT (value::INT) FROM json_array_elements_text(in_seatidsjson))
    ) THEN
        RAISE EXCEPTION 'Một hoặc nhiều ghế bạn chọn đã có người đặt.' USING ERRCODE = 'P0001';
    END IF;

    -- Tính tổng tiền
    SELECT SUM(sht.baseprice + st.surcharge) INTO v_totalamount 
    FROM json_array_elements_text(in_seatidsjson) r(seatid) 
    JOIN seat s ON CAST(r.seatid AS INT) = s.seatid 
    JOIN seattype st ON s.typeid = st.typeid 
    JOIN showtime sht ON sht.showtimeid = in_showtimeid;
    
    IF v_totalamount IS NULL THEN 
        RAISE EXCEPTION 'Dữ liệu ghế hoặc suất chiếu không hợp lệ.'; 
    END IF;

    -- Tạo Booking
    INSERT INTO booking (userid, showtimeid, bookingdate, totalamount, status, paymentmethod) 
    VALUES (in_userid, in_showtimeid, NOW(), v_totalamount, 0, 'Unpaid') 
    RETURNING bookingid INTO v_newbookingid;

    -- Tạo Tickets
    FOR v_seatid IN SELECT (value::INT) FROM json_array_elements_text(in_seatidsjson) LOOP
        INSERT INTO ticket (bookingid, showtimeid, seatid, price) 
        SELECT v_newbookingid, in_showtimeid, v_seatid, sht.baseprice + st.surcharge 
        FROM seat s 
        JOIN seattype st ON s.typeid = st.typeid 
        JOIN showtime sht ON sht.showtimeid = in_showtimeid 
        WHERE s.seatid = v_seatid;
    END LOOP;
    
    RETURN v_newbookingid;
END;
$$;

-- ================================================================
-- NHÓM 2: CHỨC NĂNG NGƯỜI DÙNG (TIẾP THEO)
-- ================================================================

-- 2.4. Xác nhận thanh toán (usp_ConfirmBookingPayment)
CREATE OR REPLACE FUNCTION usp_confirmbookingpayment(p_bookingid INT, p_userid INT, p_paymentmethod VARCHAR(50))
RETURNS VOID LANGUAGE plpgsql AS $$
DECLARE v_currentstatus SMALLINT; v_bookingdate TIMESTAMP; v_ownerid INT; v_timeoutminutes INT := 10;
BEGIN
    SELECT status, bookingdate, userid INTO v_currentstatus, v_bookingdate, v_ownerid FROM booking WHERE bookingid = p_bookingid FOR UPDATE;
    IF NOT FOUND THEN RAISE EXCEPTION 'Đơn hàng không tồn tại.' USING ERRCODE = 'P0002'; END IF;
    IF v_ownerid <> p_userid THEN RAISE EXCEPTION 'Bạn không có quyền truy cập đơn hàng này.' USING ERRCODE = 'P0003'; END IF;
    IF v_currentstatus = 1 THEN RAISE EXCEPTION 'Đơn hàng này đã được thanh toán trước đó.' USING ERRCODE = 'P0004'; END IF;
    IF v_currentstatus = 2 THEN RAISE EXCEPTION 'Đơn hàng đã bị hủy.' USING ERRCODE = 'P0005'; END IF;
    IF (NOW() - v_bookingdate) > (v_timeoutminutes || ' minutes')::interval THEN UPDATE booking SET status = 2 WHERE bookingid = p_bookingid; RAISE EXCEPTION 'Giao dịch đã hết hạn thanh toán.' USING ERRCODE = 'P0006'; END IF;
    UPDATE booking SET status = 1, paymentmethod = p_paymentmethod WHERE bookingid = p_bookingid;
END;
$$;

-- 2.5. Lấy lịch sử đặt vé (usp_GetBookingHistory)
CREATE OR REPLACE FUNCTION usp_getbookinghistory(p_userid INT)
RETURNS TABLE (bookingid INT, bookingdate TIMESTAMP, totalamount NUMERIC(18,2), status SMALLINT, paymentmethod VARCHAR(50), movietitle VARCHAR(255), posterurl VARCHAR(500), duration INT, starttime TIMESTAMP, cinemaname VARCHAR(100), roomname VARCHAR(50), seatname TEXT)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
    SELECT b.bookingid, b.bookingdate, b.totalamount, b.status, b.paymentmethod, m.title, m.posterurl, m.duration, st.starttime, c.name, sr.name, s."Row" || s."Number"::TEXT
    FROM booking b JOIN showtime st ON b.showtimeid = st.showtimeid JOIN movie m ON st.movieid = m.movieid JOIN screenroom sr ON st.roomid = sr.roomid JOIN cinema c ON sr.cinemaid = c.cinemaid JOIN ticket t ON b.bookingid = t.bookingid JOIN seat s ON t.seatid = s.seatid
    WHERE b.userid = p_userid ORDER BY b.bookingdate DESC;
END;
$$;

-- ================================================================
-- NHÓM 3: CHỨC NĂNG QUẢN TRỊ VIÊN (ADMIN)
-- ================================================================

-- 3.1. Nhập liệu phim
CREATE OR REPLACE FUNCTION usp_importmoviesbulk(p_jsondata JSON) RETURNS INT LANGUAGE plpgsql AS $$
DECLARE v_insertedcount INT;
BEGIN
    CREATE TEMP TABLE StagingMovies ON COMMIT DROP AS SELECT * FROM json_populate_recordset(null::record, p_jsondata) AS (Title VARCHAR(255), StoryLine TEXT, Director VARCHAR(100), Duration INT, ReleaseYear INT, AgeRating VARCHAR(10), Rating REAL, PosterUrl VARCHAR(500), Genres JSON, Casts JSON);
    INSERT INTO genre (genrename) SELECT DISTINCT TRIM(value::TEXT, '"') FROM StagingMovies, json_array_elements_text(Genres) ON CONFLICT (genrename) DO NOTHING;
    INSERT INTO actor (actorname) SELECT DISTINCT TRIM(value::TEXT, '"') FROM StagingMovies, json_array_elements_text(Casts) ON CONFLICT (actorname) DO NOTHING;
    INSERT INTO movie (title, storyline, director, duration, releaseyear, agerating, rating, posterurl, status) SELECT s.Title, s.StoryLine, s.Director, s.Duration, s.ReleaseYear, s.AgeRating, s.Rating, s.PosterUrl, CASE WHEN s.ReleaseYear < EXTRACT(YEAR FROM NOW()) THEN 'Ended' WHEN s.ReleaseYear = EXTRACT(YEAR FROM NOW()) THEN 'Now Showing' ELSE 'Coming Soon' END FROM StagingMovies s WHERE NOT EXISTS (SELECT 1 FROM movie m WHERE m.title = s.Title AND m.releaseyear = s.ReleaseYear);
    GET DIAGNOSTICS v_insertedcount = ROW_COUNT;
    INSERT INTO moviegenre (movieid, genreid) SELECT DISTINCT m.movieid, g.genreid FROM StagingMovies s JOIN movie m ON m.title = s.Title AND m.releaseyear = s.ReleaseYear CROSS JOIN LATERAL json_array_elements_text(s.Genres) AS jg(name) JOIN genre g ON g.genrename = TRIM(jg.name, '"') ON CONFLICT (movieid, genreid) DO NOTHING;
    INSERT INTO moviecast (movieid, actorid) SELECT DISTINCT m.movieid, a.actorid FROM StagingMovies s JOIN movie m ON m.title = s.Title AND m.releaseyear = s.ReleaseYear CROSS JOIN LATERAL json_array_elements_text(s.Casts) AS jc(name) JOIN actor a ON a.actorname = TRIM(jc.name, '"') ON CONFLICT (movieid, actorid) DO NOTHING;
    RETURN v_insertedcount;
END;
$$;

-- 3.2. Thêm phim
CREATE OR REPLACE FUNCTION usp_addmovie(p_title VARCHAR(255), p_storyline TEXT, p_director VARCHAR(100), p_duration INT, p_releaseyear INT, p_agerating VARCHAR(10), p_rating REAL, p_posterurl VARCHAR(500), p_status VARCHAR(20), p_genrenamesjson JSON, p_actornamesjson JSON)
RETURNS INT LANGUAGE plpgsql AS $$
DECLARE v_newmovieid INT;
BEGIN
    IF EXISTS (SELECT 1 FROM movie WHERE title = p_title AND releaseyear = p_releaseyear) THEN RAISE EXCEPTION 'Phim này đã tồn tại.'; END IF;
    INSERT INTO movie (title, storyline, director, duration, releaseyear, agerating, rating, posterurl, status) VALUES (p_title, p_storyline, p_director, p_duration, p_releaseyear, p_agerating, p_rating, p_posterurl, p_status) RETURNING movieid INTO v_newmovieid;
    INSERT INTO genre (genrename) SELECT DISTINCT TRIM(value::TEXT, '"') FROM json_array_elements_text(p_genrenamesjson) ON CONFLICT DO NOTHING;
    INSERT INTO moviegenre (movieid, genreid) SELECT v_newmovieid, g.genreid FROM json_array_elements_text(p_genrenamesjson) j JOIN genre g ON g.genrename = TRIM(j.value::TEXT, '"');
    INSERT INTO actor (actorname) SELECT DISTINCT TRIM(value::TEXT, '"') FROM json_array_elements_text(p_actornamesjson) ON CONFLICT DO NOTHING;
    INSERT INTO moviecast (movieid, actorid) SELECT v_newmovieid, a.actorid FROM json_array_elements_text(p_actornamesjson) j JOIN actor a ON a.actorname = TRIM(j.value::TEXT, '"');
    RETURN v_newmovieid;
END;
$$;

-- 3.3. Cập nhật phim
CREATE OR REPLACE FUNCTION usp_updatemovie(p_movieid INT, p_title VARCHAR(255), p_storyline TEXT, p_director VARCHAR(100), p_duration INT, p_releaseyear INT, p_agerating VARCHAR(10), p_rating REAL, p_posterurl VARCHAR(500), p_status VARCHAR(20), p_genrenamesjson JSON, p_actornamesjson JSON)
RETURNS VOID LANGUAGE plpgsql AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM movie WHERE movieid = p_movieid) THEN RAISE EXCEPTION 'Phim không tồn tại.'; END IF;
    UPDATE movie SET title=p_title, storyline=p_storyline, director=p_director, duration=p_duration, releaseyear=p_releaseyear, agerating=p_agerating, rating=p_rating, posterurl=p_posterurl, status=p_status WHERE movieid=p_movieid;
    DELETE FROM moviegenre WHERE movieid = p_movieid; DELETE FROM moviecast WHERE movieid = p_movieid;
    INSERT INTO genre (genrename) SELECT DISTINCT TRIM(value::TEXT, '"') FROM json_array_elements_text(p_genrenamesjson) ON CONFLICT DO NOTHING;
    INSERT INTO moviegenre (movieid, genreid) SELECT p_movieid, g.genreid FROM json_array_elements_text(p_genrenamesjson) j JOIN genre g ON g.genrename = TRIM(j.value::TEXT, '"');
    INSERT INTO actor (actorname) SELECT DISTINCT TRIM(value::TEXT, '"') FROM json_array_elements_text(p_actornamesjson) ON CONFLICT DO NOTHING;
    INSERT INTO moviecast (movieid, actorid) SELECT p_movieid, a.actorid FROM json_array_elements_text(p_actornamesjson) j JOIN actor a ON a.actorname = TRIM(j.value::TEXT, '"');
END;
$$;

-- 3.4. Xóa phim
CREATE OR REPLACE FUNCTION usp_deletemovie(p_movieid INT) RETURNS VOID LANGUAGE plpgsql AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM showtime WHERE movieid = p_movieid) THEN RAISE EXCEPTION 'Không thể xóa phim này vì đã có lịch chiếu.'; END IF;
    DELETE FROM movie WHERE movieid = p_movieid;
END;
$$;

-- 3.5. Cập nhật hồ sơ người dùng
CREATE OR REPLACE FUNCTION usp_updateuserprofile(p_userid INT, p_fullname VARCHAR(100), p_phonenumber VARCHAR(20), p_email VARCHAR(100))
RETURNS INT LANGUAGE plpgsql AS $$
DECLARE v_rowcount INT;
BEGIN
    IF EXISTS (SELECT 1 FROM appuser WHERE email = p_email AND userid <> p_userid) THEN RETURN 0; END IF;
    UPDATE appuser SET fullname = p_fullname, phonenumber = p_phonenumber, email = p_email WHERE userid = p_userid;
    GET DIAGNOSTICS v_rowcount = ROW_COUNT;
    RETURN v_rowcount;
END;
$$;

-- 3.6. Đổi mật khẩu
CREATE OR REPLACE FUNCTION usp_changeuserpassword(p_userid INT, p_newpasswordhash VARCHAR(255))
RETURNS INT LANGUAGE plpgsql AS $$
DECLARE v_rowcount INT;
BEGIN
    UPDATE appuser SET passwordhash = p_newpasswordhash WHERE userid = p_userid;
    GET DIAGNOSTICS v_rowcount = ROW_COUNT;
    RETURN v_rowcount;
END;
$$;

-- 3.7. Tạo lịch chiếu
CREATE OR REPLACE FUNCTION usp_createshowtime(p_movieid INT, p_roomid INT, p_starttime TIMESTAMP, p_baseprice NUMERIC, p_cleaningtimeminutes INT DEFAULT 15)
RETURNS INT LANGUAGE plpgsql AS $$
DECLARE v_duration INT; v_endtime TIMESTAMP; v_newshowtimeid INT;
BEGIN
    IF p_baseprice < 0 THEN RAISE EXCEPTION 'Giá vé không hợp lệ.'; END IF;
    IF p_starttime < NOW() THEN RAISE EXCEPTION 'Thời gian chiếu phải ở tương lai.'; END IF;
    SELECT duration INTO v_duration FROM movie WHERE movieid = p_movieid;
    IF NOT FOUND THEN RAISE EXCEPTION 'Phim không tồn tại.'; END IF;
    v_endtime := p_starttime + (v_duration + p_cleaningtimeminutes) * INTERVAL '1 minute';
    IF EXISTS (SELECT 1 FROM showtime WHERE roomid = p_roomid AND status = 1 AND (p_starttime < endtime) AND (v_endtime > starttime)) THEN RAISE EXCEPTION 'Phòng chiếu bị trùng lịch.'; END IF;
    INSERT INTO showtime (movieid, roomid, starttime, endtime, baseprice, status) VALUES (p_movieid, p_roomid, p_starttime, v_endtime, p_baseprice, 1) RETURNING showtimeid INTO v_newshowtimeid;
    RETURN v_newshowtimeid;
END;
$$;

-- 3.8. Cập nhật lịch chiếu
CREATE OR REPLACE FUNCTION usp_updateshowtime(p_showtimeid INT, p_movieid INT, p_roomid INT, p_starttime TIMESTAMP, p_baseprice NUMERIC, p_status SMALLINT, p_cleaningtimeminutes INT DEFAULT 15)
RETURNS VOID LANGUAGE plpgsql AS $$
DECLARE v_duration INT; v_endtime TIMESTAMP; v_oldmovieid INT; v_oldroomid INT; v_oldstarttime TIMESTAMP;
BEGIN
    IF NOT EXISTS (SELECT 1 FROM showtime WHERE showtimeid = p_showtimeid) THEN RAISE EXCEPTION 'Suất chiếu không tồn tại.'; END IF;
    IF EXISTS (SELECT 1 FROM booking WHERE showtimeid = p_showtimeid AND status = 1) THEN
        SELECT movieid, roomid, starttime INTO v_oldmovieid, v_oldroomid, v_oldstarttime FROM showtime WHERE showtimeid = p_showtimeid;
        IF (v_oldmovieid <> p_movieid OR v_oldroomid <> p_roomid OR v_oldstarttime <> p_starttime) THEN RAISE EXCEPTION 'Không thể sửa vì đã có vé bán.'; END IF;
    END IF;
    SELECT duration INTO v_duration FROM movie WHERE movieid = p_movieid;
    v_endtime := p_starttime + (v_duration + p_cleaningtimeminutes) * INTERVAL '1 minute';
    IF EXISTS (SELECT 1 FROM showtime WHERE roomid = p_roomid AND showtimeid <> p_showtimeid AND status = 1 AND (p_starttime < endtime) AND (v_endtime > starttime)) THEN RAISE EXCEPTION 'Phòng chiếu bị trùng lịch.'; END IF;
    UPDATE showtime SET movieid=p_movieid, roomid=p_roomid, starttime=p_starttime, endtime=v_endtime, baseprice=p_baseprice, status=p_status WHERE showtimeid=p_showtimeid;
END;
$$;

-- 3.9. Xóa lịch chiếu
CREATE OR REPLACE FUNCTION usp_deleteshowtime(p_showtimeid INT) RETURNS TEXT LANGUAGE plpgsql AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM showtime WHERE showtimeid = p_showtimeid) THEN RAISE EXCEPTION 'Suất chiếu không tồn tại.'; END IF;
    IF EXISTS (SELECT 1 FROM booking WHERE showtimeid = p_showtimeid AND status = 1) THEN
        UPDATE showtime SET status = 2 WHERE showtimeid = p_showtimeid;
        RETURN 'Soft Deleted';
    ELSE
        DELETE FROM ticket WHERE bookingid IN (SELECT bookingid FROM booking WHERE showtimeid = p_showtimeid);
        DELETE FROM booking WHERE showtimeid = p_showtimeid;
        DELETE FROM showtime WHERE showtimeid = p_showtimeid;
        RETURN 'Hard Deleted';
    END IF;
END;
$$;

-- 3.10. Tạo phòng và ghế
CREATE OR REPLACE FUNCTION usp_createroomwithseats(p_cinemaid INT, p_name VARCHAR(50), p_seatsjson JSON)
RETURNS INT LANGUAGE plpgsql AS $$
DECLARE v_totalseats INT; v_newroomid INT;
BEGIN
    IF EXISTS (SELECT 1 FROM screenroom WHERE cinemaid = p_cinemaid AND name = p_name) THEN RAISE EXCEPTION 'Tên phòng đã tồn tại.'; END IF;
    SELECT COUNT(*) INTO v_totalseats FROM json_array_elements(p_seatsjson);
    IF v_totalseats = 0 THEN RAISE EXCEPTION 'Sơ đồ ghế không được trống.'; END IF;
    INSERT INTO screenroom (cinemaid, name, totalseats) VALUES (p_cinemaid, p_name, v_totalseats) RETURNING roomid INTO v_newroomid;
    INSERT INTO seat (roomid, typeid, "Row", "Number", gridrow, gridcolumn)
    SELECT v_newroomid, (value->>'TypeId')::INT, (value->>'Row')::CHAR(2), (value->>'Number')::INT, (value->>'GridRow')::INT, (value->>'GridColumn')::INT
    FROM json_array_elements(p_seatsjson);
    RETURN v_newroomid;
END;
$$;

-- 3.11. Xóa phòng
CREATE OR REPLACE FUNCTION usp_deletescreenroom(p_roomid INT) RETURNS VOID LANGUAGE plpgsql AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM screenroom WHERE roomid = p_roomid) THEN RAISE EXCEPTION 'Phòng chiếu không tồn tại.'; END IF;
    IF EXISTS (SELECT 1 FROM showtime WHERE roomid = p_roomid) THEN RAISE EXCEPTION 'Không thể xóa phòng vì đã có lịch chiếu.'; END IF;
    DELETE FROM screenroom WHERE roomid = p_roomid;
END;
$$;