CREATE TABLE [dbo].[PersonUnit] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [UnitId]   INT      NOT NULL,
    [PersonId] INT      NOT NULL,
    [From]     DATETIME NOT NULL,
    [To]       DATETIME NULL,
    [IsOwner]  BIT      CONSTRAINT [DF_owner_tenant_is_owner] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_owner_tenant] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_owner_tenant_person] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId]),
    CONSTRAINT [FK_owner_tenant_Units] FOREIGN KEY ([UnitId]) REFERENCES [dbo].[Unit] ([UnitId])
);

