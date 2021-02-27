CREATE PROCEDURE [dbo].[SpUpdatePerson]
	@personId int,
    @fullName nvarchar(50),
    @phoneNumber nchar(11)
AS
	UPDATE Person  
            SET    FullName = @fullName,
                   PhoneNumber = @phoneNumber
            WHERE  PersonId = @personId  
RETURN 0
