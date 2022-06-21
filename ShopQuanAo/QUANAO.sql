USE master
IF EXISTS(SELECT * FROM sys.databases WHERE name='QUANAO')
BEGIN
        DROP DATABASE QUANAO
END
CREATE DATABASE QUANAO
GO
USE QUANAO
GO

CREATE TABLE QUYEN 
(
    MAQUYEN INT IDENTITY NOT NULL,
    TENQUYEN NVARCHAR(50), -- TÊN CÁC NHÓM QUYỀN(ADIMIN,NHÂN VIÊN)
    CONSTRAINT PK_QUYEN PRIMARY KEY (MAQUYEN) --KHÓA CHÍNH
)
CREATE TABLE TAIKHOAN
(
	TENTK VARCHAR(50) NOT NULL, 
	MATKHAU VARCHAR(21),
    HOTEN NVARCHAR(50), -- HỌ TÊN
    NGSINH DATE, -- NGÀY SINH
    GTINH NVARCHAR(10),
    EMAIL VARCHAR(50), -- ĐỊA CHỈ EMAIL
    SDT VARCHAR(11), -- SDT
    DCHI NVARCHAR(50), -- ĐỊA CHỈ NHÀ
	MAQUYEN INT,
	--KHÓA CHÍNH
    CONSTRAINT PK_TAIKHOAN PRIMARY KEY (TENTK),
	--KHÓA NGOẠI
	CONSTRAINT FK_TAIKHOAN_QUYEN FOREIGN KEY(MAQUYEN) REFERENCES QUYEN(MAQUYEN)
)
CREATE TABLE TAIKHOANKHACHHANG
(
    TENTK VARCHAR(50), 
	LOAITK NVARCHAR(10) NOT NULL,
	ANH NVARCHAR(50),
	--KHÓA CHÍNH
	--KHÓA NGOẠI
	CONSTRAINT PK_TAIKHOANKHACHHANG PRIMARY KEY (TENTK),
	CONSTRAINT FK_TKKH_TAIKHOAN FOREIGN KEY(TENTK) REFERENCES TAIKHOAN(TENTK)
)

CREATE TABLE GIAMGIA 
(
    MAGIAMGIA VARCHAR(20) NOT NULL, 
    TENGIAMGIA NVARCHAR(30), -- NGÀY TẠO HÓA ĐƠN
    TGBD DATE,
	TGKT DATE,
	PHANTRAMGIAM INT,
	--KHÓA CHÍNH
    CONSTRAINT PK_GIAMGIA PRIMARY KEY (MAGIAMGIA),
)
CREATE TABLE TKGG
(
	MAGIAMGIA VARCHAR(20) NOT NULL, 
	TENTK VARCHAR(50) NOT NULL, 
	--KHÓA CHÍNH
    CONSTRAINT PK_TKGG PRIMARY KEY (MAGIAMGIA,TENTK),
	--KHÓA NGOẠI
	CONSTRAINT FK_TKGG_GIAMGIA FOREIGN KEY(MAGIAMGIA) REFERENCES GIAMGIA(MAGIAMGIA),
	CONSTRAINT FK_TKGG_TAIKHOAN FOREIGN KEY(TENTK) REFERENCES TAIKHOAN(TENTK)

)
CREATE TABLE LOAISP 
(
    MALSP VARCHAR(6) NOT NULL,
    TENLOAI NVARCHAR(50),
	--KHÓA CHÍNH
    CONSTRAINT PK_LSP PRIMARY KEY (MALSP)
)

