use master
go
drop database if exists [InventoryManagement]
create database [InventoryManagement]
go
use [InventoryManagement];
go

CREATE TABLE [dbo].[Role](
-- 0: admin, 1: staff
	roleID INT IDENTITY(0,1) NOT NULL PRIMARY KEY, 
	[name] NVARCHAR(50) NOT NULL, 
);
GO

CREATE TABLE [dbo].[User](
	userID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	username nvarchar(50) NOT NULL UNIQUE,
	fullName nvarchar(50) NOT NULL,
	[password] NVARCHAR(50) NOT NULL, 
	roleID INT NOT NULL,
	gender INT,
	birthDay DATE,
	phoneNumber NVARCHAR(11),
	address NVARCHAR(MAX),
	[status] INT NOT NULL, -- 0: unactive, 1: active
);
GO

ALTER TABLE [User] 
ADD CONSTRAINT FK_ROLEID_USER FOREIGN KEY (roleID) REFERENCES [Role](RoleID)

CREATE TABLE [dbo].Suplier(
	suplierID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[suplierName] NVARCHAR(50) NOT NULL, 
	suplierPhone NVARCHAR(11) NOT NULL,
	taxCode NVARCHAR(20) NOT NULL UNIQUE,
	[status] INT NOT NULL, -- 0: unactive, 1: active
);
GO


CREATE TABLE [dbo].Customer(
	customerID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[customerName] NVARCHAR(50) NOT NULL,
	customerAddress NVARCHAR(50),
	customerPhone NVARCHAR(11),
	[status] INT NOT NULL, -- 0: unactive, 1: active
);
GO

CREATE TABLE [dbo].Consignment(
	consignmentID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	consignmentName NVARCHAR(50),
	[status] INT NOT NULL, -- 0: , 1: 
);
GO

CREATE TABLE [dbo].Category(
	categoryID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	categoryName NVARCHAR(50),
);
GO

CREATE TABLE [dbo].Product(
	productID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[description] NVARCHAR(max),
	categoryID INT NOT NULL,
	image NVARCHAR(MAX),	
	unit nvarchar(50),
	importPrice float,
	sellingPrice float,
	totalQuantity INT,
	status INT,  -- 0 Inactive, 1 Active
);

--ALTER TABLE [Product] 
--ADD CONSTRAINT FK_CONSIGNMENT_ID_PRODUCT FOREIGN KEY (consignmentID) REFERENCES [Consignment](consignmentID)

ALTER TABLE [Product] 
ADD CONSTRAINT FK_CATEGORY_ID_PRODUCT FOREIGN KEY (categoryID) REFERENCES [Category](categoryID)

CREATE TABLE [dbo].[Consignment_Detail](
	consignmentDetailID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	consignmentID INT NOT NULL, 
	productID INT NOT NULL,
	quantity INT
);
GO
ALTER TABLE [Consignment_Detail] 
ADD CONSTRAINT FK_CONSIGNMENT_ID_PRODUCT_CONSIGNMENT FOREIGN KEY (consignmentID) REFERENCES [Consignment](consignmentID)
ALTER TABLE [Consignment_Detail] 
ADD CONSTRAINT FK_Product_ID_PRODUCT_CONSIGNMENT FOREIGN KEY (productID) REFERENCES [Product](productID)

CREATE TABLE [dbo].[Invoice_Input](
	inputBillID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	suplierID INT NOT NULL, 
	userID INT NOT NULL,
	inputDate DATE NOT NULL,
	amount float,
);
GO

ALTER TABLE [Invoice_Input] 
ADD CONSTRAINT FK_SUPLIER_ID_INVOICE_INPUT FOREIGN KEY (suplierID) REFERENCES [Suplier](suplierID)
ALTER TABLE [Invoice_Input] 
ADD CONSTRAINT FK_USER_ID_INVOICE_INPUT FOREIGN KEY (userID) REFERENCES [User](userID)

CREATE TABLE [dbo].[Invoice_InputDetails](
	inputDetailID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	inputBillID INT NOT NULL, 
	consignmentID INT NOT NULL,
	quantity INT NOT NULL,
	totalPrice float,
);
GO

ALTER TABLE [Invoice_InputDetails] 
ADD CONSTRAINT FK_INPUTBILL_ID_Invoice_InputDetails FOREIGN KEY (inputBillID) REFERENCES [Invoice_Input](inputBillID)

ALTER TABLE [Invoice_InputDetails] 
ADD CONSTRAINT FK_CONSIGNMENT_ID_INVOICE_INPUTDETAILS FOREIGN KEY (consignmentID) REFERENCES [Consignment](consignmentID)

CREATE TABLE [dbo].[Invoice_Output](
	outputBillID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	customerID INT NOT NULL, 
	userID INT NOT NULL,
	outputDate DATE NOT NULL,
	amount float,
);
ALTER TABLE [Invoice_Output] 
ADD CONSTRAINT FK_CUSTOMERID_ID_INVOICE_OUTPUT FOREIGN KEY (customerID) REFERENCES [Customer](customerID)
ALTER TABLE [Invoice_Output] 
ADD CONSTRAINT FK_USER_ID_INVOICE_OUTPUT FOREIGN KEY (userID) REFERENCES [User](userID)

CREATE TABLE [dbo].[Invoice_OutputDetails](
	outputDetailID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	outputBillID INT NOT NULL, 
	consignmentID INT NOT NULL,
	quantity INT NOT NULL,
	totalPrice float,
);
GO


ALTER TABLE [Invoice_OutputDetails] 
ADD CONSTRAINT FK_OUTPUTBILL_ID_INVOICE_OUTPUTDETAILS FOREIGN KEY (outputBillID) REFERENCES [Invoice_Output](outputBillID)
ALTER TABLE [Invoice_OutputDetails] 
ADD CONSTRAINT FK_CONSIGNMENT_ID_INVOICE_OUTPUTDETAILS FOREIGN KEY (consignmentID) REFERENCES [Consignment](consignmentID)

INSERT INTO [Role]
VALUES ('ADMIN')
INSERT INTO [Role]
VALUES ('STAFF')

INSERT INTO [User] (username, fullName, [password], roleID , status)
VALUES ('admin', 'ADMIN', '123456',0, 1)

INSERT INTO [User] (username, fullName, [password], roleID, gender, birthDay, phoneNumber, address, status)
VALUES (N'admin@gmail.com',N'Nguyễn Văn Admin', N'123', 1, 1, '10/10/2023', '1234556789', 'TP.HCM',1);

INSERT INTO [User] (username, fullName, [password], roleID, gender, birthDay, phoneNumber, address, status)
VALUES (N'user@gmail.com',N'Bùi Thị Khách', N'123', 1, 0, '09/09/2023', '1234556789', 'TP.HCM',1);

INSERT INTO [User] (username, fullName, [password], roleID, gender, birthDay, phoneNumber, address, status)
VALUES (N'user1@gmail.com',N'Nguyễn Văn A', N'123', 1, 0, '09/09/2023', '1234556789', 'TP.HCM',1);

INSERT INTO [User] (username, fullName, [password], roleID, gender, birthDay, phoneNumber, address, status)
VALUES (N'user2@gmail.com',N'Nguyễn Van B', N'123', 1, 0, '09/09/2023', '1234556789', 'TP.HCM',1);

INSERT INTO [User] (username, fullName, [password], roleID, gender, birthDay, phoneNumber, address, status)
VALUES (N'user3@gmail.com',N'Nguyễn Văn C', N'123', 1, 0, '09/09/2023', '1234556789', 'TP.HCM',1);


