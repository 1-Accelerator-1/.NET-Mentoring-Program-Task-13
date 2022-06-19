CREATE PROCEDURE [dbo].[OrderDeleteByMonth]
	@Month int
AS
	DELETE FROM [Order] 
	WHERE MONTH([CreatedDate]) = @Month;
