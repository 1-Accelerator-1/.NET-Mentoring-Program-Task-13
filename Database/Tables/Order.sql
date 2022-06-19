CREATE TABLE [dbo].[Order]
(
	[Id] nvarchar(450) NOT NULL PRIMARY KEY,
	[Status] int Not Null,
	[CreatedDate] datetime not null,
	[UpdatedDate] datetime not null,
	[ProductId] nvarchar(450) not null
)
