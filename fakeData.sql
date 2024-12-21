USE [FlowerShopDB];

-- 3. Insert data for [Packaging]
INSERT INTO Packaging (Name, Description,IsDelete)
VALUES 
(N'Bó', N'Hoa được bó đẹp với giấy gói cao cấp, phù hợp tặng sinh nhật, kỷ niệm',0),
(N'Giỏ', N'Hoa được cắm trong giỏ mây, thích hợp cho các dịp khai trương, tân gia',0),
(N'Bình', N'Hoa được cắm trong bình thủy tinh, phù hợp để bàn, trang trí nhà',0),
(N'Hộp', N'Hoa được xếp trong hộp thiết kế sang trọng, là món quà tặng độc đáo',0),
(N'Lẵng', N'Hoa được cắm trong lẵng lớn, thích hợp cho các dịp chúc mừng, sự kiện',0),
(N'Hộp Gỗ', N'Hộp gỗ tự nhiên được thiết kế tinh tế, độc đáo',0),
(N'Giỏ Mây Đan', N'Giỏ mây thủ công với kiểu đan độc đáo',0),
(N'Hộp Nhung', N'Hộp nhung sang trọng, phù hợp cho hoa cao cấp',0);

-- Insert data for [Categories]

-- Danh mục để bán
-- Thêm danh mục cha
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'CHỦ ĐỀ', NULL, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'ĐỐI TƯỢNG', NULL, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'KIỂU DÁNG', NULL, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'HOA TƯƠI', NULL, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'MÀU SẮC', NULL, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'BỘ SƯU TẬP', NULL, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'QUÀ TẶNG KÈM', NULL, 1);

-- chủ đề
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Sinh Nhật', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Khai Trương', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Chúc Mừng', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Chia Buồn', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Chúc Sức Khỏe', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tình Yêu', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Cảm Ơn', 1, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Mừng Tốt Nghiệp', 1, 1);

--đối tượng
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Người Yêu', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Bạn Bè', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Vợ', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Chồng', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Mẹ', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Trẻ Em', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Cho Nữ', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Cho Nam', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Sếp', 2, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tặng Đồng Nghiệp', 2, 1);


--kiểu dáng
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Bó Hoa Tươi', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Giỏ Hoa Tươi', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hộp Hoa Tươi', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Bình Hoa Tươi', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Thả Bình', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Lẵng Hoa Khai Trương', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Lẵng Hoa Chia Buồn', 3, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hộp Mica', 3, 1);


-- hoa tươi
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Only Rose', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Hồng', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Hướng Dương', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Đồng Tiền', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Lan Hồ Điệp', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Cẩm Chướng', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Cát Tường', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Ly', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Baby Flower', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Cúc', 4, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Sen Đá', 4, 1);

--màu sắc
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Trắng', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Đỏ', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Hồng', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Cam', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Tím', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Vàng', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Màu Xanh (Xanh Dương, Xanh Lá)', 5, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Kết Hợp Màu', 5, 1);


-- bộ sưu tập
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Cao Cấp', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Sinh Viên', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Mẫu Hoa Mới', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Khuyến Mãi', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Ngày Phụ Nữ Việt Nam (20/10)', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Ngày Nhà Giáo', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Giáng Sinh', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tết', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Sự Kiện', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hoa Tình Yêu', 6, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Ngày Quốc Tế Phụ Nữ', 6, 1);


-- quà tặng kèm
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Bánh Kem', 7, 1);
DECLARE @BanhKemSellId INT = SCOPE_IDENTITY();
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Chocolate', 7, 1);
DECLARE @ChocolateSellId INT = SCOPE_IDENTITY();
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Trái Cây', 7, 1);
DECLARE @TraiCaySellId INT = SCOPE_IDENTITY();
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Gấu Bông', 7, 1);
DECLARE @GauBongSellId INT = SCOPE_IDENTITY();
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Nến Thơm', 7, 1);
DECLARE @NenThomSellId INT = SCOPE_IDENTITY();