CREATE TABLE SANPHAM 
(
    MASP INT IDENTITY NOT NULL, -- CREATE AUTO
    TENSP NVARCHAR(MAX), -- TÊN SẢN PHẨM
    DONGIA FLOAT, -- ĐƠN GIÁ
	HINHANH NVARCHAR(20),
    MALSP VARCHAR(6) REFERENCES LOAISP(MALSP),
	--KHÓA CHÍNH
    CONSTRAINT PK_SP PRIMARY KEY (MASP)

)
CREATE TABLE KHO
(
	MASP INT NOT NULL,
	SIZE VARCHAR(10), -- KÍCH THƯỚC
    SOLUONG INT, -- SỐ LƯỢNG TỒN KHO
	--KHÓA CHÍNH
    CONSTRAINT PK_KHO PRIMARY KEY (MASP,SIZE),
	--KHÓA NGOẠI
	CONSTRAINT FK_KHO_SANPHAM FOREIGN KEY(MASP) REFERENCES SANPHAM(MASP)

)
--CREATE TABLE NHAPHANG 
--(
--    MAPN INT IDENTITY NOT NULL,
--	TENNCC NVARCHAR,
--	TONGTIENNHAP FLOAT,
--	--KHÓA CHÍNH
--    CONSTRAINT PK_NHAPHANG PRIMARY KEY (MAPN), 
--)
--CREATE TABLE CTPN
--(
--	MASP INT NOT NULL,
--	MAPN INT NOT NULL,
--	SOLUONGNHAP INT,
--	DONGIANHAP FLOAT,
--	--KHÓA CHÍNH
--    CONSTRAINT PK_CTPN PRIMARY KEY (MAPN,MASP), 
--	--KHÓA NGOẠI
--	CONSTRAINT FK_CTPN_NHAPHANG FOREIGN KEY(MAPN) REFERENCES NHAPHANG(MAPN),
--	CONSTRAINT FK_CTPN_SP FOREIGN KEY(MASP) REFERENCES SANPHAM(MASP)

--)

CREATE TABLE HOADON 
(
    MAHD INT IDENTITY NOT NULL, -- CREATE AUTO
	TENTK VARCHAR(50), 
    NGTAO DATE, -- NGÀY TẠO HÓA ĐƠN
	DIACHIGH NVARCHAR(50), 
    TONGTIEN FLOAT, -- TỔNG (SỐ LƯỢNG * ĐƠN GIÁ)
	MAGIAMGIA VARCHAR(20),
	TIENGIAMGIA FLOAT,
	TIENGIAMTAIKHOAN FLOAT,
	THANHTOAN FLOAT,
    CONSTRAINT PK_HD PRIMARY KEY (MAHD),
	CONSTRAINT FK_HOADON_TAIKHOAN FOREIGN KEY(TENTK) REFERENCES TAIKHOAN(TENTK)
)
CREATE TABLE CHITIETHD 
(
    MAHD INT NOT NULL,
    MASP INT NOT NULL,
	SIZE VARCHAR(10),
    SOLUONG INT, -- SỐ LƯỢNG > 0, số lượng bán
	--KHÓA CHÍNH
    CONSTRAINT PK_CTHD PRIMARY KEY (MAHD,MASP,SIZE),
	--KHÓA NGOẠI
	CONSTRAINT FK_CTHD_KHO FOREIGN KEY(MASP,SIZE) REFERENCES KHO(MASP,SIZE),
	CONSTRAINT FK_CTHD_HOADON FOREIGN KEY(MAHD) REFERENCES HOADON(MAHD)
)

--CREATE TABLE DOITRA 
--(
--	MADOITRA INT IDENTITY NOT NULL,
--    MAHD INT REFERENCES HOADON(MAHD),
--    MASP INT REFERENCES SANPHAM(MASP),
--	TENTK VARCHAR(50),
--	NGDOI DATE,
--	LYDO NVARCHAR(Max),
--	TINHTRANG NVARCHAR(20),
--	PHANHOI NVARCHAR(50),
--	SONGAY INT,
--    CONSTRAINT PK_DOITRA PRIMARY KEY (MADOITRA),
--	CONSTRAINT FK_DOITRA_TAIKHOAN FOREIGN KEY(TENTK) REFERENCES TAIKHOAN(TENTK)
--)

----------------------------------------------------------------------------
--NHẬP DỮ LIỆU
----------------------------------------------------------------------------


--BẢNG QUYỀN
INSERT QUYEN VALUES(N'ADMIN')
INSERT QUYEN VALUES(N'KHÁCH HÀNG')

--BẢNG TÀI KHOẢN

--BẢNG LOẠI SP
INSERT INTO LOAISP
VALUES
('LSP001', 'Shirts'),
('LSP002', 'Hoodie'),
('LSP003', 'Crop Tops'),
('LSP004', 'Dresses'),
('LSP005', 'Bottoms'),
('LSP006', 'Sets'),
('LSP007', 'Accessories');

