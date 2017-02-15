CREATE TABLE [dbo].[Project] (
    [Id]          INT        IDENTITY (0, 1)   NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

