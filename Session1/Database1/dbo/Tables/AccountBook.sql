CREATE TABLE [dbo].[AccountBook] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Categoryyy] INT              NOT NULL,
    [Amounttt]   INT              NOT NULL,
    [Dateee]     DATETIME         NOT NULL,
    [Remarkkk]   NVARCHAR (500)   NOT NULL,
    CONSTRAINT [PK_AccountBook] PRIMARY KEY CLUSTERED ([Id] ASC)
);

