﻿USE [FlowerShopDB];

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
INSERT INTO [dbo].[Categories] (Name, ParentCategoryId, IsCategorySell)
VALUES 
('Flowers', NULL, 1),
('Gifts', NULL, 1),
('Roses', 1, 1),
('Tulips', 1, 1),
('Chocolates', 2, 1);

-- 6. Insert data for [Users]
INSERT INTO [dbo].[Users] (Id, FullName, BirthDay, Images, IsLock, IsDelete, CartId, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES 
('101', 'John Doe', '1990-01-01', '[\"no_img.png\"]', 0, 0, 1, 'johndoe', 'JOHNDOE', 'john@example.com', 'JOHN@EXAMPLE.COM', 1, 1, 0, 0, 0),
('102', 'Jane Smith', '1992-02-02', '[\"no_img.png\"]', 0, 0, 2, 'janesmith', 'JANESMITH', 'jane@example.com', 'JANE@EXAMPLE.COM', 1, 1, 0, 0, 0),
('103', 'Alice Johnson', '1985-03-03', '[\"no_img.png\"]', 0, 0, 3, 'alicejohnson', 'ALICEJOHNSON', 'alice@example.com', 'ALICE@EXAMPLE.COM', 1, 1, 0, 0, 0),
('104', 'Bob Brown', '1978-04-04', '[\"no_img.png\"]', 0, 0, 4, 'bobbrown', 'BOBBROWN', 'bob@example.com', 'BOB@EXAMPLE.COM', 1, 1, 0, 0, 0),
('105', 'Charlie Green', '1995-05-05', '[\"no_img.png\"]', 0, 0, 5, 'charliegreen', 'CHARLIEGREEN', 'charlie@example.com', 'CHARLIE@EXAMPLE.COM', 1, 1, 0, 0, 0);

-- 7. Insert data for [Addresses]
INSERT INTO [dbo].[Addresses] (Description, Phone, IsDelete, AppUserId)
VALUES 
('123 Main St, City A', '123-456-7890', 0, '101'),
('456 Oak St, City B', '234-567-8901', 0, '102'),
('789 Pine St, City C', '345-678-9012', 0, '103'),
('101 Maple St, City D', '456-789-0123', 0, '104'),
('202 Birch St, City E', '567-890-1234', 0, '105');

-- 8. Insert data for [Products] (Phụ thuộc vào Packaging đã có)
INSERT INTO [dbo].[Products] (Title, Description, Images, PackagingId, IsDelete)
VALUES 
('Rose Bouquet', 'A bouquet of red roses', '[\"no_img.png\"]', 1, 0),
('Tulip Bouquet', 'A bouquet of fresh tulips', '[\"no_img.png\"]', 2, 0),
('Chocolate Box', 'A box of assorted chocolates', '[\"no_img.png\"]', 3, 0),
('Orchid Arrangement', 'Elegant orchid arrangement', '[\"no_img.png\"]', 4, 0),
('Sunflower Bouquet', 'Bright sunflower bouquet', '[\"no_img.png\"]', 5, 0);

-- 9. Insert data for [ProductItems] (Phụ thuộc vào Categories)
INSERT INTO [dbo].[ProductItems] (Name, ImportPrice, CategoryId, Images, Description, IsDelete)
VALUES 
('Single Rose', 5, 3, '[\"no_img.png\"]', 'A single red rose', 0),
('Single Tulip', 4, 4, '[\"no_img.png\"]', 'A single tulip', 0),
('Dark Chocolate', 10, 5, '[\"no_img.png\"]', 'Dark chocolate piece', 0),
('Milk Chocolate', 8, 5, '[\"no_img.png\"]', 'Milk chocolate piece', 0),
('Orchid Plant', 20, 1, '[\"no_img.png\"]', 'Single orchid plant', 0);

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
