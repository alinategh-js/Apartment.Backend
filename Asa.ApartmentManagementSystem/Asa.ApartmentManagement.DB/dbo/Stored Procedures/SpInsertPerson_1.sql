CREATE procedure [dbo].[SpInsertPerson]
@name nvarchar(50),
@phoneNumber nvarchar(50)
as

INSERT INTO [dbo].[Person]
           ([FullName]
           ,[PhoneNumber])
     VALUES
           (@name
           ,@phoneNumber)
select SCOPE_IDENTITY()