-- danh mục trong kho, productitem
-- HOA HỒNG
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete) 
VALUES (N'HOA HỒNG', NULL, 0, 0);
DECLARE @HoaHongId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description,IsDelete)
VALUES 
(N'Hoa Hồng Đỏ', 15000, @HoaHongId, 100, N'Hoa hồng đỏ tươi - Biểu tượng của tình yêu',0),
(N'Hoa Hồng Trắng', 20000, @HoaHongId, 100, N'Hoa hồng trắng tinh khôi - Tượng trưng cho sự thuần khiết',0),
(N'Hoa Hồng Cam', 18000, @HoaHongId, 100, N'Hoa hồng cam rực rỡ - Màu của nhiệt huyết',0),
(N'Hoa Hồng Phấn', 20000, @HoaHongId, 100, N'Hoa hồng phấn nhẹ nhàng - Vẻ đẹp dịu dàng',0),
(N'Hoa Hồng Vàng', 17000, @HoaHongId, 100, N'Hoa hồng vàng tươi sáng - Màu của sự thịnh vượng',0),
(N'Hoa Hồng Ecuador', 35000, @HoaHongId, 100, N'Hoa hồng Ecuador size đại - Sang trọng và độc đáo',0),
(N'Hoa Hồng Sapphire', 30000, @HoaHongId, 50, N'Hoa hồng xanh sapphire - Quý hiếm và đặc biệt',0),
(N'Hoa Hồng Tím Pastel', 22000, @HoaHongId, 100, N'Hoa hồng tím nhạt - Vẻ đẹp lãng mạn',0),
(N'Hoa Hồng Juliet', 28000, @HoaHongId, 80, N'Hoa hồng juliet - Màu cam đào quyến rũ',0),
(N'Hoa Hồng Ohara', 25000, @HoaHongId, 80, N'Hoa hồng ohara garden - Phong cách cổ điển',0),
(N'Hồng Trà', 16000, @HoaHongId, 100, N'Hoa hồng nâu đỏ - Hương thơm đặc trưng',0),
(N'Hồng Cabbage', 23000, @HoaHongId, 70, N'Hoa hồng bắp cải - Cánh hoa xếp lớp độc đáo',0);

-- HOA LAN
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete) 
VALUES (N'HOA LAN', NULL, 0, 0);
DECLARE @HoaLanId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description,IsDelete)
VALUES 
(N'Lan Hồ Điệp Trắng', 100000, @HoaLanId, 50, N'Lan hồ điệp trắng - Sang trọng và quý phái',0),
(N'Lan Hồ Điệp Tím', 120000, @HoaLanId, 50, N'Lan hồ điệp tím - Màu sắc hoàng gia',0),
(N'Lan Hồ Điệp Hồng', 110000, @HoaLanId, 50, N'Lan hồ điệp hồng - Nhẹ nhàng và tinh tế',0),
(N'Lan Mokara Đỏ', 80000, @HoaLanId, 50, N'Lan mokara đỏ - Màu sắc nổi bật',0),
(N'Lan Mokara Vàng', 85000, @HoaLanId, 50, N'Lan mokara vàng - Tươi sáng',0),
(N'Lan Vũ Nữ', 150000, @HoaLanId, 30, N'Lan vũ nữ - Quý phái và kiêu sa',0),
(N'Lan Dendro Berry', 90000, @HoaLanId, 40, N'Lan dendro berry - Màu tím mộng mơ',0),
(N'Lan Phi Điệp Tím', 200000, @HoaLanId, 20, N'Lan phi điệp tím - Đặc biệt quý hiếm',0),
(N'Lan Ngọc Điểm', 180000, @HoaLanId, 25, N'Lan ngọc điểm - Nhỏ xinh và thanh tao',0),
(N'Lan Cattleya', 160000, @HoaLanId, 30, N'Lan cattleya - Màu sắc rực rỡ',0);

