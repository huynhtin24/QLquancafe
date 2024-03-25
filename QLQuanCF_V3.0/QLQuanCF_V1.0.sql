CREATE DATABASE QLQuanCF
GO
USE QLQuanCF
GO


--Món -- Food
--Bàn -- Table
--Danh sách món -- FoodCategory
--Tài khoản -- Account
--Hóa đơn -- Bill
--Chi tiết hóa đơn -- BillInfo
--Nhập Hàng
--Xuất Hàng
--Mặt hàng
GO

CREATE TABLE TableFood
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa đặt tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống', -- Trống, có người
)
GO
CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Chưa có tên hiển thị',
	Pasword NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL DEFAULT 0 --1: admin 0: Nhân viên
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa được đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0,
	
	FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTable INT NOT NULL,
	status INT NOT NULL DEFAULT 0, --1: đãthanh toán 0:chưa thanh toán
	discount INT DEFAULT 0,
	totalPrice MONEY,
	FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0,
	--idNVien INT NOT NULL,
	--PRIMARY KEY(idHoaDon, idMonAn),

	FOREIGN KEY (idBill) REFERENCES dbo.Bill(id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (idFood) REFERENCES dbo.Food(id) ON UPDATE CASCADE ON DELETE CASCADE,
	--FOREIGN KEY (idNVien) REFERENCES dbo.NhanVien(id) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE NhanVien
(
	id INT IDENTITY PRIMARY KEY,
	MaNV NVARCHAR(100) NOT NULL,
	HoNV NVARCHAR(40) NOT NULL DEFAULT N'',
	TenNV NVARCHAR(40) NOT NULL DEFAULT N'',
	DChi NVARCHAR(200) DEFAULT N'',
	SDT NVARCHAR(11),
	NamSinh CHAR(4),
	Ca NVARCHAR(20) DEFAULT N'Sáng',--Sáng, Chiều, Tối
	NgayVaoLam DATE NOT NULL DEFAULT GETDATE(),
	LoaiNV INT NOT NULL DEFAULT 0 --1: Chính thức 0:Thử việc

	--FOREIGN KEY (MaNV) REFERENCES dbo.TaiKhoan(TenDangNhap) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE MatHang
(
	id INT IDENTITY PRIMARY KEY,
	TenMH VARCHAR(100) NOT NULL,
	DGiaMH FLOAT NOT NULL DEFAULT 0
)
GO

CREATE TABLE NhapHang
(
	id INT IDENTITY PRIMARY KEY,
	idMH INT NOT NULL,
	DGiaMH FLOAT NOT NULL DEFAULT 0,
	SoLuong INT NOT NULL DEFAULT 0,
	NgayNhapHang DATE NOT NULL DEFAULT GETDATE()

	FOREIGN KEY (idMH) REFERENCES dbo.MatHang(id) ON UPDATE CASCADE ON DELETE CASCADE
)
GO

CREATE TABLE XuatHang
(
	id INT IDENTITY PRIMARY KEY,
	idMH INT NOT NULL,
	DGiaMH FLOAT NOT NULL DEFAULT 0,
	SoLuong INT NOT NULL DEFAULT 0,
	NgayXuatHang DATE NOT NULL DEFAULT GETDATE()

	FOREIGN KEY (idMH) REFERENCES dbo.MatHang(id) ON UPDATE CASCADE ON DELETE CASCADE
)
GO
INSERT INTO dbo.Account( UserName ,DisplayName ,Pasword ,Type)
VALUES  ( N'admin' , -- UserName - nvarchar(100)
          N'Admin' , -- DisplayName - nvarchar(100)
          N'admin' , -- Pasword - nvarchar(1000)
          1  -- Type - int
        )
INSERT INTO dbo.Account( UserName ,DisplayName ,Pasword ,Type)
VALUES  ( N'ThanhQuan' , -- UserName - nvarchar(100)
          N'ThanhQuan' , -- DisplayName - nvarchar(100)
          N'admin' , -- Pasword - nvarchar(1000)
          1  -- Type - int
        )
INSERT INTO dbo.Account( UserName ,DisplayName ,Pasword ,Type)
VALUES  ( N'ThanhHuy' , -- UserName - nvarchar(100)
          N'ThanhHuy' , -- DisplayName - nvarchar(100)
          N'admin' , -- Pasword - nvarchar(1000)
          0  -- Type - int
        )


INSERT INTO dbo.NhanVien( MaNV ,HoNV ,TenNV ,DChi ,SDT ,NamSinh ,Ca ,NgayVaoLam ,LoaiNV)
VALUES  ( N'NV01' , -- MaNV - nvarchar(100)
          N'Nguyễn Thanh' , -- HoNV - varchar(40)
          N'Quân' , -- TenNV - varchar(40)
          N'Sóc Trăng' , -- DChi - varchar(200)
          N'012345678' , -- SDT - nvarchar(11)
          '2002' , -- NamSinh - char(4)
          N'Sáng' , -- Ca - nvarchar(20)
          GETDATE() , -- NgayVaoLam - date
          1  -- LoaiNV - int
        )
INSERT INTO dbo.NhanVien( MaNV ,HoNV ,TenNV ,DChi ,SDT ,NamSinh ,Ca ,NgayVaoLam ,LoaiNV)
VALUES  ( N'ThanhHuy' , -- MaNV - nvarchar(100)
          N'Lê Thanh' , -- HoNV - varchar(40)
          N'Huy' , -- TenNV - varchar(40)
          N'Sóc Trăng' , -- DChi - varchar(200)
          N'098745632' , -- SDT - nvarchar(11)
          '2002' , -- NamSinh - char(4)
          N'Chiều' , -- Ca - nvarchar(20)
          GETDATE() , -- NgayVaoLam - date
          1  -- LoaiNV - int
        )
GO
CREATE PROC USP_GetAccountByUserName
@userName NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO
EXEC dbo.USP_GetAccountByUserName @userName = N'Admin' -- nvarchar(100)
GO
SELECT * FROM dbo.Account WHERE UserName = N'admin' AND Pasword = N'admin'

SELECT * FROM dbo.Account
SELECT * FROM dbo.NhanVien

GO
CREATE PROC USP_Login
@userName NVARCHAR(100), @passWord NVARCHAR(100)
AS
BEGIN
	SELECT  * FROM dbo.Account WHERE UserName = @userName AND Pasword = @passWord
END
GO


DECLARE @i INT = 1
WHILE @i <= 10
BEGIN
	INSERT dbo.TableFood (name)VALUES (N'Bàn '+ CAST(@i AS nvarchar(100)))
	SET @i = @i +1
END
GO

  GO
    
SELECT * FROM dbo.TableFood
drop  dbo.TableFood
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO
UPDATE dbo.TableFood SET status = N'Có người' WHERE id = 9
GO
EXEC dbo.USP_GetTableList
GO
--thêm category
INSERT INTO dbo.FoodCategory( name )
VALUES  ( N'Đồ uống có ga'),	  -- name - nvarchar(100)
		( N'Cafe'),
		( N'Đồ uống')
--thêm món ăn
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Cocacola', -- name - nvarchar(100)
          1, -- idCategory - int
          10000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'7 Up', -- name - nvarchar(100)
          1, -- idCategory - int
          11000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Sting', -- name - nvarchar(100)
          1, -- idCategory - int
          12000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Cà phê đen', -- name - nvarchar(100)
          2, -- idCategory - int
          15000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Cà phê sữa', -- name - nvarchar(100)
          2, -- idCategory - int
          18000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Cà phê nhiều sữa', -- name - nvarchar(100)
          2, -- idCategory - int
          18000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Cacao nóng', -- name - nvarchar(100)
          3, -- idCategory - int
          21000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Sũa chua', -- name - nvarchar(100)
          3, -- idCategory - int
          10000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Capuchino', -- name - nvarchar(100)
          3, -- idCategory - int
          25000.0  -- price - float
          )
INSERT INTO dbo.Food( name, idCategory, price )
VALUES  ( N'Bạc xỉu', -- name - nvarchar(100)
          3, -- idCategory - int
          17000.0  -- price - float
          )
--thêm Bill, hóa đơn
INSERT INTO dbo.Bill( DateCheckIn, DateCheckOut ,idTable ,status)
VALUES  ( GETDATE() , -- DateCheckIn - date
          NULL , -- DateCheckOut - date
          1 , -- idTable - int
          0  -- status - int
        )
INSERT INTO dbo.Bill( DateCheckIn, DateCheckOut ,idTable ,status)
VALUES  ( GETDATE() , -- DateCheckOut - date
          NULL , -- DateCheckIn - date
          2 , -- idTable - int
          0  -- status - int
        )
INSERT INTO dbo.Bill( DateCheckIn, DateCheckOut ,idTable ,status)
VALUES  ( GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          2 , -- idTable - int
          1  -- status - int
        )
INSERT INTO dbo.Bill( DateCheckIn, DateCheckOut ,idTable ,status)
VALUES  ( GETDATE() , -- DateCheckIn - date
          GETDATE() , -- DateCheckOut - date
          3 , -- idTable - int
          0  -- status - int
        )
--thên bill info
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 1, -- idBill - int
          1, -- idFood - int
          3  -- count - int
          )
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 1, -- idBill - int
          6, -- idFood - int
          3  -- count - int
          )
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 3, -- idBill - int
          4, -- idFood - int
          3  -- count - int
          )
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 1, -- idBill - int
          6, -- idFood - int
          3  -- count - int
          )
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 2, -- idBill - int
          3, -- idFood - int
          2  -- count - int
          )
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 2, -- idBill - int
          1, -- idFood - int
          2  -- count - int
          )
INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( 2, -- idBill - int
          5, -- idFood - int
          3  -- count - int
          )

GO
SELECT * FROM dbo.Bill WHERE idTable = 3 AND status = 1
SELECT * FROM dbo.BillInfo WHERE idBill = 2
SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
GO
SELECT f.name, bi.count, f.price, f.price * bi.count AS totalPrice FROM dbo.BillInfo bi, dbo.Bill AS b, dbo.Food AS f
WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = 2
GO
SELECT * FROM dbo.TableFood
SELECT * FROM dbo.Food
GO
SELECT * FROM dbo.FoodCategory
GO

CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
	INSERT INTO dbo.Bill( DateCheckOut ,DateCheckIn ,idTable ,status,discount)
	VALUES  ( NULL, -- DateCheckOut - date
	          GETDATE(), -- DateCheckIn - date
	          @idTable , -- idTable - int
	          0, -- status - int
			  0
	        )
END
GO
CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT , @count INT 
AS
BEGIN
	INSERT INTO dbo.BillInfo( idBill, idFood, count )
VALUES  ( @idBill, -- idBill - int
          @idFood, -- idFood - int
          @count  -- count - int
          )
END
GO

ALTER PROC USP_InsertBillInfo
@idBill INT, @idFood INT , @count INT 
AS
BEGIN
	DECLARE @isExitBillInfo INT
	DECLARE @foodCount INT = 1
	SELECT @isExitBillInfo = id, @foodCount = b.count FROM dbo.BillInfo as b WHERE idBill = @idBill AND idFood = @idFood

	IF (@isExitBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @foodCount + @count 
		IF(@newCount > 0)
			UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood = @idFood
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	END
    ELSE
	BEGIN
		INSERT INTO dbo.BillInfo( idBill, idFood, count)
		VALUES  ( @idBill, -- idBill - int
				 @idFood, -- idFood - int
				 @count  -- count - int
				 )
	END 
END
go

GO
SELECT MAX(id) FROM dbo.Bill

UPDATE dbo.Bill SET status = 1 WHERE id =1
GO
--xây dựng trigger
DROP TRIGGER UTG_UpdateBillInfo
DROP TRIGGER UTG_UpdateBill
GO 
CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = idBill FROM Inserted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0

	DECLARE @count INT
	SELECT @count = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idBill

	IF (@count > 0)
		UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable
	ELSE
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO 
CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = id FROM Inserted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill

	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0

	IF (@count = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO
DELETE dbo.BillInfo
DELETE dbo.Bill

--thêm dl bảng bill
ALTER TABLE dbo.Bill
ADD discount INT
UPDATE dbo.Bill SET discount = 0 
GO
SELECT * FROM dbo.Bill
--chuyển bàn
GO

CREATE PROC USP_SwitchTable
@idTable1 INT, @idTable2 INT
AS
BEGIN
	DECLARE @idFirstBill INT 
	DECLARE @idSecondBill INT 
	DECLARE @isFirstTableEmtry INT = 1
	DECLARE @isSecondTableEmtry INT = 1

	SELECT @idSecondBill = id  FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0
	SELECT @idFirstBill = id  FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0

	IF (@idFirstBill IS NULL)
	BEGIN
		INSERT dbo.Bill( DateCheckIn ,DateCheckOut ,idTable ,status ,discount)
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idTable1 , -- idTable - int
		          0 , -- status - int
		          0  -- discount - int
		        )
		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable1 AND status = 0

	END
		SELECT @isFirstTableEmtry = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idFirstBill
	IF (@idSecondBill IS NULL)
	BEGIN
		INSERT dbo.Bill( DateCheckIn ,DateCheckOut ,idTable ,status ,discount)
		VALUES  ( GETDATE() , -- DateCheckIn - date
		          NULL , -- DateCheckOut - date
		          @idTable2 , -- idTable - int
		          0 , -- status - int
		          0  -- discount - int
		        )
		SELECT @idSecondBill = MAX(id) FROM dbo.Bill WHERE idTable = @idTable2 AND status = 0

	END
	SELECT @isSecondTableEmtry = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idSecondBill

	SELECT id INTO IDBillInfoTable FROM dbo.BillInfo WHERE idBill = @idSecondBill

	UPDATE dbo.BillInfo SET idBill = @idSecondBill WHERE idBill = @idFirstBill

	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT *  FROM IDBillInfoTable)

	DROP TABLE IDBillInfoTable

	IF (@isFirstTableEmtry = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable2
	
	IF (@isSecondTableEmtry = 0)
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable1
		
END
GO
EXEC dbo.USP_SwitchTable @idTable1 = 5, -- int
    @idTable2 = 6 -- int

UPDATE dbo.Bill SET dateCheckOut = GETDATE() WHERE id = 40
SELECT * FROM dbo.TableFood
ALTER TABLE dbo.Bill ADD totalPrice FLOAT

DELETE dbo.BillInfo
GO


CREATE PROC USP_GetLisstBillByDate
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT t.name AS [Tên bàn], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], discount AS [Giảm giá],b.totalPrice AS [Tổng tiền]
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idTable
END
GO