----BẢNG SẢN PHẨM
INSERT INTO SANPHAM
VALUES
('Shirt A', 270000, '1.jpg','LSP001'),
('Shirt B', 900000, '2.jpg','LSP001'),
('Shirt C', 775500, '3.jpg','LSP001'),
('Shirt D', 458500, '4.jpg','LSP001'),
('Shirt E', 540500, '5.jpg','LSP001'),
('Shirt F', 775500, '6.jpg','LSP001'),
('Shirt G', 1297500, '7.jpg','LSP001'),
('Shirt H', 1800000, '8.jpg','LSP001'),
('Shirt I', 8200000, '9.jpg','LSP001'),

('Hoodie A', 205000, '10.jpg','LSP002'),
('Hoodie B', 108000, '11.jpg','LSP002'),
('Hoodie C', 1990875, '12.jpg','LSP002'),
('Hoodie D', 400000, '13.jpg','LSP002'),
('Hoodie E', 775500, '14.jpg','LSP002'),
('Hoodie F', 420000, '15.jpg','LSP002'),

('Crop top A', 259500, '16.jpg','LSP003'),
('Crop top B', 400000, '17.jpg','LSP003'),
('Crop top C', 400000, '18.jpg','LSP003'),
('Crop top D', 259500, '19.jpg','LSP003'),
('Crop top E', 400000, '20.jpg','LSP003'),
('Crop top F', 550000, '21.jpg','LSP003'),
('Crop top G', 400000, '22.jpg','LSP003'),
('Crop top H', 1299000, '23.jpg','LSP003'),
('Crop top I', 776000, '24.jpg','LSP003'),

('Dress A', 410000, '25.jpg','LSP004'),
('Dress B', 205000, '26.jpg','LSP004'),
('Dress C', 820000, '27.jpg','LSP004'),
('Dress D', 388000, '28.jpg','LSP004'),
('Dress E', 775500, '29.jpg','LSP004'),
('Dress F', 400000, '30.jpg','LSP004'),
('Dress G', 388000, '31.jpg','LSP004'),
('Dress H', 615000, '32.jpg','LSP004'),
('Dress I', 417000, '33.jpg','LSP004'),

('Bottom A', 820000, '34.jpg','LSP005'),
('Bottom B', 259500, '35.jpg','LSP005'),
('Bottom C', 400000, '36.jpg','LSP005'),
('Bottom D', 259500, '37.jpg','LSP005'),
('Bottom E', 400000, '38.jpg','LSP005'),
('Bottom F', 458500, '39.jpg','LSP005'),
('Bottom G', 205000, '40.jpg','LSP005'),
('Bottom H', 205000, '41.jpg','LSP005'),
('Bottom I', 400000, '42.jpg','LSP005'),
('Bottom J', 1299000, '43.jpg','LSP005'),

('Set A', 12990000, '44.jpg','LSP006'),
('Set B', 775500, '45.jpg','LSP006'),
('Set C', 400000, '46.jpg','LSP006'),
('Set D', 615000, '47.jpg','LSP006'),
('Set E', 820000, '48.jpg','LSP006'),
('Set F', 400000, '49.jpg','LSP006'),
('Set G', 775500, '50.jpg','LSP006'),

('Accessories A', 150000, '51.jpg','LSP007'),
('Accessories B', 200000, '52.jpg','LSP007'),
('Accessories C', 45000, '53.jpg','LSP007'),
('Accessories D', 300000, '54.jpg','LSP007'),
('Accessories E', 517000, '55.jpg','LSP007'),
('Accessories F', 400000, '56.jpg','LSP007'),
('Accessories G', 45000, '57.jpg','LSP007'),
('Accessories H', 900000, '58.jpg','LSP007'),
('Accessories I', 12990000, '59.jpg','LSP007');

