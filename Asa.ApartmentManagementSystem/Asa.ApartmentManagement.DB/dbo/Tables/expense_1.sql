CREATE TABLE [dbo].[expense] (
    [Id]       INT           NOT NULL,
    [title]    NVARCHAR (20) NOT NULL,
    [category_id] INT     NOT NULL,
    [from]     DATETIME      NOT NULL,
    [to]       DATETIME      NOT NULL,
    [cost]     DECIMAL (18)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

