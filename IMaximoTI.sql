IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CodeChallengeAlMaximoTI')
    CREATE DATABASE CodeChallengeAlMaximoTI;
GO

USE CodeChallengeAlMaximoTI;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ProductTypes')
BEGIN
    CREATE TABLE ProductTypes (
        ID INT IDENTITY PRIMARY KEY,
        name nvarchar(25),
        description nvarchar(MAX)
    );
END;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Suppliers')
BEGIN
    CREATE TABLE Suppliers (
        ID INT IDENTITY PRIMARY KEY,
        name nvarchar(50),
        description nvarchar(MAX)
    );
END;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Products')
BEGIN
    CREATE TABLE Products (
        ID INT IDENTITY PRIMARY KEY,
        clave nvarchar(15),
        name NVARCHAR(25),
        product_type_id INT FOREIGN KEY REFERENCES ProductTypes(ID),
        status VARCHAR(10),
        price FLOAT,
    );
END;
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Suppliers_Products')
BEGIN
    CREATE TABLE Suppliers_Products (
        supplier_id INT FOREIGN KEY REFERENCES Suppliers(ID),
        product_id INT FOREIGN KEY REFERENCES Products(ID),
        supplier_price FLOAT,
        supplier_clave NVARCHAR(15),
        PRIMARY KEY (supplier_id, product_id)
    );
END;
GO

CREATE PROCEDURE SP_GetCustomSuppliersProducts 
    @ProductID INT
AS
BEGIN
    select sp.supplier_id, sp.product_id, sp.supplier_price, sp.supplier_clave,
            s.name as 'supplier_name', pt.name as 'product_type', pt.description as 'product_description',
            p.name as 'product_name', p.price as 'product_price', p.status as 'product_status'  
        FROM Suppliers_Products as sp
        INNER JOIN Products AS p ON p.ID = sp.product_id
        INNER JOIN Suppliers AS s ON s.ID = sp.supplier_id
        INNER JOIN ProductTypes AS pt ON pt.ID = p.product_type_id
    where sp.product_id = @ProductID
END;
GO

CREATE PROCEDURE SP_ProductTypes AS
BEGIN
    SELECT * FROM ProductTypes
END;
GO

CREATE PROCEDURE SP_Suppliers 
AS
BEGIN
    SELECT * FROM Suppliers
END;
GO

CREATE PROCEDURE SP_Products 
AS
BEGIN
    SELECT p.ID, p.clave, p.name, p.product_type_id, pt.name AS 'product_type', p.status, p.price
    FROM Products AS p
    INNER JOIN ProductTypes AS pt ON pt.ID = p.product_type_id
END;
GO

CREATE PROCEDURE SP_Suppliers_Products 
AS
BEGIN
    SELECT * FROM Suppliers_Products 
END;
GO

CREATE PROCEDURE SP_GetProductByID
    @ProductID INT
AS
BEGIN
    SELECT * FROM Products
    WHERE ID = @ProductID;
END;
GO

CREATE PROCEDURE SP_UpdateProduct
    @ProductID INT,
    @NewClave NVARCHAR(15),
    @NewName NVARCHAR(25),
    @NewProductTypeID INT,
    @NewStatus VARCHAR(10),
    @NewPrice FLOAT
AS
BEGIN
    UPDATE Products
    SET
        clave = @NewClave,
        name = @NewName,
        product_type_id = @NewProductTypeID,
        status = @NewStatus,
        price = @NewPrice
    WHERE
        ID = @ProductID;
END;
GO

CREATE PROCEDURE SP_InsertProduct
    @Clave NVARCHAR(15),
    @Name NVARCHAR(25),
    @ProductTypeID INT,
    @Status VARCHAR(10),
    @Price FLOAT = 0.0
AS
BEGIN
    INSERT INTO Products (clave, name, product_type_id, status, price)
    VALUES (@Clave, @Name, @ProductTypeID, @Status, @Price);
END;
GO

CREATE PROCEDURE SP_DeleteProduct
    @ProductID INT
AS
BEGIN
    DELETE FROM Suppliers_Products
    WHERE product_id = @ProductID;
    DELETE FROM Products
    WHERE ID = @ProductID;
END;
GO

create PROCEDURE SP_GetSupplierByID
    @SupplierID int
AS
BEGIN
    Select * from Suppliers
    where ID = @SupplierID;
END;
GO

create PROCEDURE SP_GetSupplierProductByIds
    @ProductID int,
    @SupplierID int
AS
BEGIN
    select * from Suppliers_Products
    where supplier_id = @SupplierID AND product_id = @ProductID;