-- HOA HƯỚNG DƯƠNG
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete) 
VALUES (N'HOA HƯỚNG DƯƠNG', NULL, 0, 0);
DECLARE @HuongDuongId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES 
(N'Hướng Dương Vàng Lớn', 25000, @HuongDuongId, 100, N'Hoa hướng dương vàng size lớn - Rực rỡ',0),
(N'Hướng Dương Vàng Nhỏ', 15000, @HuongDuongId, 150, N'Hoa hướng dương vàng size nhỏ - Xinh xắn',0),
(N'Hướng Dương Cam', 28000, @HuongDuongId, 80, N'Hoa hướng dương màu cam - Độc đáo',0),
(N'Hướng Dương Đỏ', 30000, @HuongDuongId, 70, N'Hoa hướng dương đỏ - Hiếm có',0),
(N'Hướng Dương Teddy', 35000, @HuongDuongId, 60, N'Hoa hướng dương teddy - Nhỏ xinh đáng yêu',0);

-- HOA CÚC
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete) 
VALUES (N'HOA CÚC', NULL, 0, 0);
DECLARE @HoaCucId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES 
(N'Cúc Họa Mi', 12000, @HoaCucId, 200, N'Cúc họa mi - Tinh khôi và thanh tao',0),
(N'Cúc Mẫu Đơn', 18000, @HoaCucId, 150, N'Cúc mẫu đơn - Sang trọng và quý phái',0),
(N'Cúc Ping Pong', 10000, @HoaCucId, 200, N'Cúc ping pong - Nhỏ xinh tròn trĩnh',0),
(N'Cúc Đại Đóa', 20000, @HoaCucId, 100, N'Cúc đại đóa - To và rực rỡ',0),
(N'Cúc Calimero', 15000, @HoaCucId, 150, N'Cúc calimero - Nhỏ nhắn đáng yêu',0),
(N'Cúc Rossi', 16000, @HoaCucId, 120, N'Cúc rossi - Màu hồng phấn nhẹ nhàng',0),
(N'Cúc Vàng', 12000, @HoaCucId, 200, N'Cúc vàng - Truyền thống và phổ biến',0);



-- Thêm Products
INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) 
VALUES
-- 1. Bó hoa hồng đỏ tình yêu
(N'Love Of Rose', 
N'Bó hoa hồng đỏ Ecuador 30 bông - Biểu tượng của tình yêu mãnh liệt',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11590_luxury-bloom.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11590_luxury-bloom-2.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11590_luxury-bloom-3.jpg",
  "no_img.png"]',
1, -- Bó
0),

-- 2. Bó hoa sinh nhật mix
(N'Happy Birthday Bloom',
N'Bó hoa sinh nhật kết hợp hồng và hướng dương - Tươi vui và rực rỡ',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/12479_rising-sun.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/12479_rising-sun-2.jpg",
  "no_img.png"]',
1, -- Bó
0),

-- 3. Bó hoa cao cấp
(N'Premium Love',
N'Bó hoa hồng cao cấp 40 bông kết hợp các loại hoa nhập khẩu',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/13308_premium-blend.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/13308_premium-blend-2.jpg",
  "no_img.png"]',
1, -- Bó
0)
-- Tiếp tục thêm các sản phẩm khác...

-- Thêm ProductPrices
INSERT INTO ProductPrices (ProductId, Price, Priority, StartDate, EndDate, IsDelete)
VALUES
(1, 750000, 1, NULL, NULL, 0),
(2, 650000, 1, NULL, NULL, 0),
(3, 1200000, 1, NULL, NULL, 0);

-- Thêm ProductCategories
INSERT INTO ProductCategories (ProductId, CategoryId, IsDelete)
VALUES
-- Love Of Rose
(1, 14, 0), -- Hoa Tình Yêu
(1, 16, 0), -- Hoa Tặng Người Yêu
(1, 27, 0), -- Bó Hoa Tươi
(1, 30, 0), -- Hoa Hồng
(1, 39, 0), -- Màu Đỏ
(1, 46, 0), -- Hoa Cao Cấp

