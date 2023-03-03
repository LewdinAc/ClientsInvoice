use [ClientInvoicesDB];

if object_id(N'[InvoiceDetails]') is not null
begin
	drop table InvoiceDetails;
end;

if object_id(N'[Invoices]') is not null
begin
	drop table Invoices;
end;

if object_id(N'[Customers]') is not null
begin
	drop table Customers;
end;	

if object_id(N'[CustomerTypes]') is not null
begin
	drop table CustomerTypes;
end;

if object_id(N'[__EFMigrationsHistory]') is not null
begin
	drop table __EFMigrationsHistory;
end