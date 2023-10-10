CREATE DATABASE [StudentDB2]
USE [StudentDB2]

-- hedt
CREATE TABLE [dbo].[HeDT](
	[MaHeDT] [nchar](10) NOT NULL,
	[TenHeDT] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_HeDT] PRIMARY KEY CLUSTERED 
(
	[MaHeDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- khoa hoc
CREATE TABLE [dbo].[KhoaHoc](
	[MaKhoaHoc] [nchar](10) NOT NULL,
	[TenKhoaHoc] [nvarchar](50) NULL,
 CONSTRAINT [PK_KhoaHoc] PRIMARY KEY CLUSTERED 
(
	[MaKhoaHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- khoa
CREATE TABLE [dbo].[Khoa](
	[MaKhoa] [nchar](10) NOT NULL,
	[TenKhoa] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](50) NOT NULL,
	[DienThoai] [nchar](10) NULL,
 CONSTRAINT [PK_Khoa] PRIMARY KEY CLUSTERED 
(
	[MaKhoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- lop
CREATE TABLE [dbo].[Lop](
	[MaLop] [nchar](10) NOT NULL,
	[TenLop] [nvarchar](50) NOT NULL,
	[MaKhoa] [nchar](10) NOT NULL,
	[MaHeDT] [nchar](10) NOT NULL,
	[MaKhoaHoc] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED 
(
	[MaLop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_HeDT] FOREIGN KEY([MaHeDT])
REFERENCES [dbo].[HeDT] ([MaHeDT])
GO

ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_HeDT]
GO

ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_Khoa] FOREIGN KEY([MaKhoa])
REFERENCES [dbo].[Khoa] ([MaKhoa])
GO

ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_Khoa]
GO

ALTER TABLE [dbo].[Lop]  WITH CHECK ADD  CONSTRAINT [FK_Lop_KhoaHoc] FOREIGN KEY([MaKhoaHoc])
REFERENCES [dbo].[KhoaHoc] ([MaKhoaHoc])
GO

ALTER TABLE [dbo].[Lop] CHECK CONSTRAINT [FK_Lop_KhoaHoc]
GO
-- mon hoc
CREATE TABLE [dbo].[MonHoc](
	[MaMH] [nchar](10) NOT NULL,
	[TenMH] [nvarchar](50) NOT NULL,
	[SoTC] [int] NOT NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MaMH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
-- sinh vien
CREATE TABLE [dbo].[SinhVien](
	[MaSV] [nchar](10) NOT NULL,
	[TenSV] [nvarchar](50) NOT NULL,
	[GioiTinh] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[QueQuan] [nvarchar](50) NOT NULL,
	[MaLop] [nchar](10) NOT NULL,
 CONSTRAINT [PK_SinhVien] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SinhVien]  WITH CHECK ADD  CONSTRAINT [FK_SinhVien_Lop] FOREIGN KEY([MaLop])
REFERENCES [dbo].[Lop] ([MaLop])
GO

ALTER TABLE [dbo].[SinhVien] CHECK CONSTRAINT [FK_SinhVien_Lop]
GO
-- diem
CREATE TABLE [dbo].[Diem](
	[MaSV] [nchar](10) NOT NULL,
	[MaMH] [nchar](10) NOT NULL,
	[DiemL1] [float] NOT NULL,
	[DiemL2] [float] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Diem]  WITH CHECK ADD  CONSTRAINT [FK_Diem_MonHoc] FOREIGN KEY([MaMH])
REFERENCES [dbo].[MonHoc] ([MaMH])
GO

ALTER TABLE [dbo].[Diem] CHECK CONSTRAINT [FK_Diem_MonHoc]
GO

ALTER TABLE [dbo].[Diem]  WITH CHECK ADD  CONSTRAINT [FK_Diem_SinhVien] FOREIGN KEY([MaSV])
REFERENCES [dbo].[SinhVien] ([MaSV])
GO

ALTER TABLE [dbo].[Diem] CHECK CONSTRAINT [FK_Diem_SinhVien]

-- chen du lieu
-- Chèn dữ liệu vào bảng HeDT
INSERT INTO HeDT (MaHeDT, TenHeDT) VALUES
('HDT001', N'Đại học'),
('HDT002', N'Văn bằng hai');
-- Chèn dữ liệu vào bảng KhoaHoc
INSERT INTO KhoaHoc (MaKhoaHoc, TenKhoaHoc) VALUES
('KH001', N'Khoá 2021'),
('KH002', N'Khoa 2022');
-- Chèn dữ liệu vào bảng Khoa
INSERT INTO Khoa (MaKhoa, TenKhoa, DiaChi, DienThoai) VALUES
('K001', N'Khoa Công nghệ thông tin', N'Địa chỉ 1', '0123456789'),
('K002', N'Quản trị kinh doanh', N'Địa chỉ 2', '0987654321'),
('K003', N'Ngôn ngữ Anh', N'Địa chỉ 3', NULL);
-- Chèn dữ liệu vào bảng Lop
INSERT INTO Lop (MaLop, TenLop, MaKhoa, MaHeDT, MaKhoaHoc) VALUES
('L001', 'Lớp 01', 'K001', 'HDT001', 'KH001'),
('L002', 'Lớp 02', 'K002', 'HDT002', 'KH002');
-- Chèn dữ liệu vào bảng MonHoc
INSERT INTO MonHoc (MaMH, TenMH, SoTC) VALUES
('MH001', N'Toán rời rạc', 4),
('MH002', N'Giải tích 1', 3),
('MH003', N'Triết học Mac-Lenin', 4),
('MH004', N'Lịch sử Đảng', 4);
-- Chèn dữ liệu vào bảng SinhVien
INSERT INTO SinhVien (MaSV, TenSV, GioiTinh, NgaySinh, QueQuan, MaLop) VALUES
('SV001', N'Nguyễn Văn A', 'Nam', '2000-01-01', N'Hà Nội', 'L001'),
('SV002', N'Trần Thị B', 'Nữ', '2001-02-02', N'Hồ Chí Minh', 'L002'),
('SV003', N'Lê Văn C', 'Nam', '2002-03-03', N'Đà Nẵng', 'L001');
-- Chèn dữ liệu vào bảng Diem
INSERT INTO Diem (MaSV, MaMH, DiemL1, DiemL2) VALUES
('SV001', 'MH001', 8.5, 7.0),
('SV001', 'MH002', 9.0, 8.5),
('SV002', 'MH001', 7.0, 6.5),
('SV002', 'MH002', 8.0, 7.5);