-- Happy Birthday Bloom
(2, 8, 0),  -- Hoa Sinh Nhật
(2, 27, 0), -- Bó Hoa Tươi
(2, 31, 0), -- Hoa Hướng Dương
(2, 45, 0), -- Kết Hợp Màu
(2, 47, 0), -- Hoa Sinh Viên

-- Premium Love
(3, 14, 0), -- Hoa Tình Yêu
(3, 27, 0), -- Bó Hoa Tươi
(3, 30, 0), -- Hoa Hồng
(3, 46, 0); -- Hoa Cao Cấp

-- Thêm ProductProductItems
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete)
VALUES
-- Love Of Rose
(1, 6, 30, 0),  -- 30 bông hồng Ecuador

-- Happy Birthday Bloom
(2, 4, 15, 0),  -- 15 hồng phấn
(2, 25, 5, 0),  -- 5 hướng dương
(2, 31, 10, 0), -- 10 cúc calimero

-- Premium Love
(3, 6, 40, 0),  -- 40 hồng Ecuador
(3, 2, 10, 0),  -- 10 hồng trắng
(3, 8, 5, 0);   -- 5 hồng tím pastel


-- Thêm Products tiếp
INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) 
VALUES
-- 4. Giỏ hoa khai trương
(N'Thành Công', 
N'Giỏ hoa khai trương sang trọng kết hợp hồng và lan hồ điệp',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13398_success-2.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13398_success-3.jpg",
 "no_img.png"]',
2, -- Giỏ
0),

-- 5. Giỏ hoa sinh nhật
(N'Happy Day',
N'Giỏ hoa sinh nhật tươi tắn với hồng, cúc và hướng dương',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13391_happy-day.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13391_happy-day-2.jpg",
 "no_img.png"]',
2, -- Giỏ
0),

-- 6. Hộp hoa cao cấp
(N'Luxury Box',
N'Hộp hoa hồng premium với thiết kế sang trọng',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/hop-hoa/13397_elegance-bloom.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/hop-hoa/13397_elegance-bloom-2.jpg",
 "no_img.png"]',
4, -- Hộp
0);

-- Thêm ProductPrices
INSERT INTO ProductPrices (ProductId, Price, Priority, StartDate, EndDate, IsDelete)
VALUES
(4, 2500000, 1, NULL, NULL, 0),
(5, 850000, 1, NULL, NULL, 0),
(6, 1500000, 1, NULL, NULL, 0);

-- Thêm ProductCategories
INSERT INTO ProductCategories (ProductId, CategoryId, IsDelete)
VALUES
-- Thành Công
(4, 9, 0),   -- Hoa Khai Trương
(4, 28, 0),  -- Giỏ Hoa Tươi
(4, 33, 0),  -- Lan Hồ Điệp
(4, 46, 0),  -- Hoa Cao Cấp
(4, 56, 0),  -- Hoa Sự Kiện

-- Happy Day
(5, 8, 0),   -- Hoa Sinh Nhật
(5, 28, 0),  -- Giỏ Hoa Tươi
(5, 30, 0),  -- Hoa Hồng
(5, 31, 0),  -- Hoa Hướng Dương
(5, 45, 0),  -- Kết Hợp Màu

-- Luxury Box
(6, 29, 0),  -- Hộp Hoa Tươi
(6, 30, 0),  -- Hoa Hồng
(6, 46, 0);  -- Hoa Cao Cấp

-- Thêm ProductProductItems
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete)
VALUES
-- Thành Công
(4, 6, 24, 0),   -- 24 hồng Ecuador
(4, 13, 3, 0),   -- 3 lan hồ điệp tím
(4, 14, 3, 0),   -- 3 lan hồ điệp hồng

-- Happy Day
(5, 4, 20, 0),   -- 20 hồng phấn
(5, 25, 3, 0),   -- 3 hướng dương
(5, 31, 10, 0),  -- 10 cúc calimero

-- Luxury Box
(6, 6, 36, 0),   -- 36 hồng Ecuador
(6, 2, 12, 0);   -- 12 hồng trắng


INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) 
VALUES
-- 7. Bó hoa sinh viên
(N'Sweet Day', 
N'Bó hoa nhỏ xinh với hướng dương và cúc họa mi, phù hợp sinh viên',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11569_sunshine.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11569_sunshine-2.jpg",
 "no_img.png"]',
1, 0),

-- 8. Lẵng hoa chúc mừng
(N'Congratulations', 
N'Lẵng hoa chúc mừng khai trương hoành tráng với lan và hồng',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/lang-hoa/13372_successful-2.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/lang-hoa/13372_successful-3.jpg",
 "no_img.png"]',
5, 0),

-- 9. Bình hoa để bàn
(N'Office Charm',
N'Bình hoa tươi trang trí văn phòng với cúc và hồng',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/binh-hoa/13266_blushing-room.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/binh-hoa/13266_blushing-room-2.jpg",
 "no_img.png"]',
3, 0);

INSERT INTO ProductPrices (ProductId, Price, Priority, StartDate, EndDate, IsDelete)
VALUES
(7, 350000, 1, NULL, NULL, 0),
(8, 2800000, 1, NULL, NULL, 0),
(9, 750000, 1, NULL, NULL, 0);

INSERT INTO ProductCategories (ProductId, CategoryId, IsDelete)
VALUES
-- Sweet Day
(7, 47, 0), -- Hoa Sinh Viên
(7, 31, 0), -- Hoa Hướng Dương
(7, 27, 0), -- Bó Hoa Tươi
(7, 48, 0), -- Mẫu Hoa Mới

-- Congratulations
(8, 9, 0),  -- Hoa Khai Trương
(8, 33, 0), -- Lẵng Hoa 
(8, 46, 0), -- Hoa Cao Cấp
(8, 45, 0), -- Kết Hợp Màu

-- Office Charm
(9, 30, 0), -- Bình Hoa Tươi
(9, 37, 0), -- Hoa Cúc
(9, 25, 0); -- Hoa Tặng Đồng Nghiệp

INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete)
VALUES
-- Sweet Day
(7, 25, 3, 0),   -- 3 hướng dương
(7, 28, 10, 0),  -- 10 cúc họa mi

-- Congratulations  
(8, 6, 50, 0),   -- 50 hồng Ecuador
(8, 11, 5, 0),   -- 5 lan hồ điệp trắng
(8, 13, 5, 0),   -- 5 lan hồ điệp tím

-- Office Charm
(9, 4, 12, 0),   -- 12 hồng phấn
(9, 31, 10, 0),  -- 10 cúc calimero
(9, 33, 5, 0);   -- 5 cúc đại đóa



-- Thêm danh mục trong kho cho quà tặng 
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete)
VALUES (N'QUÀ TẶNG KÈM-KHO', NULL, 0, 0);
DECLARE @QuaTangKem_KhoId INT = SCOPE_IDENTITY();

INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete)
VALUES (N'BÁNH KEM', @QuaTangKem_KhoId, 0, 0);
DECLARE @BanhKemId INT = SCOPE_IDENTITY();

INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete)
VALUES (N'CHOCOLATE', @QuaTangKem_KhoId, 0, 0);
DECLARE @ChocolateId INT = SCOPE_IDENTITY();

INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete)
VALUES (N'GẤU BÔNG', @QuaTangKem_KhoId, 0, 0);
DECLARE @GauBongId INT = SCOPE_IDENTITY();

-- Thêm ProductItems cho từng loại
INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
-- Bánh kem
(N'Bánh Kem Chocolate', 150000, @BanhKemId, 50, N'Bánh kem socola cao cấp - 20cm', 0);
DECLARE @BanhKemChocolateId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Bánh Red Velvet', 180000, @BanhKemId, 30, N'Bánh red velvet thơm ngon - 20cm', 0);
DECLARE @BanhRedVelvetId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Bánh Kem Vanilla', 150000, @BanhKemId, 50, N'Bánh kem vanilla truyền thống - 20cm', 0);
DECLARE @BanhVanillaId INT = SCOPE_IDENTITY();

