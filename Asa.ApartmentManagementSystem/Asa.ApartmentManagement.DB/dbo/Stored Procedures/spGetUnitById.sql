CREATE PROCEDURE [dbo].[spGetUnitById]
	@unitId int
AS
BEGIN
	SELECT *
	from Units u
	where u.Id = @unitId
END
