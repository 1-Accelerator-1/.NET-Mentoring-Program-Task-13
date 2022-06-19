CREATE PROCEDURE [dbo].[DeleteTestDataFromOrderTable]
AS
BEGIN
	DELETE FROM [dbo].[Order] WHERE ([Id] != N'f41b5bc6-650a-4fke-a6aa-315eg24c3c15') and 
	([Id] != N'cf0fvv1f-c650-42cc-a735-5a90f349f218') and
	([Id] != N'763cf8b8-4fdf-4fad-8d1d-a7cgg9b71a34') and
	([Id] != N'd5631f81-1ff8-4fcc-a1d8-801142f5809c');
END
GO