-- Chocolate
INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES 
(N'Ferrero Rocher 16 viên', 180000, @ChocolateId, 100, N'Hộp chocolate Ferrero Rocher 16 viên', 0);
DECLARE @FerreroId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Chocolate Godiva', 350000, @ChocolateId, 50, N'Hộp chocolate Godiva cao cấp', 0);
DECLARE @GodivaId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Chocolate Lindor', 250000, @ChocolateId, 80, N'Hộp chocolate Lindor thượng hạng', 0);
DECLARE @LindorId INT = SCOPE_IDENTITY();

-- Gấu bông
INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Gấu Teddy 30cm', 120000, @GauBongId, 100, N'Gấu bông teddy màu nâu - 30cm', 0);
DECLARE @TeddyId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Gấu Love 25cm', 100000, @GauBongId, 100, N'Gấu bông ôm tim Love you - 25cm', 0);
DECLARE @LoveId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Thỏ Bông 35cm', 150000, @GauBongId, 80, N'Thỏ bông dễ thương - 35cm', 0);
DECLARE @RabbitId INT = SCOPE_IDENTITY();

-- Sử dụng SCOPE_IDENTITY() để lấy ID tăng dần
INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Premium Chocolate Collection',
N'Bộ sưu tập chocolate cao cấp Godiva và Lindor',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/so-co-la-d-art/12752_chocolate-truffle-nau-9.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId4 INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Bunny Sweet Box',
N'Set quà thỏ bông xinh xắn và Ferrero Rocher',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/gau-bong/13043_bup-be-non-phu-thuy.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId5 INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Double Love Bears',
N'Cặp gấu Teddy và Love siêu dễ thương',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/gau-bong/13273_mini-teddy-bear.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId6 INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Chocolate Paradise',
N'Combo 3 loại chocolate cao cấp nhất',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/so-co-la-d-art/12893_chocolatle-big-box-36.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId7 INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Sweet Cake Duo',
N'Bộ đôi bánh kem chocolate và vanilla',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/banh-kem-brodard/6862_banh-kem-sua-tuoi-20cm-m13.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId8 INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Love Bear Cake Set',
N'Set quà gấu Love và bánh Red Velvet',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/banh-kem-brodard/15518_banh-passio-cheese-mousse-16cm.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId9 INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Cake White',
N'Combo bánh kem, chocolate và gấu bông cao cấp',
1,
'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/banh-kem-brodard/15516_banh-kem-trai-cay-sua-tuoi-20cm-m14.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductId10 INT = SCOPE_IDENTITY();

-- Insert ProductPrices
INSERT INTO ProductPrices (ProductId, Price, Priority, StartDate, EndDate, IsDelete) VALUES
(@ProductId4, 799000, 1, NULL, NULL, 0),
(@ProductId5, 449000, 1, NULL, NULL, 0),
(@ProductId6, 399000, 1, NULL, NULL, 0),
(@ProductId7, 899000, 1, NULL, NULL, 0),
(@ProductId8, 499000, 1, NULL, NULL, 0),
(@ProductId9, 599000, 1, NULL, NULL, 0),
(@ProductId10, 999000, 1, NULL, NULL, 0);

-- Insert ProductCategories
INSERT INTO ProductCategories (ProductId, CategoryId, IsDelete) VALUES
(@ProductId4, @ChocolateSellId, 0),
(@ProductId5, @ChocolateSellId, 0),
(@ProductId5, @GauBongSellId , 0),
(@ProductId6, @GauBongSellId , 0),
(@ProductId7, @ChocolateSellId, 0),
(@ProductId8, @BanhKemSellId , 0),
(@ProductId9, @BanhKemSellId , 0),
(@ProductId9, @GauBongSellId , 0),
(@ProductId10, @BanhKemSellId , 0),
(@ProductId10, @ChocolateSellId, 0),
(@ProductId10, @GauBongSellId , 0);

