CREATE PROCEDURE [dbo].[AddTestDataToOrderTable]
AS
BEGIN
	INSERT [dbo].[Order] ([Id], [Status], [CreatedDate], [UpdatedDate], [ProductId]) 
	VALUES (N'r51b5bc6-650a-4fke-a6aa-315e444c3c15', 3, CAST(N'2021-06-21' AS DateTime), CAST(N'2022-07-21' AS DateTime), N'1ad2e869-5bfa-402a-ac26-672c68a89d57')
END
GO