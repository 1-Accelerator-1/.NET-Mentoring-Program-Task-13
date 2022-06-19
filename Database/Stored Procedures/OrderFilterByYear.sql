CREATE PROCEDURE [dbo].[OrderFilterByYear]
	@Year int
AS
	SELECT [Id], [Status], [CreatedDate], [UpdatedDate], [ProductId] 
	FROM [Order] 
	WHERE YEAR([CreatedDate]) = @Year;
