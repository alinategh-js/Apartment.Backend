CREATE TABLE [dbo].[Unit] (
    [UnitId]     INT            IDENTITY (1, 1) NOT NULL,
    [BuildingId] INT            NOT NULL,
    [Number]     INT            NOT NULL,
    [Area]       DECIMAL (5, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([UnitId] ASC),
    CONSTRAINT [Fk_unit_building] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Building] ([BuildingId])
);

