CREATE TABLE [dbo].[Expense] (
    [ExpenseId]         INT           NOT NULL,
    [Title]             NVARCHAR (20) NOT NULL,
    [ExpenseCategoryId] INT           NOT NULL,
    [From]              DATETIME      NOT NULL,
    [To]                DATETIME      NOT NULL,
    [Cost]              DECIMAL (18)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ExpenseId] ASC)
);

