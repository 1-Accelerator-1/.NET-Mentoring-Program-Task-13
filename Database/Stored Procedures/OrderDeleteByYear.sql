CREATE PROCEDURE [dbo].[OrderDeleteByYear]
	@Year int
AS
	DELETE FROM [Order] 
	WHERE YEAR([CreatedDate]) = @Year;