-- Insert ProductProductItems
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductId4, @GodivaId, 1, 0),
(@ProductId4, @LindorId, 1, 0),
(@ProductId5, @RabbitId, 1, 0),
(@ProductId5, @FerreroId, 1, 0),
(@ProductId6, @TeddyId, 1, 0),
(@ProductId6, @LoveId, 1, 0),
(@ProductId7, @GodivaId, 1, 0),
(@ProductId7, @LindorId, 1, 0),
(@ProductId7, @FerreroId, 1, 0),
(@ProductId8, @BanhKemChocolateId, 1, 0),
(@ProductId8, @BanhVanillaId, 1, 0),
(@ProductId9, @LoveId, 1, 0),
(@ProductId9, @BanhRedVelvetId, 1, 0),
(@ProductId10, @BanhKemChocolateId, 1, 0),
(@ProductId10, @GodivaId, 1, 0),
(@ProductId10, @TeddyId, 1, 0);




-- Thêm danh mục TRÁI CÂY và NẾN THƠM trong kho
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete)
VALUES (N'TRÁI CÂY', @QuaTangKem_KhoId, 0, 0);
DECLARE @TraiCayKhoId INT = SCOPE_IDENTITY();

INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell, IsDelete)
VALUES (N'NẾN THƠM', @QuaTangKem_KhoId, 0, 0);
DECLARE @NenThomKhoId INT = SCOPE_IDENTITY();


-- Thêm sản phẩm vào kho Trái Cây
INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Táo Đỏ', 120000, @TraiCayKhoId, 50, N'Táo đỏ nhập khẩu tươi ngon', 0);
DECLARE @TaoDoId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Cam Vàng', 150000, @TraiCayKhoId, 40, N'Cam vàng thơm ngon, cung cấp vitamin C', 0);
DECLARE @CamVangId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Nho Đen', 200000, @TraiCayKhoId, 30, N'Nho đen không hạt nhập khẩu', 0);
DECLARE @NhoDenId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Dâu Tây', 250000, @TraiCayKhoId, 20, N'Dâu tây tươi Đà Lạt, đóng hộp đẹp mắt', 0);
DECLARE @DauTayId INT = SCOPE_IDENTITY();


-- Thêm sản phẩm vào kho Nến Thơm
INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Nến Thơm Lavender', 120000, @NenThomKhoId, 50, N'Nến thơm hương Lavender thư giãn', 0);
DECLARE @LavenderId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Nến Thơm Vani', 100000, @NenThomKhoId, 40, N'Nến thơm hương Vani ngọt ngào', 0);
DECLARE @VaniId INT = SCOPE_IDENTITY();

INSERT INTO ProductItems (Name, ImportPrice, CategoryId, Quantity, Description, IsDelete)
VALUES
(N'Nến Thơm Cam', 110000, @NenThomKhoId, 20, N'Nến thơm hương cam tươi mát', 0);
DECLARE @CamId INT = SCOPE_IDENTITY();


-- Sản phẩm để bán từ kho Trái Cây
INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Hộp Táo Đỏ',
N'Hộp quà trái cây với táo đỏ nhập khẩu tươi ngon',
1,
'["https://bizweb.dktcdn.net/100/107/356/products/ho-p-ta-o-do-ha-n-quo-c-samsung-1k.jpg?v=1598241579597", "no_img.png"]',
4, 0);
DECLARE @ProductTaoDoId INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Hộp Cam Vàng',
N'Hộp quà trái cây với cam vàng thơm ngon, bổ dưỡng',
1,
'["https://www.lottemart.vn/media/catalog/product/cache/0x0/0/4/0400229220003-3.jpg.webp", "no_img.png"]',
4, 0);
DECLARE @ProductCamVangId INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Hộp Nho Đen',
N'Hộp quà trái cây với nho đen không hạt nhập khẩu',
1,
'["https://www.lottemart.vn/media/catalog/product/cache/0x0/0/7/0770795264146-1.jpg.webp", "no_img.png"]',
4, 0);
DECLARE @ProductNhoDenId INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Hộp Dâu Tây',
N'Hộp quà trái cây với dâu tây tươi từ Đà Lạt',
1,
'["https://dalatfarm.net/wp-content/uploads/2022/01/dau-nhat-mix-nhieu-loai.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductDauTayId INT = SCOPE_IDENTITY();



