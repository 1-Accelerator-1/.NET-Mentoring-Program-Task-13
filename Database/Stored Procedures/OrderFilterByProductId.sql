CREATE PROCEDURE [dbo].[OrderFilterByProductId]
	@ProductId nvarchar(450)
AS
	SELECT [Id], [Status], [CreatedDate], [UpdatedDate], [ProductId] 
	FROM [Order] 
	WHERE [ProductId] = @ProductId;
