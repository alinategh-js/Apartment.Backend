CREATE procedure [dbo].[person_create]
@name nvarchar(50),
@phone_number nvarchar(50)
as

INSERT INTO [dbo].[Person]
           ([name]
           ,[phone_number])
     VALUES
           (@name
           ,@phone_number)
select SCOPE_IDENTITY()
