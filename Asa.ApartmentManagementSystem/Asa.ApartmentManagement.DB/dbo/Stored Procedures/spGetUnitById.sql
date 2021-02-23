CREATE PROCEDURE [dbo].[SpGetUnitById]
	@unitId int
AS
BEGIN
	SELECT *
	from Unit u
	where u.UnitId = @unitId
END