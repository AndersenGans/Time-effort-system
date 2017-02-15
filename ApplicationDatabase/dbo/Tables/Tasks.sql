CREATE TABLE [dbo].[Task] (
    [Id]    INT       IDENTITY (0, 1)   NOT NULL,
    [Title]     VARCHAR (50) NOT NULL,
    [ProjectID] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([Id])
);