-- Sản phẩm để bán từ kho Nến Thơm
INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Nến Lavender',
N'Nến thơm hương Lavender thư giãn, cao cấp',
1,
'["https://natudar.com/wp-content/uploads/2024/08/nen-thom-oai-huong-lavender-img-1.jpg", "no_img.png"]',
4, 0);
DECLARE @ProductLavenderId INT = SCOPE_IDENTITY();

INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Nến Vani',
N'Nến thơm hương Vani ngọt ngào, lãng mạn',
1,
'["https://file.hstatic.net/200000455983/file/huong-vani_5ce16635b8d7422ea5e6666b64f6ff5b_grande.png", "no_img.png"]',
4, 0);
DECLARE @ProductVaniId INT = SCOPE_IDENTITY();


INSERT INTO Products (Title, Description, Quantity, Images, PackagingId, IsDelete) VALUES
(N'Nến Cam',
N'Nến thơm hương cam tươi mát, dễ chịu',
1,
'["https://bizweb.dktcdn.net/100/419/636/products/cam-que.jpg?v=1712983395237", "no_img.png"]',
4, 0);
DECLARE @ProductCamId INT = SCOPE_IDENTITY();



-- Gán giá cho các sản phẩm Trái Cây
INSERT INTO ProductPrices (ProductId, Price, Priority, StartDate, EndDate, IsDelete) VALUES
(@ProductTaoDoId, 150000, 1, NULL, NULL, 0),
(@ProductCamVangId, 180000, 1, NULL, NULL, 0),
(@ProductNhoDenId, 200000, 1, NULL, NULL, 0),
(@ProductDauTayId, 250000, 1, NULL, NULL, 0);

-- Gán giá cho các sản phẩm Nến Thơm
INSERT INTO ProductPrices (ProductId, Price, Priority, StartDate, EndDate, IsDelete) VALUES
(@ProductLavenderId, 120000, 1, NULL, NULL, 0),
(@ProductVaniId, 100000, 1, NULL, NULL, 0),
(@ProductCamId, 110000, 1, NULL, NULL, 0);



-- Gán sản phẩm vào danh mục Trái Cây
INSERT INTO ProductCategories (ProductId, CategoryId, IsDelete) VALUES
(@ProductTaoDoId, @TraiCaySellId, 0),
(@ProductCamVangId, @TraiCaySellId, 0),
(@ProductNhoDenId, @TraiCaySellId, 0),
(@ProductDauTayId, @TraiCaySellId, 0);

-- Gán sản phẩm vào danh mục Nến Thơm
INSERT INTO ProductCategories (ProductId, CategoryId, IsDelete) VALUES
(@ProductLavenderId, @NenThomSellId, 0),
(@ProductVaniId, @NenThomSellId, 0),
(@ProductCamId, @NenThomSellId, 0);

-- Gán ProductItem cho Hộp Táo Đỏ
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductTaoDoId, @TaoDoId, 1, 0);

-- Gán ProductItem cho Hộp Cam Vàng
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductCamVangId, @CamVangId, 1, 0);

-- Gán ProductItem cho Hộp Nho Đen
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductNhoDenId, @NhoDenId, 1, 0);

-- Gán ProductItem cho Hộp Dâu Tây
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductDauTayId, @DauTayId, 1, 0);

-- Gán ProductItem cho Nến Lavender
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductLavenderId, @LavenderId, 1, 0);

-- Gán ProductItem cho Nến Vani
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductVaniId, @VaniId, 1, 0);


-- Gán ProductItem cho Nến Cam
INSERT INTO ProductProductItems (ProductId, ProductItemId, Quantity, IsDelete) VALUES
(@ProductCamId, @CamId, 1, 0);

--bảng tham số
INSERT INTO ParameterConfigurations(AllowedFeedbackDay,ShippingCostPerKilometer) VALUES
(7,5000);