--BẢNG KHO
INSERT INTO KHO 
VALUES
(1,'S',10),
(2,'S',30),
(3,'S',30),
(4,'S',30),
(5,'S',30),
(6,'S',30),
(7,'S',30),
(8,'S',30),
(9,'S',30),
--
(1,'M',30),
(2,'M',30),
(3,'M',30),
(4,'M',30),
(5,'M',30),
(6,'M',30),
(7,'M',30),
(8,'M',30),
(9,'M',30),
--
(1,'L',30),
(2,'L',30),
(3,'L',30),
(4,'L',30),
(5,'L',30),
(6,'L',30),
(7,'L',30),
(8,'L',30),
(9,'L',30),
--
(1,'XL',30),
(2,'XL',30),
(3,'XL',30),
(4,'XL',30),
(5,'XL',30),
(6,'XL',30),
(7,'XL',30),
(8,'XL',30),
(9,'XL',30),
--------------------------------
(10, 'S',30),
(11, 'S',30),
(12, 'S',30),
(13, 'S',30),
(14, 'S',30),
(15, 'S',30),
--
(10, 'M',30),
(11, 'M',30),
(12, 'M',30),
(13, 'M',30),
(14, 'M',30),
(15, 'M',30),
--
(10, 'L',30),
(11, 'L',30),
(12, 'L',30),
(13, 'L',30),
(14, 'L',30),
(15, 'L',30),
--
(10, 'XL',30),
(11, 'XL',30),
(12, 'XL',30),
(13, 'XL',30),
(14, 'XL',30),
(15, 'XL',30),
---
(16, 'S',30),
(17, 'S',30),
(18, 'S',30),
(19, 'S',30),
(20, 'S',30),
(21, 'S',30),
(22, 'S',30),
(23, 'S',30),
(24, 'S',30),
--
(16, 'M',30),
(17, 'M',30),
(18, 'M',30),
(19, 'M',30),
(20, 'M',30),
(21, 'M',30),
(22, 'M',30),
(23, 'M',30),
(24, 'M',30),
--
(16, 'L',30),
(17, 'L',30),
(18, 'L',30),
(19, 'L',30),
(20, 'L',30),
(21, 'L',30),
(22, 'L',30),
(23, 'L',30),
(24, 'L',30),
--
(16, 'XL',30),
(17, 'XL',30),
(18, 'XL',30),
(19, 'XL',30),
(20, 'XL',30),
(21, 'XL',30),
(22, 'XL',30),
(23, 'XL',30),
(24, 'XL',30),
---


(25, 'S',30),
(26, 'S',30),
(27, 'S',30),
(28, 'S',30),
(29, 'S',30),
(30, 'S',30),
(31, 'S',30),
(32, 'S',30),
(33, 'S',30),
--
(25, 'M',30),
(26, 'M',30),
(27, 'M',30),
(28, 'M',30),
(29, 'M',30),
(30, 'M',30),
(31, 'M',30),
(32, 'M',30),
(33, 'M',30),
--
(25, 'L',30),
(26, 'L',30),
(27, 'L',30),
(28, 'L',30),
(29, 'L',30),
(30, 'L',30),
(31, 'L',30),
(32, 'L',30),
(33, 'L',30),
--
(25, 'XL',30),
(26, 'XL',30),
(27, 'XL',30),
(28, 'XL',30),
(29, 'XL',30),
(30, 'XL',30),
(31, 'XL',30),
(32, 'XL',30),
(33, 'XL',30),
---


(34, 'S',30),
(35, 'S',30),
(36, 'S',30),
(37, 'S',30),
(38, 'S',30),
(39, 'S',30),
(40, 'S',30),
(41, 'S',30),
(42, 'S',30),
(43, 'S',30),
--
(34, 'M',30),
(35, 'M',30),
(36, 'M',30),
(37, 'M',30),
(38, 'M',30),
(39, 'M',30),
(40, 'M',30),
(41, 'M',30),
(42, 'M',30),
(43, 'M',30),
--
(34, 'L',30),
(35, 'L',30),
(36, 'L',30),
(37, 'L',30),
(38, 'L',30),
(39, 'L',30),
(40, 'L',30),
(41, 'L',30),
(42, 'L',30),
(43, 'L',30),
--
(34, 'XL',30),
(35, 'XL',30),
(36, 'XL',30),
(37, 'XL',30),
(38, 'XL',30),
(39, 'XL',30),
(40, 'XL',30),
(41, 'XL',30),
(42, 'XL',30),
(43, 'XL',30),

---

