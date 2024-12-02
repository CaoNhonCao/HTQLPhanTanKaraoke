CREATE DATABASE Karaoke;
GO
USE Karaoke;
GO

-- 1. Bảng Chi nhánh (Branch)
CREATE TABLE ChiNhanh (
    MaChiNhanh CHAR(5) PRIMARY KEY, -- Khóa chính
    TenChiNhanh NVARCHAR(255), -- Tên chi nhánh
    DiaChi NVARCHAR(255), -- Địa chỉ chi nhánh
    SoDienThoai NVARCHAR(20) -- Số điện thoại
);

-- 2. Bảng Khách hàng (Customer)
CREATE TABLE KhachHang (
    MaKhachHang CHAR(10) Not null, -- Mã khách hàng
    HoTen NVARCHAR(100), -- Họ và tên
    SoDienThoai NVARCHAR(15), -- Số điện thoại
    Email NVARCHAR(100), -- Email
    NgaySinh DATE, -- Ngày sinh
    DiaChi NVARCHAR(255), -- Địa chỉ
    GioiTinh NVARCHAR(10), -- Giới tính
    MaChiNhanh CHAR(5), -- Khóa ngoại
    LoaiKhachHang NVARCHAR(20) DEFAULT 'Thường', -- Loại khách hàng
    GhiChu NVARCHAR(255), -- Ghi chú
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
	primary key (MaKhachHang)
);
-- 3. Bảng Nhân viên (Employee)
CREATE TABLE NhanVien (
    MaNhanVien CHAR(10) not null, -- Mã nhân viên
    HoTen NVARCHAR(100), -- Họ và tên
    SoDienThoai NVARCHAR(15), -- Số điện thoại
    Email NVARCHAR(100), -- Email
    ChucVu NVARCHAR(50), -- Chức vụ
    LuongCoBan DECIMAL(10, 2), -- Lương cơ bản
    NgayVaoLam DATE, -- Ngày vào làm
    MaChiNhanh CHAR(5), -- Khóa ngoại
    GhiChu NVARCHAR(255), -- Ghi chú
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
	PRIMARY KEY (MaNhanVien)
);

-- 4. Bảng Phòng hát (Room)
CREATE TABLE PhongHat (
    MaPhong CHAR(10) not null, -- Mã phòng hát
    SoPhong NVARCHAR(10), -- Số phòng hát
    LoaiPhong NVARCHAR(50), -- Loại phòng
    SucChua INT, -- Sức chứa
    GiaTheoGio DECIMAL(10, 2), -- Giá thuê theo giờ
    TrangThai NVARCHAR(20), -- Trạng thái
    MaChiNhanh CHAR(5), -- Khóa ngoại
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
	PRIMARY KEY (MaPhong)
);

-- 5. Bảng Dịch vụ (Service)
CREATE TABLE DichVu (
    MaDichVu CHAR(10) not null,
    TenDichVu NVARCHAR(100), -- Tên dịch vụ
    GiaDichVu DECIMAL(10, 2) CHECK (GiaDichVu >= 0), -- Giá dịch vụ
    MaChiNhanh CHAR(5), -- Khóa ngoại
    GhiChu NVARCHAR(255), -- Ghi chú
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
	Primary key (MaDichVu),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

CREATE TABLE DichVuChoPhong (
	ID char(10) not null,
    MaPhong char(10) NOT NULL,
    MaDichVu char(10) NOT NULL,
	MaChiNhanh CHAR(5),
    SoLuong INT, 
    NgaySuDung DATETIME,
	TrangThai bit,
	ThanhTien decimal(18,2),
    CONSTRAINT PK_Phong_DichVu PRIMARY KEY (ID, MaPhong, MaDichVu),
    CONSTRAINT FK_PhongHat FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong),
    CONSTRAINT FK_DichVu FOREIGN KEY (MaDichVu) REFERENCES DichVu(MaDichVu)
);

-- 6. Bảng Hóa đơn (Invoice)
CREATE TABLE HoaDon (
    MaHoaDon CHAR(10) PRIMARY KEY, -- Mã hóa đơn
    MaKhachHang CHAR(10), -- Khóa ngoại
    MaPhong CHAR(10), -- Khóa ngoại
    NgayLapHoaDon DATE, -- Ngày lập
    TongTien DECIMAL(10, 2), -- Tổng tiền
    ThanhToan NVARCHAR(10), -- Trạng thái thanh toán
    GiamGia DECIMAL(18, 2) DEFAULT 0, -- Giảm giá
    GhiChu NVARCHAR(255), -- Ghi chú
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong)
);

