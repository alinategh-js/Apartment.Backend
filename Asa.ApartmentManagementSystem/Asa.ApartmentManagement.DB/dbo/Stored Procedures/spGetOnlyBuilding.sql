CREATE PROCEDURE [dbo].[spGetOnlyBuilding]
	
AS
BEGIN
	SELECT top 1 * from Building
END
