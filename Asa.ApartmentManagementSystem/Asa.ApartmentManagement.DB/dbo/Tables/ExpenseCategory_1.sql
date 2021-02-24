CREATE TABLE [dbo].[ExpenseCategory] (
    [ExpenseCategoryId] INT           NOT NULL,
    [Name]              NVARCHAR (20) NOT NULL,
    [FormulaId]         INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ExpenseCategoryId] ASC)
);

