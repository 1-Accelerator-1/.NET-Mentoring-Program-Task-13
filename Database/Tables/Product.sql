CREATE TABLE [dbo].[Product]
(
	[Id] nvarchar(450) NOT NULL PRIMARY KEY,
	[Name] nvarchar(100) not null,
	[Description] nvarchar(max) not null,
	[Weight] decimal(18, 9) not null,
	[Height] decimal(18, 9) not null,
	[Width] decimal(18, 9) not null,
	[Length] decimal(18, 9) not null
)