(44,  'S',30),
(45,  'S',30),
(46,  'S',30),
(47,  'S',30),
(48,  'S',30),
(49,  'S',30),
(50,  'S',30),
--
(44,  'M',30),
(45,  'M',30),
(46,  'M',30),
(47,  'M',30),
(48,  'M',30),
(49,  'M',30),
(50,  'M',30),
--
(44,  'L',30),
(45,  'L',30),
(46,  'L',30),
(47,  'L',30),
(48,  'L',30),
(49,  'L',30),
(50,  'L',30),
--
(44,  'XL',30),
(45,  'XL',30),
(46,  'XL',30),
(47,  'XL',30),
(48,  'XL',30),
(49,  'XL',30),
(50,  'XL',30),

(51,'OVS',30),
(52,'OVS',30),
(53,'OVS',30),
(54,'OVS',30),
(55,'OVS',30),
(56,'OVS',30),
(57,'OVS',30),
(58,'OVS',30),
(59,'OVS',30);


SET DATEFORMAT DMY
INSERT INTO TAIKHOAN
VALUES
(N'TK001','123',N'Trần Trọng Bình Phương','16/07/2001',N'Nam','phuong@gmail.com',0324567899,N'Phường 13, Tân Bình, TPHCM',1),
(N'TK002','123',N'Bùi Thị Ái Ly','12/12/2001',N'Nữ','aily@gmail.com',0984563217,N'Hóc Môn, TPHCM',2),
(N'TK003','123',N'Nguyễn Ngọc Nhung','21/03/2001',N'Nữ','ngocnhung@gmail.com',0985321456,N'Đồng Nai ',1),
(N'TK004','123',N'Võ Văn Tin','25/09/2001',N'Nam','vantin@gmail.com',0985123654,N'TPHCM',1),
(N'TK005','123',N'Lê Thùy Na','15/07/2001',N'Nữ','na@gmail.com',0985123632,N'Phú Yên',1)


insert into TAIKHOANKHACHHANG
values('TK001',N'Đồng','Bronze.jpg'),
('TK002',N'Đồng','Bronze.jpg'),
('TK003',N'Đồng','Bronze.jpg'),
('TK004',N'Đồng','Bronze.jpg'),
('TK005',N'Đồng','Bronze.jpg');

--Bảng chi tiết phiếu nhập



--BẢNG GIẢM GIÁ
INSERT INTO GIAMGIA VALUES('SALE3',N'Giảm 3 phần trăm','30/05/2022','30/06/2022',3)
INSERT INTO GIAMGIA VALUES('SALE5',N'Giảm 5 phần trăm','30/05/2022','30/06/2022',5)
INSERT INTO GIAMGIA VALUES('SALE7',N'Giảm 7 phần trăm','30/05/2022','30/06/2022',7)
INSERT INTO GIAMGIA VALUES('SALE9',N'Giảm 9 phần trăm','30/05/2022','30/06/2022',9)
INSERT INTO GIAMGIA VALUES('SALE10',N'Giảm 10 phần trăm','30/05/2022','30/06/2022',10)
INSERT INTO GIAMGIA VALUES('SALE12',N'Giảm 12 phần trăm','30/05/2022','30/06/2022',12)
INSERT INTO GIAMGIA VALUES('SALE15',N'Giảm 15 phần trăm','30/05/2022','30/06/2022',15)


----- PROC
--proc thong ke san pham ban ra theo nam
create procedure Sp_ThongKeSanPhamBanTheoNam
	@year INT
as
begin

	select Year(NGTAO) as years, cthd.MASP , TENSP,SIZE,SUM(SOLUONG) as tongsl
	from HOADON hd, CHITIETHD cthd,SANPHAM sp
	where hd.MAHD = cthd.MAHD and YEAR(NGTAO) = @year and cthd.MASP = sp.MASP
	group by YEAR(NGTAO), cthd.MASP,TENSP,SIZE
	order by MASP,SIZE
end

---proc xuat thong tin chi tiet giao dich

create procedure Sp_ChiTietGiaoDich
	@mahd INT
as
begin

	select TENSP,SIZE,DONGIA,SOLUONG
	from CHITIETHD cthd,SANPHAM sp
	where MAHD = @mahd and cthd.MASP = sp.MASP
	order by sp.MASP,SIZE
end