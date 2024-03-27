CREATE DATABASE Information_System_Of_DNS;
GO

USE Information_System_Of_DNS;
GO


DROP TABLE Histories_of_Changes_In_Prices_of_Products;
DROP TABLE Datas_for_Receipt;
DROP TABLE Supplys;
DROP TABLE Stock;
DROP TABLE Products;
DROP TABLE Products_Categories;
DROP TABLE Suppliers;
DROP TABLE Users;
DROP TABLE Users_Datas;
DROP TABLE Roles;
DROP TABLE Receipts_and_Products;


CREATE TABLE Roles(
	Role_ID		INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Role_Name	VARCHAR(20)		NOT NULL
);
GO

INSERT INTO Roles (Role_Name) VALUES ('Администратор');
GO
INSERT INTO Roles (Role_Name) VALUES ('Кассир');
GO
INSERT INTO Roles (Role_Name) VALUES ('Кладовщик');
GO
INSERT INTO Roles (Role_Name) VALUES ('Нет');
GO


CREATE TABLE Users_Datas(
	User_Data_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	User_Data_Name			VARCHAR(15)		NOT NULL,
	User_Data_Surname		VARCHAR(20)		NOT NULL,
	User_Data_Second_Name	VARCHAR(20)
);
GO

INSERT INTO Users_Datas (User_Data_Name, User_Data_Surname, User_Data_Second_Name) VALUES ('Иван', 'Иванов', 'Иванович');
GO
INSERT INTO Users_Datas (User_Data_Name, User_Data_Surname, User_Data_Second_Name) VALUES ('Петр', 'Петров', 'Петрович');
GO


CREATE TABLE Users(
	User_ID1		INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	User_Username	VARCHAR(50)		NOT NULL,
	User_Email		VARCHAR(40)		NOT NULL,
	User_Password	VARCHAR(40)	    NOT NULL,
	ID_Role			INT				NOT NULL,
	ID_User_Data	INT				NOT NULL,

	FOREIGN KEY (ID_Role) REFERENCES Roles(Role_ID),
	FOREIGN KEY (ID_User_Data) REFERENCES Users_Datas(User_Data_ID)
);
GO

INSERT INTO Users (User_Username, User_Email, User_Password, ID_Role, ID_User_Data) VALUES ('1', '1@', '6b86b273ff34fce19d6b804eff5a3f5747ada4ea', 1, 1);
GO
INSERT INTO Users (User_Username, User_Email, User_Password, ID_Role, ID_User_Data) VALUES ('2', 'user2@example.com', 'd4735e3a265e16eee03f59718b9b5d03019c07d8', 3, 2);
GO


CREATE TABLE Suppliers(
	Supplier_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Supplier_Name		VARCHAR(100)	NOT NULL,
	Supplier_Info		VARCHAR(200)	NOT NULL,
	Supplier_Address	VARCHAR(100)	NOT NULL
);
GO

INSERT INTO Suppliers (Supplier_Name, Supplier_Info, Supplier_Address) VALUES ('ООО "Электроника-Маркет"', 'Крупный поставщик компьютеров и ноутбуков', 'ул. Промышленная, 5, Москва, Россия');
GO
INSERT INTO Suppliers (Supplier_Name, Supplier_Info, Supplier_Address) VALUES ('Группа компаний "ГаджетТрейд"', 'Поставщик смартфонов и планшетов', 'пр. Технологический, 12, Санкт-Петербург, Россия');
GO
INSERT INTO Suppliers (Supplier_Name, Supplier_Info, Supplier_Address) VALUES ('Интернет-магазин "Умный Дом"', 'Продажа умных часов и умных колонок', 'ул. Цифровая, 3, Екатеринбург, Россия');
GO
INSERT INTO Suppliers (Supplier_Name, Supplier_Info, Supplier_Address) VALUES ('ООО "Технотрейд-Про"', 'Поставщик бытовой техники', 'ул. Потребительская, 8, Краснодар, Россия');
GO
INSERT INTO Suppliers (Supplier_Name, Supplier_Info, Supplier_Address) VALUES ('АО "ЭлектроСтройМаг"', 'Оптовая торговля электроинструментом', 'пр. Энергетический, 21, Челябинск, Россия');
GO

CREATE TABLE Products_Categories(
	Product_Category_ID		INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Product_Category_Name	VARCHAR(30)		NOT NULL		
);
GO

CREATE TABLE Products(
	Product_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Product_Name		VARCHAR(100)    NOT NULL,
	Product_Price		REAL			NOT NULL,
	ID_Product_Category	INT				NOT NULL,

	FOREIGN KEY (ID_Product_Category)   REFERENCES Products_Categories(Product_Category_ID)
);
GO

CREATE TABLE Stock(
	Stock_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Stock_Quantity		INT,
	ID_Product			INT				NOT NULL,

	FOREIGN KEY (ID_Product)  REFERENCES Products(Product_ID)
);
GO

