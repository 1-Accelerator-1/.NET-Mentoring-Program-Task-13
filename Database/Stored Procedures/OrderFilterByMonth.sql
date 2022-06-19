CREATE PROCEDURE [dbo].[OrderFilterByMonth]
	@Month int
AS
	SELECT [Id], [Status], [CreatedDate], [UpdatedDate], [ProductId] 
	FROM [Order] 
	WHERE MONTH([CreatedDate]) = @Month;
