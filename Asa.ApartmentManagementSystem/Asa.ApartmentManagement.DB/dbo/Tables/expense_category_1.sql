CREATE TABLE [dbo].[expense_category] (
    [Id]      INT           NOT NULL,
    [name]    NVARCHAR (20) NOT NULL,
    [formula_id] INT     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

