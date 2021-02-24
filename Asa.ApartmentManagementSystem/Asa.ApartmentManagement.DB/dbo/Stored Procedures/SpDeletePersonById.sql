CREATE PROCEDURE [dbo].[SpDeletePersonById]
	@personId int
AS
	DELETE FROM Person  
    WHERE  PersonId = @personId 
RETURN 0
