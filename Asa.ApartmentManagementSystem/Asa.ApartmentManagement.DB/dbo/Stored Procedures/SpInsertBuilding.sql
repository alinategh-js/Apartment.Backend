CREATE procedure [dbo].[SpInsertBuilding]
@name nvarchar(50),
@numberOfUnits int
as

INSERT INTO [dbo].[Building]
           ([Name]
           ,[NumberOfUnits])
     VALUES
           (@name
           ,@numberOfUnits)
select SCOPE_IDENTITY()