-- Заполнение таблицы Products_Categories с пятью категориями
INSERT INTO Products_Categories (Product_Category_Name) VALUES ('Ноутбуки и компьютеры');
GO
INSERT INTO Products_Categories (Product_Category_Name) VALUES ('Смартфоны и планшеты');
GO
INSERT INTO Products_Categories (Product_Category_Name) VALUES ('Бытовая техника');
GO
INSERT INTO Products_Categories (Product_Category_Name) VALUES ('Телевизоры и мониторы');
GO
INSERT INTO Products_Categories (Product_Category_Name) VALUES ('Аксессуары и комплектующие');
GO

CREATE TABLE Supplys(
	Supply_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Supply_Quantity		INT				NOT NULL,
	Supply_Date			DATETIME		NOT NULL,
	ID_Supplier 		INT				NOT NULL,
	ID_Product			INT				NOT NULL,

	FOREIGN KEY (ID_Supplier) REFERENCES Suppliers(Supplier_ID),
	FOREIGN KEY (ID_Product)  REFERENCES Products(Product_ID)
);
GO


CREATE TABLE Datas_for_Receipt(
	Receipt_ID							INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	Receipt_Full_Price					REAL			NOT NULL,
	Receipt_Buyer_Amount				REAL			NOT NULL,
	Receipt_Change						REAL,
	Receipt_Date						DATETIME		NOT NULL
);
GO




CREATE TABLE Receipts_and_Products(
	Receipts_and_Products_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	ID_Receipt							INT				NOT NULL,
	ID_Product							INT				NOT NULL,
	Quantity_from_Stock					INT				NOT NULL,

	FOREIGN KEY (ID_Receipt) REFERENCES Datas_for_Receipt(Receipt_ID),
	FOREIGN KEY (ID_Product) REFERENCES Products(Product_ID)
);
GO



CREATE TABLE Histories_of_Changes_In_Prices_of_Products(
	ChangesP__of_Products_ID			INT				PRIMARY KEY IDENTITY(1,1)	NOT NULL,
	ChangesP__of_Products_Old			REAL			NOT NULL,
	ChangesP__of_Products_New			REAL,		
	ChangesP__of_Products_Change_Date	DATETIME,
	ChangesP__of_Products_Reason		VARCHAR(100),
	ID_Product							INT				NOT NULL,

	FOREIGN KEY (ID_Product)  REFERENCES Products(Product_ID)
);
GO


		-- Триггер при удалении данных из таблицы "Users"
CREATE TRIGGER trg_DeleteUsersData
ON Users
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Users_Datas
    WHERE User_Data_ID IN (SELECT deleted.ID_User_Data FROM deleted)
END;
GO

CREATE TRIGGER trgAfterInsertProduct
ON Products
AFTER INSERT
AS
BEGIN
    INSERT INTO Histories_of_Changes_In_Prices_of_Products (ChangesP__of_Products_Old, ChangesP__of_Products_New, ChangesP__of_Products_Change_Date, ChangesP__of_Products_Reason, ID_Product)
    SELECT
        Product_Price,
        0,
        GETDATE(),
        'Нет изменений',
        Product_ID
    FROM
        inserted;
END;
GO

CREATE TRIGGER trg_Products_Delete
ON Products
AFTER DELETE
AS
BEGIN
    DELETE FROM Histories_of_Changes_In_Prices_of_Products
    WHERE ID_Product IN (SELECT deleted.Product_ID FROM deleted);
END;
GO

CREATE TRIGGER trg_Products_Insert
ON Products
AFTER INSERT
AS
BEGIN
    INSERT INTO Stock (Stock_Quantity, ID_Product)
    SELECT 0, inserted.Product_ID
    FROM inserted;

    INSERT INTO Supplys (Supply_Quantity, Supply_Date, ID_Supplier, ID_Product)
    SELECT 0, GETDATE(), 1, inserted.Product_ID  -- замените "1" на соответствующий ID вашего поставщика
    FROM inserted;
END;
GO

CREATE TRIGGER trg_Histories_of_Changes_Update
ON Histories_of_Changes_In_Prices_of_Products
AFTER UPDATE
AS
BEGIN
    UPDATE Products
    SET Product_Price = i.ChangesP__of_Products_New
    FROM Products p
    INNER JOIN inserted i ON p.Product_ID = i.ID_Product;
END;
GO

SELECT * FROM Users
GO
SELECT * FROM Users_Datas;
GO
SELECT * FROM Roles;
GO
SELECT * FROM Suppliers;
GO
SELECT * FROM Supplys;
GO
SELECT * FROM Products_Categories;
GO
SELECT * FROM Stock;
GO
SELECT * FROM Products;
GO
SELECT * FROM Datas_for_Receipt;
GO
SELECT * FROM Histories_of_Changes_In_Prices_of_Products;
GO
SELECT * FROM Receipts_and_Products;
GO