CREATE PROC USP_UpdateAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @passWord NVARCHAR(100), @newPassWord NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0

	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE UserName = @userName AND Pasword = @passWord

	IF (@isRightPass = 1)
	BEGIN
		IF(@newPassWord = NULL OR @newPassWord = '')
		BEGIN 
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, Pasword = @newPassWord WHERE UserName = @userName
		END 
END
GO

 PROC USP_GestListFood
AS
BEGIN
	SELECT f.id, f.name,f.price,f.idCategory  FROM Food AS f,FoodCategory AS fc
	WHERE f.idCategory = fc.id
END

EXEC USP_GestListFood
GO
CREATE PROC USP_InsertFood
@name VARCHAR(100), @idcategory INT , @price FLOAT 
AS
BEGIN
INSERT INTO dbo.Food
        ( name, idCategory, price )
VALUES  ( N''+@name+'', -- name - nvarchar(100)
          @idcategory, -- idCategory - int
          @price  -- price - float
          )
END
GO
CREATE PROC USP_UpdateFood
@name VARCHAR(100), @idcategory INT , @price FLOAT 
AS
BEGIN
	
END
GO
CREATE TRIGGER UTG_DeleteBillInfo
ON dbo.BillInfo FOR DELETE
AS
BEGIN
	DECLARE @idBillInfo INT 
	DECLARE @idBill INT 
	SELECT @idBillInfo = id, @idBill = Deleted.idBill FROM Deleted

	DECLARE @idTable INT
	SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill

	DECLARE @count INT
	
	SELECT @count = COUNT(*) FROM dbo.BillInfo AS bi, dbo.Bill AS b 
	WHERE b.id = bi.idBill AND b.id = @idBill AND b.status = 0

	IF(@count = 0 )
		UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO
--Chuyển có dấu sang không dấu
CREATE FUNCTION dbo.fChuyenCoDauThanhKhongDau(@inputVar NVARCHAR(MAX) )
RETURNS NVARCHAR(MAX)
AS
BEGIN 
IF (@inputVar IS NULL OR @inputVar = '') RETURN ''

DECLARE @RT NVARCHAR(MAX)
DECLARE @SIGN_CHARS NCHAR(256)
DECLARE @UNSIGN_CHARS NCHAR (256)

SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩ� ��ậấèẻẽẹéềểễệếìỉĩịíò ỏõọóồổỗộốờởỡợớùủũụ úừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢ ÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉ ỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖ� �ỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲ� ��ỸỴÝ' + NCHAR(272) + NCHAR(208)
SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooo oooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEE EIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'

DECLARE @COUNTER int
DECLARE @COUNTER1 int

SET @COUNTER = 1
WHILE (@COUNTER <= LEN(@inputVar))
BEGIN 
SET @COUNTER1 = 1
WHILE (@COUNTER1 <= LEN(@SIGN_CHARS) + 1)
BEGIN
IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@inputVar,@COUNTER ,1))
BEGIN 
IF @COUNTER = 1
SET @inputVar = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)-1) 
ELSE
SET @inputVar = SUBSTRING(@inputVar, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)- @COUNTER)
BREAK
END
SET @COUNTER1 = @COUNTER1 +1
END
SET @COUNTER = @COUNTER +1
END
-- SET @inputVar = replace(@inputVar,' ','-')
RETURN @inputVar
END
SELECT * FROM dbo.Food WHERE dbo.fChuyenCoDauThanhKhongDau(name) LIKE N'%' + dbo.fChuyenCoDauThanhKhongDau(N'thuoc') + '%'

