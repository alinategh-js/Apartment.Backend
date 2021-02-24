CREATE PROCEDURE [dbo].[SpPersonById]
	@personId int
AS
BEGIN
	SELECT *
	from Person p
	where p.PersonId = @personId
END
