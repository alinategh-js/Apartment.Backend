CREATE procedure [dbo].[SpInsertUnit]
@buildingId int,
@unitNumber int,
@area		decimal
as

INSERT INTO Unit
           (BuildingId
           ,Number
		   ,Area)
     VALUES
           (@buildingId
           ,@unitNumber
		   ,@area)
select SCOPE_IDENTITY()