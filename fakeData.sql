USE [FlowerShopDB];

-- 1. Insert data for [Roles]
INSERT INTO [dbo].[Roles] (Id, Name, NormalizedName, ConcurrencyStamp)
VALUES 
('1', 'Admin', 'ADMIN', NEWID()),
('2', 'Customer', 'CUSTOMER', NEWID()),
('3', 'Staff', 'STAFF', NEWID()),
('4', 'Supplier', 'SUPPLIER', NEWID()),
('5', 'Guest', 'GUEST', NEWID());

-- 2. Insert data for [Carts]
INSERT INTO [dbo].[Carts] DEFAULT VALUES;
INSERT INTO [dbo].[Carts] DEFAULT VALUES;
INSERT INTO [dbo].[Carts] DEFAULT VALUES;
INSERT INTO [dbo].[Carts] DEFAULT VALUES;
INSERT INTO [dbo].[Carts] DEFAULT VALUES;

-- 3. Insert data for [Packaging]
INSERT INTO [dbo].[Packaging] (Name, Description, IsDelete)
VALUES 
('Standard Wrap', 'Standard flower wrap', 0),
('Gift Wrap', 'Gift wrap for special occasions', 0),
('Box', 'Box packaging', 0),
('Luxury Wrap', 'Luxury wrap for premium gifts', 0),
('Eco-Friendly Wrap', 'Environmentally friendly wrap', 0);

-- 4. Insert data for [PaymentMethods]
INSERT INTO [dbo].[PaymentMethods] (Name, Description, Price, Status, IsDelete)
VALUES 
('Credit Card', 'Payment via credit card', 0, 1, 0),
('PayPal', 'Payment via PayPal', 0, 1, 0),
('Bank Transfer', 'Payment via bank transfer', 0, 1, 0),
('Cash', 'Payment in cash', 0, 1, 0),
('Gift Card', 'Payment via gift card', 0, 1, 0);

-- 5. Insert data for [Categories]
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
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Bánh Kem Tous Les Jours', 7, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Bánh Kem Brodard', 7, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Chocolate', 7, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Trái Cây', 7, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Gấu Bông', 7, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Nến Thơm', 7, 1);
INSERT INTO Categories (Name, ParentCategoryId, IsCategorySell) VALUES (N'Hamper', 7, 1);




-- 6. Insert data for [Users]
INSERT INTO [dbo].[Users] (Id, FullName, BirthDay, IsLock, IsDelete, CartId, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES 
('101', 'John Doe', '1990-01-01',  0, 0, 1, 'johndoe', 'JOHNDOE', 'john@example.com', 'JOHN@EXAMPLE.COM', 1, 1, 0, 0, 0),
('102', 'Jane Smith', '1992-02-02', 0, 0, 2, 'janesmith', 'JANESMITH', 'jane@example.com', 'JANE@EXAMPLE.COM', 1, 1, 0, 0, 0),
('103', 'Alice Johnson', '1985-03-03',  0, 0, 3, 'alicejohnson', 'ALICEJOHNSON', 'alice@example.com', 'ALICE@EXAMPLE.COM', 1, 1, 0, 0, 0),
('104', 'Bob Brown', '1978-04-04',  0, 0, 4, 'bobbrown', 'BOBBROWN', 'bob@example.com', 'BOB@EXAMPLE.COM', 1, 1, 0, 0, 0),
('105', 'Charlie Green', '1995-05-05',  0, 0, 5, 'charliegreen', 'CHARLIEGREEN', 'charlie@example.com', 'CHARLIE@EXAMPLE.COM', 1, 1, 0, 0, 0);

-- 7. Insert data for [Addresses]
INSERT INTO [dbo].[Addresses] (Description, Phone, IsDelete, AppUserId)
VALUES 
('123 Main St, City A', '123-456-7890', 0, '101'),
('456 Oak St, City B', '234-567-8901', 0, '102'),
('789 Pine St, City C', '345-678-9012', 0, '103'),
('101 Maple St, City D', '456-789-0123', 0, '104'),
('202 Birch St, City E', '567-890-1234', 0, '105');


-- 9. Insert data for [ProductItems] (Phụ thuộc vào Categories)
INSERT INTO [dbo].[ProductItems] (Name, ImportPrice, CategoryId, Description, IsDelete,Quantity)
VALUES 
('Single Rose', 5, 3, 'A single red rose', 0,99),
('Single Tulip', 4, 4,  'A single tulip', 0,3232),
('Dark Chocolate', 10, 5, 'Dark chocolate piece', 0,54354),
('Milk Chocolate', 8, 5,  'Milk chocolate piece', 0,43),
('Orchid Plant', 20, 1,  'Single orchid plant', 0,3);

-- 10. Insert data for [CartDetails] (Phụ thuộc vào Products và Carts đã có)
INSERT INTO [dbo].[CartDetails] (CartId, ProductId, Quantity)
VALUES 
(1, 1, 2),
(2, 2, 1),
(3, 3, 3),
(4, 4, 1),
(5, 5, 2);

-- 11. Insert data for [SaleInvoices] (Phụ thuộc vào Users và PaymentMethods)
INSERT INTO [dbo].[SaleInvoices] (CreateDay, CustomerId, PaymentMethodId, Status, IsDelete)
VALUES 
(GETDATE(), '101', 1, 1, 0),
(GETDATE(), '102', 2, 1, 0),
(GETDATE(), '103', 3, 1, 0),
(GETDATE(), '104', 4, 1, 0),
(GETDATE(), '105', 5, 1, 0);

-- 12. Insert data for [SaleInvoiceDetails] (Phụ thuộc vào SaleInvoices và Products)
INSERT INTO [dbo].[SaleInvoiceDetails] (SaleInvoiceId, ProductId, Quantity, UnitPrice, IsDelete)
VALUES 
(1, 1, 2, 15, 0),
(2, 2, 1, 10, 0),
(3, 3, 3, 25, 0),
(4, 4, 1, 20, 0),
(5, 5, 2, 30, 0);
