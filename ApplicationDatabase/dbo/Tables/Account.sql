CREATE TABLE [dbo].[Account] (
    [Id]            INT          IDENTITY (0, 1) NOT NULL,
    [Name]          VARCHAR (50) NOT NULL,
    [MiddleName]    VARCHAR (40) NULL,
    [Surname]       VARCHAR (50) NOT NULL,
    [Email]         VARCHAR (50) NOT NULL,
    [Login]         VARCHAR (50) NOT NULL,
    [Password]      VARCHAR (50) NOT NULL,
    [Access Level ] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

