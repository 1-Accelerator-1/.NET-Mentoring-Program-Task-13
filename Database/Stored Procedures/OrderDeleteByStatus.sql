CREATE PROCEDURE [dbo].[OrderDeleteByStatus]
	@Status int
AS
	DELETE FROM [Order] 
	WHERE [Status] = @Status;
