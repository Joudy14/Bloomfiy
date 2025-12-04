USE BloomfiyDB;
GO

-- First, delete all data to start fresh
DELETE FROM ProductColors;
DELETE FROM Products;
DELETE FROM Colors;
DELETE FROM Categories;
GO

-- Reset identity counters
DBCC CHECKIDENT ('Categories', RESEED, 0);
DBCC CHECKIDENT ('Colors', RESEED, 0);
DBCC CHECKIDENT ('Products', RESEED, 0);
DBCC CHECKIDENT ('ProductColors', RESEED, 0);
GO

-- Insert fresh data
INSERT INTO [Categories] ([category_name], [description]) VALUES
('Bouquets', 'Beautiful flower arrangements'),
('Single Varieties', 'Individual flower types'),
('Arrangements', 'Designed floral displays'),
('Premium Collection', 'Luxury floral selections');
GO

INSERT INTO [Colors] ([color_name], [color_code], [price_adjustment]) VALUES
('Red', '#FF0000', 0.00),
('White', '#FFFFFF', 0.00),
('Pink', '#FF69B4', 2.00),
('Yellow', '#FFD700', 1.50),
('Purple', '#800080', 3.00);
GO

INSERT INTO [Products] ([name], [description], [base_price], [stock_quantity], [image_url], [category_id]) VALUES
('Peony', 'Lush, rose-shaped blooms with rich sweet fragrance', 68.99, 25, '/Images/products_img/peony/peony_pink.png', 2),
('Red Roses Bouquet', '12 beautiful red roses perfect for romantic occasions', 45.99, 30, '/Images/products_img/roses_red.jpg', 1),
('White Lilies', 'Elegant white lilies symbolizing purity and beauty', 39.99, 20, '/Images/products_img/lilies_white.jpg', 2),
('Mixed Spring Bouquet', 'Colorful mix of seasonal flowers for any occasion', 52.99, 15, '/Images/products_img/mixed_bouquet.jpg', 1);
GO

-- Link products to colors (NO DUPLICATES)
INSERT INTO [ProductColors] ([product_id], [color_id]) VALUES
(1, 3), -- Peony - Pink
(1, 1), -- Peony - Red
(1, 2), -- Peony - White
(2, 1), -- Roses - Red
(3, 2), -- Lilies - White
(4, 3), -- Mixed - Pink
(4, 4), -- Mixed - Yellow
(4, 5); -- Mixed - Purple
GO

PRINT '✅ Database cleaned and fresh data inserted!';
GO