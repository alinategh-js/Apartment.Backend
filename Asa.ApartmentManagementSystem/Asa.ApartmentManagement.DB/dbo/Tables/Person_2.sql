CREATE TABLE [dbo].[Person] (
    [PersonId]    INT           IDENTITY (1, 1) NOT NULL,
    [FullName]    NVARCHAR (50) NOT NULL,
    [PhoneNumber] NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([PersonId] ASC)
);