-- 8. Bảng Đặt phòng (Booking)
CREATE TABLE DatPhong (
    MaDatPhong CHAR(10) PRIMARY KEY, -- Mã đặt phòng
    MaKhachHang CHAR(10), -- Khóa ngoại
    MaPhong CHAR(10), -- Khóa ngoại
    ThoiGianBatDau DATETIME, -- Thời gian bắt đầu
    ThoiGianKetThuc DATETIME, -- Thời gian kết thúc
    TinhTrang NVARCHAR(20), -- Tình trạng
    GhiChu NVARCHAR(255), -- Ghi chú
    NgayTao DATETIME DEFAULT GETDATE(), -- Ngày tạo
    NgayCapNhat DATETIME DEFAULT GETDATE(), -- Ngày cập nhật
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong)
);

-- 9. Bảng Bảng lương (Payroll)
CREATE TABLE BangLuong (
    MaBangLuong CHAR(10) PRIMARY KEY, -- Mã bảng lương
    MaNhanVien CHAR(10), -- Khóa ngoại
    ThangTinhLuong Date, -- Tháng tính lương
    SoGioLam int,
    LuongThucLanh DECIMAL(10, 2), -- Lương thực lãnh
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);


-- 10. Bảng Điểm danh (Attendance)
CREATE TABLE DiemDanh (
    MaDiemDanh Char(10) PRIMARY KEY, 
    MaNhanVien CHAR(10), -- Khóa ngoại
    NgayDiemDanh DATE, -- Ngày điểm danh
    ThoiGianDiLam DATETIME, -- Thời gian đến
    ThoiGianVeLam DATETIME, -- Thời gian về
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE LichSuNhanLuong (
    MaNhanVien Char(10),
    Thang INT,
    Nam INT,
    TongGioLam DECIMAL(18,2),
    TongLuong DECIMAL(18,2),
    TrangThaiNhanLuong BIT,
    PRIMARY KEY(MaNhanVien, Thang, Nam),
    FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- 11. Bảng Đánh giá khách hàng (CustomerReview)
CREATE TABLE DanhGiaKhachHang (
    MaKhachHang CHAR(10), -- Khóa ngoại
    MaPhong CHAR(10),
    NoiDung NVARCHAR(500), -- Nội dung đánh giá
    DiemDanhGia INT CHECK (DiemDanhGia BETWEEN 1 AND 5), -- Điểm đánh giá
    NgayDanhGia DATE, -- Ngày đánh giá
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong)
);

-- 12. Bảng Khuyến mãi (Promotion)
CREATE TABLE KhuyenMai (
    MaKhuyenMai CHAR(10) PRIMARY KEY, -- Mã khuyến mãi
	MaKhachHang CHAR(10),
    TenKhuyenMai NVARCHAR(100), -- Tên khuyến mãi
    PhanTramGiam DECIMAL(5, 2), -- Phần trăm giảm giá
    MaChiNhanh CHAR(5), -- Khóa ngoại
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
);

-- 13. Bảng Doanh thu (Revenue)
CREATE TABLE DoanhThu (
    MaDoanhThu CHAR(10) PRIMARY KEY, -- Mã doanh thu
    Thang DATE, -- Tháng
    TongDoanhThu DECIMAL(18, 2), -- Tổng doanh thu
    TongChiPhi DECIMAL(18, 2), -- Tổng chi phí
    LoiNhuan DECIMAL(18, 2), -- Lợi nhuận
    GhiChu NVARCHAR(255), -- Ghi chú
    MaChiNhanh CHAR(5), -- Khóa ngoại
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 14. Bảng nhà cung cấp
CREATE TABLE NhaCungCap (
	MaNhaCungCap CHAR(10) PRIMARY KEY,
	TenNhaCungCap NVARCHAR(100),
	DiaChi NVARCHAR(250),
	SoDienThoai Char(15),
	MaChiNhanh char(5),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 15. Bảng Sản phẩm (Product)
CREATE TABLE SanPham (
    MaSanPham CHAR(10) PRIMARY KEY, -- Mã sản phẩm
    TenSanPham NVARCHAR(100), -- Tên sản phẩm
    LoaiSanPham NVARCHAR(50), -- Loại sản phẩm
    DonViTinh NVARCHAR(50), -- Đơn vị tính
    GiaBan DECIMAL(18, 2), -- Giá bán
    MaChiNhanh CHAR(5), -- Khóa ngoại
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 16. Bảng Nhập hàng (ImportOrder)
CREATE TABLE NhapHang (
    MaNhapHang CHAR(10) PRIMARY KEY,
	MaSanPham CHAR(10),
    NgayNhapHang DATE,
    SoLuong INT, -- Số lượng
    DonGia DECIMAL(18, 2), -- Đơn giá nhập
    ThanhTien DECIMAL(18, 2), -- Thành tiền
    MaNhaCungCap CHAR(10), 
    MaChiNhanh CHAR(5),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
);

-- 17. Bảng Chi phí khác (Expenses)
CREATE TABLE ChiPhiKhac (
    MaChiPhi CHAR(10) PRIMARY KEY, -- Mã chi phí
    TenChiPhi NVARCHAR(100), -- Tên chi phí
    SoTien DECIMAL(18, 2) CHECK (SoTien >= 0), -- Số tiền
    NgayChi DATE, -- Ngày phát sinh
    GhiChu NVARCHAR(255), -- Ghi chú
    MaChiNhanh CHAR(5), -- Khóa ngoại
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 18. Bảng Quản lý kho (Inventory)
CREATE TABLE QuanLyKho (
    MaSanPham CHAR(10), -- Khóa ngoại
    SoLuongTon INT, -- Số lượng tồn
	MaChiNhanh CHAR(5),
    FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 19. Bảng Lịch sử bảo trì (MaintenanceHistory)
CREATE TABLE LichSuBaoTri (
    MaBaoTri CHAR(10) PRIMARY KEY, -- Mã bảo trì
    MaPhong CHAR(10), -- Khóa ngoại
    NgayBaoTri DATE, -- Ngày bảo trì
    MoTaBaoTri NVARCHAR(255), -- Mô tả
    ChiPhiBaoTri DECIMAL(18, 2), -- Chi phí bảo trì
    GhiChu NVARCHAR(255), -- Ghi chú
	MaChiNhanh CHAR(5),
    FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong),
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

-- 20. Bảng Hoạt động hệ thống (SystemActivity)
CREATE TABLE HoatDongHeThong (
    MaHoatDong CHAR(10) PRIMARY KEY, -- Mã hoạt động
    TenNhanVien NVARCHAR(100), -- Nhân viên thực hiện
    MoTaHoatDong NVARCHAR(255), -- Mô tả hoạt động
    NgayThucHien DATETIME, -- Thời gian thực hiện
    MaChiNhanh CHAR(5), -- Khóa ngoại
    FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
);

CREATE TABLE DonHang (
	MaDonHang char(10),
	MaPhong char(10),
	NgayTao date,
	TongTien decimal(18, 2),
	TrangThai bit,
	GhiChu nvarchar(255),
	primary key (MaDonHang),
    FOREIGN KEY (MaPhong) REFERENCES PhongHat(MaPhong)
);

CREATE TABLE ChiTietDonHang (
	MaDonHang char(10),
	MaSanPham char(10),
	SoLuong int,
	DonGia decimal(18, 2),
	ThanhTien decimal(18, 2),
	primary key (MaDonHang, MaSanPham),
    FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
	FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);
-- Thêm chỉ mục cho các trường thường xuyên được tìm kiếm
CREATE INDEX IDX_KhachHang ON KhachHang(MaKhachHang);
CREATE INDEX IDX_NhanVien ON NhanVien(MaNhanVien);
CREATE INDEX IDX_PhongHat ON PhongHat(MaPhong);
CREATE INDEX IDX_DichVu ON DichVu(MaDichVu);
CREATE INDEX IDX_HoaDon ON HoaDon(MaHoaDon);
CREATE INDEX IDX_DatPhong ON DatPhong(MaDatPhong);
CREATE INDEX IDX_ChiPhiKhac ON ChiPhiKhac(MaChiPhi);
CREATE INDEX IDX_LichSuBaoTri ON LichSuBaoTri(MaBaoTri);
CREATE INDEX IDX_DoanhThu ON DoanhThu(MaDoanhThu);
