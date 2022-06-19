CREATE PROCEDURE [dbo].[OrderDeleteByProductId]
	@ProductId nvarchar(450)
AS
	DELETE FROM [Order] 
	WHERE [ProductId] = @ProductId;
