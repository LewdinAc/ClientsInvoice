IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO


BEGIN TRANSACTION;
GO


IF OBJECT_ID(N'[CustomerTypes]') IS NULL
BEGIN
    CREATE TABLE [CustomerTypes] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_CustomerTypes] PRIMARY KEY ([Id])
    );
    
	SET IDENTITY_INSERT CustomerTypes ON;

    if not exists(select Id from CustomerTypes where Id = 1)
    begin
        insert into Customertypes(Id, Description) values (1, 'Normal');
    end;

    if not exists(select Id from CustomerTypes where Id = 2)
    begin
        insert into Customertypes(Id, Description) values (2, 'Company');
    end;

	SET IDENTITY_INSERT CustomerTypes OFF;
END
GO


IF OBJECT_ID(N'[Customers]') IS NULL
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(70) NOT NULL,
        [Address] nvarchar(150) NOT NULL,
        [IsActive] bit NOT NULL,
        [CostumerTypeId] int NOT NULL,
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customers_CustomerTypes_CostumerTypeId] FOREIGN KEY ([CostumerTypeId]) REFERENCES [CustomerTypes] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_Customer_Name_IsActive] ON [Customers] ([Name], [IsActive]);
    CREATE INDEX [IX_Customer_CustomerType_IsActive] ON [Customers] ([CostumerTypeId], [IsActive]);

	SET IDENTITY_INSERT Customers ON;

    if not exists(select Id from Customers where Id = 1)
    begin
        insert into Customers(Id, Name, Address, IsActive, CostumerTypeId)
		values (1, 'Jhon Doe', 'building 00, apt. 45, UnitedStates', 1, 1);
    end;

    if not exists(select Id from Customers where Id = 2)
    begin
        insert into Customers(Id, Name, Address, IsActive, CostumerTypeId)
		values (2, 'Matt Smith', 'building 00, apt. 46, UnitedStates', 1, 1);
    end;

    if not exists(select Id from Customers where Id = 3)
    begin
        insert into Customers(Id, Name, Address, IsActive, CostumerTypeId)
		values (3, 'Amazon', 'building 00, fl. 3, Canada', 1, 1);
    end;

	SET IDENTITY_INSERT Customers OFF;
END
GO

IF OBJECT_ID(N'[Invoices]') IS NULL
BEGIN
    CREATE TABLE [Invoices] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [SubTotal] real NOT NULL,
        [TotalItbis] real NOT NULL,
        [Total] real NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Invoices] PRIMARY KEY ([Id])
    );

    CREATE INDEX [IX_Invoice_CustomerId_CreatedDate] ON [Invoices] ([CustomerId], [CreatedDate]);
END
GO

IF OBJECT_ID(N'[InvoiceDetails]') IS NULL
BEGIN
    CREATE TABLE [InvoiceDetails] (
        [Id] int NOT NULL IDENTITY,
        [InvoiceId] int NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Quantity] int NOT NULL,
        [Price] real NOT NULL,
        [Total] real NOT NULL,
        CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InvoiceDetails_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE CASCADE
    );

    CREATE INDEX [IX_InvoiceDetails_InvoiceId] ON [InvoiceDetails] ([InvoiceId]);
END
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230303132011_InitialCreate', N'7.0.3');
GO

COMMIT;
GO