CREATE TABLE [dbo].[Report] (
    [Id]   INT          IDENTITY (0, 1) NOT NULL,
    [TaskID]     INT           NOT NULL,
    [AccountID]  INT           NOT NULL,
    [Start Date] DATE          NOT NULL,
    [End Date]   DATE          NOT NULL,
    [Effort]     FLOAT (53)    NOT NULL,
    [Overtime]   FLOAT (53)    NOT NULL,
    [Descrition] VARCHAR (MAX) NULL,
    [Status]     VARCHAR (10)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AccountID]) REFERENCES [dbo].[Account] ([Id]),
    FOREIGN KEY ([TaskID]) REFERENCES [dbo].[Task] ([Id])
);

