CREATE PROCEDURE [dbo].[AddTestDataToProductTable]
AS
BEGIN
	INSERT [dbo].[Product] ([Id], [Name], [Description], [Weight], [Height], [Width], [Length]) 
	VALUES (N'hy1f2bc6-850a-4f5e-abaa-315ec24c3c15', N'Test Product', N'Test Product Description', 100.2, 54.4, 45, 67.3)
END
GO