SELECT UserName,DisplayName,Type FROM dbo.Account
SELECT * FROM dbo.TableFood
EXEC dbo.USP_GetTableList
SELECT * FROM dbo.FoodCategory

DELETE dbo.TableFood where id = '19' and status = 'Trống'
SELECT * FROM dbo.NhanVien 
GO

CREATE PROC USP_GetLisstBillByDateAndPage
@checkIn DATE, @checkOut DATE, @page INT 
AS
BEGIN
	DECLARE @pageRow INT = 10
	DECLARE @selectRow INT = @pageRow
	DECLARE @exceptRow INT = (@page - 1) * @pageRow

	;WITH BillShow AS (SELECT b.id, t.name AS [Tên bàn], DateCheckIn AS [Ngày vào], DateCheckOut AS [Ngày ra], b.discount AS [Giảm giá],b.totalPrice AS [Tổng tiền]
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idTable)

	SELECT TOP (@selectRow) * FROM BillShow WHERE id NOT IN (SELECT TOP (@exceptRow) id FROM BillShow)

END
GO

EXEC USP_GetLisstBillByDateAndPage @checkIn = '2022-01-1', -- date
    @checkOut = '2022-01-11', -- date
    @page = 2 -- int
EXEC dbo.USP_GetLisstBillByDate @checkIn = '2022-01-1',
    @checkOut = '2022-01-11'
GO
CREATE PROC USP_GetNumBillByDate
@checkIn DATE, @checkOut DATE
AS
BEGIN
	SELECT COUNT(*)
	FROM dbo.Bill AS b, dbo.TableFood AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idTable
END
GO
CREATE PROC USP_HoaDon
@idBill INT 
AS
BEGIN

		SELECT   Bill.id, Bill.DateCheckIn, Bill.DateCheckOut, Food.name, Food.price, BillInfo.count, Bill.discount, Bill.totalPrice
		FROM     Bill INNER JOIN
                 BillInfo ON Bill.id = BillInfo.idBill INNER JOIN
                 Food ON BillInfo.idFood = Food.id
		WHERE   idBill = @idBill
END
GO
EXEC dbo.USP_HoaDon @idBill = 51 -- int
SELECT * FROM dbo.Bill

 INSERT dbo.NhanVien( MaNV , HoNV ,TenNV ,DChi ,SDT ,NamSinh ,Ca ,NgayVaoLam ,LoaiNV)
 VALUES  ( N'' , N'' ,N'' , N'' ,N'' , '' , N'' , GETDATE(), 0)                

 SELECT * FROM dbo.Account
 GO
 CREATE PROC USP_InsertAccount
@userName NVARCHAR(100), @displayName NVARCHAR(100), @passWord NVARCHAR(100), @Type INT
AS
BEGIN
	INSERT INTO dbo.Account( UserName ,DisplayName ,Pasword ,Type)
	VALUES  ( N''+@userName+'' , -- UserName - nvarchar(100)
	          N''+@displayName+'' , -- DisplayName - nvarchar(100)
	          N''+@passWord+'' , -- Pasword - nvarchar(1000)
	          @Type  -- Type - int
	        ) 
