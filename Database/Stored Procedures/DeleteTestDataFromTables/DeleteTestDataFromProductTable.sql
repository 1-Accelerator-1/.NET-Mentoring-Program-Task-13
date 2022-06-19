CREATE PROCEDURE [dbo].[DeleteTestDataFromProductTable]
AS
BEGIN
	DELETE FROM [dbo].[Product] WHERE ([Id] != N'541f2bc6-850a-4f5e-abaa-315ec24c3c15') and
	([Id] != N'cf0f951f-c650-42cc-a735-5a90f349f218') and
	([Id] != N'1ad2e869-5bfa-402a-ac26-672c68a89d57');
END
GO