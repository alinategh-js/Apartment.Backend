CREATE PROCEDURE [dbo].[SpGetOnlyBuilding]
	
AS
BEGIN
	SELECT top 1 * from Building
END