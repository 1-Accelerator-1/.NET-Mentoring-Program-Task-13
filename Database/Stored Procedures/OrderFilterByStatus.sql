CREATE PROCEDURE [dbo].[OrderFilterByStatus]
	@Status int
AS
	SELECT [Id], [Status], [CreatedDate], [UpdatedDate], [ProductId] 
	FROM [Order] 
	WHERE [Status] = @Status;
