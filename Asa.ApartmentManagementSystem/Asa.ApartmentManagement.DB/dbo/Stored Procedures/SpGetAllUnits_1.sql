CREATE procedure [dbo].[SpGetAllUnits]
@buildingId int
as
SELECT UnitId,BuildingId,Number ,Area
  FROM Unit
  where BuildingId=@buildingId