END;
GO

create procedure SP_InsertSupplier_Product
    @ProductID int,
    @SupplierID int,
    @SupplierClave NVARCHAR(15),
    @SupplierPrice FLOAT
AS
BEGIN
    INSERT INTO Suppliers_Products (supplier_id, product_id, supplier_price, supplier_clave)
        VALUES (@SupplierID, @ProductID, @SupplierPrice, @SupplierClave);
END;
GO


-- ***** INSERTS ******

-- INSERT INTO ProductTypes (name, description)
-- VALUES
-- ('Electrónicos', 'Productos electrónicos de consumo.'),
-- ('Ropa', 'Ropa y accesorios de moda.'),
-- ('Hogar', 'Productos para el hogar.'),
-- ('Juguetes', 'Juguetes y juegos para niños.'),
-- ('Electrodomésticos', 'Electrodomésticos para el hogar.'),
-- ('Deportes', 'Artículos deportivos y equipos.'),
-- ('Alimentos', 'Alimentos y bebidas.'),
-- ('Muebles', 'Muebles y decoración del hogar.'),
-- ('Automóviles', 'Repuestos y accesorios para automóviles.'),
-- ('Libros', 'Libros y publicaciones educativas.');

-- INSERT INTO Suppliers (name, description)
-- VALUES
-- ('ABC Electronics', 'Proveedor de productos electrónicos.'),
-- ('Fashion World', 'Proveedor de ropa y moda.'),
-- ('Home Essentials', 'Proveedor de productos para el hogar.'),
-- ('Kids Universe', 'Proveedor de juguetes para niños.'),
-- ('Household Appliances Inc.', 'Proveedor de electrodomésticos para el hogar.'),
-- ('Sports Gear Co.', 'Proveedor de artículos deportivos y equipos.'),
-- ('Gourmet Foods Ltd.', 'Proveedor de alimentos y bebidas gourmet.'),
-- ('Furniture Emporium', 'Proveedor de muebles y decoración del hogar.'),
-- ('Auto Parts Direct', 'Proveedor de repuestos para automóviles.'),
-- ('Bookworm Books', 'Proveedor de libros y publicaciones educativas.');

-- INSERT INTO Products (clave, name, product_type_id, status, price)
-- VALUES
-- ('P001', 'Teléfono móvil Samsung', 1, 'enabled', 499.99),
-- ('P002', 'Laptop', 1, 'disabled', 899.99),
-- ('P003', 'Camiseta', 2, 'enabled', 19.99),
-- ('P004', 'Sofá', 8, 'enabled', 599.99),
-- ('P005', 'Balón de fútbol', 6, 'enabled', 15.99),
-- ('P006', 'Refrigerador', 5, 'enabled', 699.99),
-- ('P007', 'Juguete de construcción', 4, 'enabled', 29.99),
-- ('P008', 'Cafetera', 5, 'enabled', 49.99),
-- ('P009', 'Tablet', 1, 'enabled', 249.99),
-- ('P010', 'Zapatos deportivos', 2, 'enabled', 59.99),
-- ('P011', 'Comida para perros', 7, 'enabled', 9.99),
-- ('P012', 'Mesa de comedor', 8, 'disabled', 199.99),
-- ('P013', 'Aceite de motor', 9, 'enabled', 12.99),
-- ('P014', 'Libro de ciencia', 10, 'enabled', 24.99),
-- ('P015', 'Aire acondicionado', 5, 'enabled', 349.99);


-- INSERT INTO Suppliers_Products (supplier_id, product_id, supplier_price, supplier_clave)
-- VALUES
-- (1, 1, 450.00, 'S001'), -- Proveedor 1 suministra Producto 1
-- (1, 2, 800.00, 'S002'), -- Proveedor 1 suministra Producto 2
-- (2, 3, 15.00, 'S003'),  -- Proveedor 2 suministra Producto 3
-- (3, 4, 550.00, 'S004'), -- Proveedor 3 suministra Producto 4
-- (4, 5, 12.00, 'S005'),  -- Proveedor 4 suministra Producto 5
-- (5, 6, 680.00, 'S006'), -- Proveedor 5 suministra Producto 6
-- (6, 7, 28.00, 'S007'),  -- Proveedor 6 suministra Producto 7
-- (7, 8, 45.00, 'S008'),  -- Proveedor 7 suministra Producto 8
-- (8, 9, 220.00, 'S009'), -- Proveedor 8 suministra Producto 9
-- (9, 10, 55.00, 'S010'); -- Proveedor 9 suministra Producto 10