END
GO
 CREATE PROC USP_InsertMatHang
@tenMatHang NVARCHAR(100), @dgiaMH FLOAT
AS
BEGIN
	INSERT INTO dbo.MatHang( TenMH, DGiaMH )
	VALUES  ( ''+@tenMatHang+'', -- TenMH - varchar(100)
	          0.0  -- DGiaMH - float
	          )
END
GO
 CREATE PROC USP_InsertTableFood
@name NVARCHAR(100), @status NVARCHAR(100)
AS
BEGIN
	INSERT INTO dbo.TableFood( name, status )
	VALUES  ( N''+@name+'', -- name - nvarchar(100)
	          N''+@status+''  -- status - nvarchar(100)
	          )
END
GO
 CREATE PROC USP_DeleteTableFood
@id int
AS
BEGIN
	DELETE dbo.TableFood
	WHERE id = @id
END
GO
 CREATE PROC USP_InsertNhapHang
@id INT , @dgia FLOAT, @soluong INT
AS
BEGIN
	INSERT INTO dbo.NhapHang( idMH, DGiaMH, SoLuong, NgayNhapHang )
	VALUES  ( @id, -- idMH - int
	          @dgia, -- DGiaMH - float
	          @soluong, -- SoLuong - int
	          GETDATE()  -- NgayNhapHang - date
	          )
END
GO
CREATE PROC USP_DeleteNhapHang
@id INT
AS
BEGIN
	DELETE dbo.NhapHang
	WHERE id = @id
END
GO
CREATE PROC USP_InsertXuatHang
@id INT , @dgia FLOAT, @soluong INT
AS
BEGIN
	INSERT INTO dbo.XuatHang
	        ( idMH, DGiaMH, SoLuong, NgayXuatHang )
	VALUES  ( @id, -- idMH - int
	          @dgia, -- DGiaMH - float
	          @soluong, -- SoLuong - int
	          GETDATE()  -- NgayXuatHang - date
	          )
END
GO
CREATE PROC USP_DeleteXuatHang
@id INT
AS
BEGIN
	DELETE dbo.XuatHang
	WHERE id = @id
END
GO
 CREATE PROC USP_InsertFoodCategory
@Name NVARCHAR(100)
AS
BEGIN
	INSERT INTO dbo.FoodCategory
	        ( name )
	VALUES  ( N''+@Name+''  -- name - nvarchar(100)
	          )
END
GO
 CREATE PROC USP_InsertNhanVien
@MaNV nvarchar(100),@HoNV nvarchar(40),@TenNV nvarchar(40),@DChi nvarchar(200),@SDT nvarchar(11),@NamSinh CHAR(4),@Ca nvarchar(20),@LoaiNV int
AS
BEGIN
	INSERT INTO dbo.NhanVien( MaNV ,HoNV ,TenNV ,DChi ,SDT ,NamSinh ,Ca ,NgayVaoLam ,LoaiNV)
	VALUES  ( N''+@MaNV+'' , -- MaNV - nvarchar(100)
	          N''+@HoNV+'' , -- HoNV - nvarchar(40)
	          N''+@TenNV+'' , -- TenNV - nvarchar(40)
	          N''+@DChi+'' , -- DChi - nvarchar(200)
	          N''+@SDT+'' , -- SDT - nvarchar(11)
	          ''+@NamSinh+'' , -- NamSinh - char(4)
	          N'@Ca'++'' , -- Ca - nvarchar(20)
	          GETDATE() , -- NgayVaoLam - date
	          0  -- LoaiNV - int
	        )
END
GO
 CREATE PROC USP_DeleteNhanVien
@MaNV nvarchar(100)
AS
BEGIN
	 DELETE dbo.NhanVien
	 WHERE MaNV = @MaNV
END
GO

 CREATE PROC USP_GetListXuatHang
AS
BEGIN
	SELECT * FROM dbo.XuatHang
END
GO
CREATE PROC USP_DeleteAccount
@userName NVARCHAR(100)
AS
BEGIN
	DELETE dbo.Account
	WHERE UserName = @userName
END
GO
EXEC dbo.USP_DeleteAccount @userName = N'D' -- nvarchar(100)
  