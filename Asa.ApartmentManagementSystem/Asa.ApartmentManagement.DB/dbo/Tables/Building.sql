CREATE TABLE [dbo].[Building] (
    [BuildingId]    INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [NumberOfUnits] INT           NOT NULL,
    CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED ([BuildingId] ASC)
);

