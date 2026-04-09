CREATE DATABASE Inventory;
GO

USE Inventory;
GO

CREATE TABLE [__EFMigrationsHistory] (
  [MigrationId] nvarchar(150) NOT NULL PRIMARY KEY,
  [ProductVersion] nvarchar(32) NOT NULL
);

CREATE TABLE [Category] (
  [Id] bigint  IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Name] nvarchar(max) NOT NULL,
  [CreationDate] datetime2(7)  NULL,
  [ModificationDate] datetime2(7)  NULL
);

CREATE TABLE [Product] (
  [Id] uniqueidentifier  NOT NULL PRIMARY KEY,
  [Name] nvarchar(max) NOT NULL,
  [Description] nvarchar(max) NULL,
  [CategoryId] bigint  NOT NULL,
  [Image] varbinary(max)  NOT NULL,
  [Price] decimal(18,2)  NOT NULL,
  [Stock] int  NOT NULL,
  [CreationDate] datetime2(7)  NULL,
  [ModificationDate] datetime2(7)  NULL
);

CREATE TABLE [Transaction] (
  [Id] bigint  IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Code] uniqueidentifier  NOT NULL,
  [Date] datetime2(7)  NOT NULL,
  [TransactionTypeId] bigint  NOT NULL,
  [ProductId] uniqueidentifier  NOT NULL,
  [Amount] int  NOT NULL,
  [UnitPrice] decimal(18,2)  NOT NULL,
  [TotalPrice] decimal(18,2)  NOT NULL,
  [Detail] nvarchar(max) NULL,
  [CreationDate] datetime2(7)  NULL,
  [ModificationDate] datetime2(7)  NULL
);


CREATE TABLE [TransactionType] (
  [Id] bigint  IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [Name] nvarchar(max) NOT NULL,
  [CreationDate] datetime2(7)  NULL,
  [ModificationDate] datetime2(7)  NULL
);

ALTER TABLE [Product] ADD CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE [Transaction] ADD CONSTRAINT [FK_Transaction_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

ALTER TABLE [Transaction] ADD CONSTRAINT [FK_Transaction_TransactionType_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [TransactionType] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION;

INSERT INTO [Category] ([Name], [CreationDate], [ModificationDate])
VALUES 
('Electrónica', GETDATE(), GETDATE()),
('Ropa', GETDATE(), GETDATE()),
('Alimentos', GETDATE(), GETDATE()),
('Hogar', GETDATE(), GETDATE()),
('Libros', GETDATE(), GETDATE()),
('Juguetes', GETDATE(), GETDATE()),
('Deportes', GETDATE(), GETDATE()),
('Belleza', GETDATE(), GETDATE());

INSERT INTO [TransactionType] ([Name], [CreationDate], [ModificationDate]) VALUES 
('Venta', GETDATE(), GETDATE()),
('Compra', GETDATE(), GETDATE());