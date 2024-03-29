USE [master]
GO
/****** Object:  Database [InventoryManagement]    Script Date: 3/17/2023 10:04:25 PM ******/
CREATE DATABASE [InventoryManagement]
GO
ALTER DATABASE [InventoryManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventoryManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventoryManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InventoryManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InventoryManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InventoryManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InventoryManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InventoryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InventoryManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InventoryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InventoryManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InventoryManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InventoryManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InventoryManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [InventoryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InventoryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InventoryManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InventoryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InventoryManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InventoryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InventoryManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InventoryManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InventoryManagement] SET  MULTI_USER 
GO
ALTER DATABASE [InventoryManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InventoryManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InventoryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InventoryManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InventoryManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InventoryManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InventoryManagement] SET QUERY_STORE = OFF
GO
USE [InventoryManagement]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryID] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consignment]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consignment](
	[consignmentID] [int] IDENTITY(1,1) NOT NULL,
	[consignmentName] [nvarchar](50) NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[consignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consignment_Detail]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consignment_Detail](
	[consignmentDetailID] [int] IDENTITY(1,1) NOT NULL,
	[consignmentID] [int] NOT NULL,
	[productID] [int] NOT NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[consignmentDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customerID] [int] IDENTITY(1,1) NOT NULL,
	[customerName] [nvarchar](max) NULL,
	[customerAddress] [nvarchar](max) NULL,
	[customerPhone] [nvarchar](11) NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[customerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_Input]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_Input](
	[inputBillID] [int] IDENTITY(1,1) NOT NULL,
	[suplierID] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[inputDate] [date] NOT NULL,
	[amount] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[inputBillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_InputDetails]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_InputDetails](
	[inputDetailID] [int] IDENTITY(1,1) NOT NULL,
	[inputBillID] [int] NOT NULL,
	[consignmentDetailID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[inputDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_Output]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_Output](
	[outputBillID] [int] IDENTITY(1,1) NOT NULL,
	[customerID] [int] NOT NULL,
	[userID] [int] NOT NULL,
	[outputDate] [date] NOT NULL,
	[amount] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[outputBillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_OutputDetails]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_OutputDetails](
	[outputDetailID] [int] IDENTITY(1,1) NOT NULL,
	[outputBillID] [int] NOT NULL,
	[consignmentDetailID] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[outputDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productID] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](max) NULL,
	[categoryID] [int] NOT NULL,
	[image] [nvarchar](max) NULL,
	[unit] [nvarchar](50) NULL,
	[importPrice] [float] NULL,
	[sellingPrice] [float] NULL,
	[totalQuantity] [int] NULL,
	[status] [int] NULL,
	[productName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleID] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[roleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suplier]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suplier](
	[suplierID] [int] IDENTITY(1,1) NOT NULL,
	[suplierName] [nvarchar](50) NOT NULL,
	[suplierPhone] [nvarchar](11) NOT NULL,
	[taxCode] [nvarchar](20) NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[suplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/17/2023 10:04:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[fullName] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[roleID] [int] NOT NULL,
	[gender] [int] NULL,
	[birthDay] [date] NULL,
	[phoneNumber] [nvarchar](11) NULL,
	[address] [nvarchar](max) NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (1, N'Rượu vang đỏ')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (2, N'Rượu vang trắng')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (3, N'Rượu vang hồng')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (4, N'Rượu vang ngọt')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (5, N'Rượu vang moscato')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (6, N'Rượu vang 18 độ')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (7, N'Champagne')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Consignment] ON 

INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (2, N'L001', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (3, N'L002', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (4, N'L003', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (5, N'L004', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (6, N'L005', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (7, N'L007', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (8, N'L006', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (9, N'L008', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (10, N'L010', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (11, N'L030', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (12, N'L023', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (13, N'L111', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (14, N'L333', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (15, N'C11', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (16, N'L001', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (17, N'L002', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (18, N'L010', 1)
INSERT [dbo].[Consignment] ([consignmentID], [consignmentName], [status]) VALUES (19, N'L111', 1)
SET IDENTITY_INSERT [dbo].[Consignment] OFF
GO
SET IDENTITY_INSERT [dbo].[Consignment_Detail] ON 

INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (32, 16, 1, 28)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (33, 16, 2, 18)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (34, 16, 3, 30)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (35, 17, 4, 10)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (36, 17, 5, 21)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (37, 18, 6, 27)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (38, 18, 7, 29)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (39, 19, 1, 33)
INSERT [dbo].[Consignment_Detail] ([consignmentDetailID], [consignmentID], [productID], [quantity]) VALUES (40, 19, 4, 33)
SET IDENTITY_INSERT [dbo].[Consignment_Detail] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([customerID], [customerName], [customerAddress], [customerPhone], [status]) VALUES (9, N'CÔNG TY TNHH GIANG NAM GIANG', N'18A phố Ngô Tất Tố - Phường Văn Miếu - Quận Đống đa - Hà Nội', N'012547885', 1)
INSERT [dbo].[Customer] ([customerID], [customerName], [customerAddress], [customerPhone], [status]) VALUES (10, N'CÔNG TY TNHH THƯƠNG MẠI VÀ DỊCH VỤ DIỆU LINH VIỆT NAM', N' Cụm 13 - Xã Tân Hội - Huyện Đan Phượng - Hà Nội', N'087544212', 1)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice_Input] ON 

INSERT [dbo].[Invoice_Input] ([inputBillID], [suplierID], [userID], [inputDate], [amount]) VALUES (16, 6, 3, CAST(N'2023-03-17' AS Date), 40500000)
INSERT [dbo].[Invoice_Input] ([inputBillID], [suplierID], [userID], [inputDate], [amount]) VALUES (17, 3, 3, CAST(N'2023-03-19' AS Date), 30240000)
INSERT [dbo].[Invoice_Input] ([inputBillID], [suplierID], [userID], [inputDate], [amount]) VALUES (18, 6, 3, CAST(N'2023-03-17' AS Date), 36650000)
INSERT [dbo].[Invoice_Input] ([inputBillID], [suplierID], [userID], [inputDate], [amount]) VALUES (19, 6, 3, CAST(N'2023-03-17' AS Date), 33330000)
SET IDENTITY_INSERT [dbo].[Invoice_Input] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice_InputDetails] ON 

INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (15, 16, 32, 30, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (16, 16, 33, 30, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (17, 16, 34, 30, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (18, 17, 35, 21, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (19, 17, 36, 21, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (20, 18, 37, 50, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (21, 18, 38, 50, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (22, 19, 39, 33, NULL)
INSERT [dbo].[Invoice_InputDetails] ([inputDetailID], [inputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (23, 19, 40, 33, NULL)
SET IDENTITY_INSERT [dbo].[Invoice_InputDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice_Output] ON 

INSERT [dbo].[Invoice_Output] ([outputBillID], [customerID], [userID], [outputDate], [amount]) VALUES (1, 9, 3, CAST(N'2023-03-17' AS Date), 700000)
INSERT [dbo].[Invoice_Output] ([outputBillID], [customerID], [userID], [outputDate], [amount]) VALUES (2, 10, 3, CAST(N'2023-03-16' AS Date), 12660000)
INSERT [dbo].[Invoice_Output] ([outputBillID], [customerID], [userID], [outputDate], [amount]) VALUES (3, 9, 3, CAST(N'2023-03-18' AS Date), 16217000)
SET IDENTITY_INSERT [dbo].[Invoice_Output] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice_OutputDetails] ON 

INSERT [dbo].[Invoice_OutputDetails] ([outputDetailID], [outputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (1, 1, 32, 2, NULL)
INSERT [dbo].[Invoice_OutputDetails] ([outputDetailID], [outputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (2, 2, 35, 11, NULL)
INSERT [dbo].[Invoice_OutputDetails] ([outputDetailID], [outputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (3, 2, 33, 12, NULL)
INSERT [dbo].[Invoice_OutputDetails] ([outputDetailID], [outputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (4, 3, 37, 23, NULL)
INSERT [dbo].[Invoice_OutputDetails] ([outputDetailID], [outputBillID], [consignmentDetailID], [quantity], [totalPrice]) VALUES (5, 3, 38, 21, NULL)
SET IDENTITY_INSERT [dbo].[Invoice_OutputDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (1, N'Nếu để kể đến những chai vang đỏ ấn tượng của Italia thì chai Rượu Vang 125 Primitivo Del Salento là điển hình tiêu biểu hơn cả.', 1, N'Ruou-Vang-125-Primitivo-Del-Salento.jpg', N'ML', 350000, 550000, 61, 1, N'Rượu Vang 125 Primitivo Del Salento')
INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (2, N'Đối với những tín đồ của rượu vang Italia có lẽ quý khách hàng chẳng còn xa lạ với sản phẩm Rượu Vang 1973 Sangiovese Puglia ấy', 1, N'Ruou-Vang-1973-Sangiovese-Puglia.jpg', N'ML', 450000, 650000, 18, 1, N'Rượu Vang 1973 Sangiovese Puglia')
INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (3, N'Những ấn tượng đầu tiên dành cho chai Rượu Vang 20 Edizione đó chính là sự đậm đà trong từng nốt hương vị rượu khác nhau', 1, N'Ruou-Vang-20-Edizione-Limited-Release.jpg', N'ML', 550000, 850000, 30, 1, N'Rượu Vang 20 Edizione Limited Release')
INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (4, N'Nổi tiếng là chai rượu vang trắng điển hình tiêu biểu đến từ đất nước Italia, chai Rượu Vang 3 Passo Bianco có lẽ mang đến cho quý khách hàng những cảm nhận vô cùng phong phú và đa dạng', 2, N'Ruou-Vang-3-Passo-Bianco.jpg', N'ML', 660000, 880000, 43, 1, N'Rượu Vang 3 Passo Bianco')
INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (5, N'So với những chai vang đỏ thì vang trắng Italia cũng chẳng bao giờ làm cho con người ta trở nên dễ dàng lãng quên đi', 2, N'Ruou-Vang-47-Anno-Domini-Moscato.jpg', N'ML', 780000, 900000, 21, 1, N'Rượu Vang 47 Anno Domini Moscato')
INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (6, N'Được biết đến là dòng vang trắng ấn tượng, chai Rượu Vang Allegrini Corte Giara Pinot Grigio trở thành một trong số những sự lựa chọn tối ưu dành cho người dùng. ', 2, N'Ruou-Vang-Allegrini-Corte-Giara-Pinot-Grigio.jpg', N'ML', 412000, 712000, 27, 1, N'Rượu Vang Allegrini Corte Giara Pinot Grigio')
INSERT [dbo].[Product] ([productID], [description], [categoryID], [image], [unit], [importPrice], [sellingPrice], [totalQuantity], [status], [productName]) VALUES (7, N'Ý là một quốc gia có nền công nghiệp sản xuất rượu vang phát triển hưng thịnh hàng đầu thế giới.', 4, N'Ruou-Sparkling-Tosti-1820-Butterfly.jpg', N'ML', 321000, 632000, 29, 1, N'Rượu Sparkling Tosti 1820 Butterfly')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([roleID], [name]) VALUES (0, N'ADMIN')
INSERT [dbo].[Role] ([roleID], [name]) VALUES (1, N'STAFF')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Suplier] ON 

INSERT [dbo].[Suplier] ([suplierID], [suplierName], [suplierPhone], [taxCode], [status]) VALUES (3, N'CÔNG TY TNHH RƯỢU 9 CHUM', N'0843254127', N'0100109106', 1)
INSERT [dbo].[Suplier] ([suplierID], [suplierName], [suplierPhone], [taxCode], [status]) VALUES (5, N'CÔNG TY TNHH ĐẦU TƯ XNK ROYAL', N'0931305789', N'0100112437', 1)
INSERT [dbo].[Suplier] ([suplierID], [suplierName], [suplierPhone], [taxCode], [status]) VALUES (6, N'CÔNG TY CỔ PHẦN RƯỢU VODKA NGA', N'084544567', N'4500626794', 1)
INSERT [dbo].[Suplier] ([suplierID], [suplierName], [suplierPhone], [taxCode], [status]) VALUES (8, N'CÔNG TY TNHH SAKAYA', N'084544123', N'4500622944', 1)
INSERT [dbo].[Suplier] ([suplierID], [suplierName], [suplierPhone], [taxCode], [status]) VALUES (9, N'CÔNG TY TNHH XUẤT NHẬP KHẨU HỒNG ĐĂNG DIAMOND SKY', N'0845612477', N'0107990896', 1)
SET IDENTITY_INSERT [dbo].[Suplier] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userID], [username], [fullName], [password], [roleID], [gender], [birthDay], [phoneNumber], [address], [status]) VALUES (2, N'admin', N'ADMIN', N'123456', 0, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[User] ([userID], [username], [fullName], [password], [roleID], [gender], [birthDay], [phoneNumber], [address], [status]) VALUES (3, N'admin@gmail.com', N'Nguyễn Văn Admin', N'123', 1, 1, CAST(N'2023-10-10' AS Date), N'1234556789', N'TP.HCM', 1)
INSERT [dbo].[User] ([userID], [username], [fullName], [password], [roleID], [gender], [birthDay], [phoneNumber], [address], [status]) VALUES (4, N'user@gmail.com', N'Bùi Thị Khách', N'123', 1, 0, CAST(N'2023-09-09' AS Date), N'1234556789', N'TP.HCM', 1)
INSERT [dbo].[User] ([userID], [username], [fullName], [password], [roleID], [gender], [birthDay], [phoneNumber], [address], [status]) VALUES (5, N'user1@gmail.com', N'Nguyễn Văn A', N'123', 1, 0, CAST(N'2023-09-09' AS Date), N'1234556789', N'TP.HCM', 1)
INSERT [dbo].[User] ([userID], [username], [fullName], [password], [roleID], [gender], [birthDay], [phoneNumber], [address], [status]) VALUES (6, N'user2@gmail.com', N'Nguyễn Van B', N'123', 1, 0, CAST(N'2023-09-09' AS Date), N'1234556789', N'TP.HCM', 1)
INSERT [dbo].[User] ([userID], [username], [fullName], [password], [roleID], [gender], [birthDay], [phoneNumber], [address], [status]) VALUES (7, N'user3@gmail.com', N'Nguyễn Văn C', N'123', 1, 0, CAST(N'2023-09-09' AS Date), N'1234556789', N'TP.HCM', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Suplier__D97858A68F57E08B]    Script Date: 3/17/2023 10:04:25 PM ******/
ALTER TABLE [dbo].[Suplier] ADD UNIQUE NONCLUSTERED 
(
	[taxCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__F3DBC5721A703B97]    Script Date: 3/17/2023 10:04:25 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Consignment_Detail]  WITH CHECK ADD  CONSTRAINT [FK_CONSIGNMENT_ID_PRODUCT_CONSIGNMENT] FOREIGN KEY([consignmentID])
REFERENCES [dbo].[Consignment] ([consignmentID])
GO
ALTER TABLE [dbo].[Consignment_Detail] CHECK CONSTRAINT [FK_CONSIGNMENT_ID_PRODUCT_CONSIGNMENT]
GO
ALTER TABLE [dbo].[Consignment_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Product_ID_PRODUCT_CONSIGNMENT] FOREIGN KEY([productID])
REFERENCES [dbo].[Product] ([productID])
GO
ALTER TABLE [dbo].[Consignment_Detail] CHECK CONSTRAINT [FK_Product_ID_PRODUCT_CONSIGNMENT]
GO
ALTER TABLE [dbo].[Invoice_Input]  WITH CHECK ADD  CONSTRAINT [FK_SUPLIER_ID_INVOICE_INPUT] FOREIGN KEY([suplierID])
REFERENCES [dbo].[Suplier] ([suplierID])
GO
ALTER TABLE [dbo].[Invoice_Input] CHECK CONSTRAINT [FK_SUPLIER_ID_INVOICE_INPUT]
GO
ALTER TABLE [dbo].[Invoice_Input]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID_INVOICE_INPUT] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Invoice_Input] CHECK CONSTRAINT [FK_USER_ID_INVOICE_INPUT]
GO
ALTER TABLE [dbo].[Invoice_InputDetails]  WITH CHECK ADD  CONSTRAINT [FK_CONSIGNMENTDETAIL_ID_INVOICE_INPUTDETAILS] FOREIGN KEY([consignmentDetailID])
REFERENCES [dbo].[Consignment_Detail] ([consignmentDetailID])
GO
ALTER TABLE [dbo].[Invoice_InputDetails] CHECK CONSTRAINT [FK_CONSIGNMENTDETAIL_ID_INVOICE_INPUTDETAILS]
GO
ALTER TABLE [dbo].[Invoice_InputDetails]  WITH CHECK ADD  CONSTRAINT [FK_INPUTBILL_ID_Invoice_InputDetails] FOREIGN KEY([inputBillID])
REFERENCES [dbo].[Invoice_Input] ([inputBillID])
GO
ALTER TABLE [dbo].[Invoice_InputDetails] CHECK CONSTRAINT [FK_INPUTBILL_ID_Invoice_InputDetails]
GO
ALTER TABLE [dbo].[Invoice_Output]  WITH CHECK ADD  CONSTRAINT [FK_CUSTOMERID_ID_INVOICE_OUTPUT] FOREIGN KEY([customerID])
REFERENCES [dbo].[Customer] ([customerID])
GO
ALTER TABLE [dbo].[Invoice_Output] CHECK CONSTRAINT [FK_CUSTOMERID_ID_INVOICE_OUTPUT]
GO
ALTER TABLE [dbo].[Invoice_Output]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID_INVOICE_OUTPUT] FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Invoice_Output] CHECK CONSTRAINT [FK_USER_ID_INVOICE_OUTPUT]
GO
ALTER TABLE [dbo].[Invoice_OutputDetails]  WITH CHECK ADD  CONSTRAINT [FK_CONSIGNMENTDETAIL_ID_INVOICE_OUTPUTDETAILS] FOREIGN KEY([consignmentDetailID])
REFERENCES [dbo].[Consignment_Detail] ([consignmentDetailID])
GO
ALTER TABLE [dbo].[Invoice_OutputDetails] CHECK CONSTRAINT [FK_CONSIGNMENTDETAIL_ID_INVOICE_OUTPUTDETAILS]
GO
ALTER TABLE [dbo].[Invoice_OutputDetails]  WITH CHECK ADD  CONSTRAINT [FK_OUTPUTBILL_ID_INVOICE_OUTPUTDETAILS] FOREIGN KEY([outputBillID])
REFERENCES [dbo].[Invoice_Output] ([outputBillID])
GO
ALTER TABLE [dbo].[Invoice_OutputDetails] CHECK CONSTRAINT [FK_OUTPUTBILL_ID_INVOICE_OUTPUTDETAILS]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_CATEGORY_ID_PRODUCT] FOREIGN KEY([categoryID])
REFERENCES [dbo].[Category] ([categoryID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_CATEGORY_ID_PRODUCT]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_ROLEID_USER] FOREIGN KEY([roleID])
REFERENCES [dbo].[Role] ([roleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_ROLEID_USER]
GO
USE [master]
GO
ALTER DATABASE [InventoryManagement] SET  READ_WRITE 
GO
