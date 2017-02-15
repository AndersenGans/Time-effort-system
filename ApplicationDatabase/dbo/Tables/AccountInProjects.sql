CREATE TABLE [dbo].[Roles] (
    [AccountId] INT          NOT NULL,
    [ProjectId] INT          NOT NULL,
    [RoleId]    INT NOT NULL,
	PRIMARY KEY ([AccountId], [ProjectId]),
    FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[ProjectRoles] ([Id]),
);

