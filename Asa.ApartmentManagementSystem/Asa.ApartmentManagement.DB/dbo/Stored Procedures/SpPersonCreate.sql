CREATE procedure [dbo].[SpPersonCreate]
@name nvarchar(50),
@phone_number nvarchar(50)
as

INSERT INTO [dbo].[Person]
           ([full_name]
           ,[phone_number])
     VALUES
           (@name
           ,@phone_number)
select SCOPE_IDENTITY()
