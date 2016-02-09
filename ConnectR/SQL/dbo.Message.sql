CREATE TABLE [dbo].[Message] (
    [MessageId]      INT            NOT NULL IDENTITY,
    [ConversationId] INT            NOT NULL,
    [ProfileId]      INT            NOT NULL,
    [Date]           DATETIME       NOT NULL,
    [Text]           NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([MessageId] ASC),
    CONSTRAINT [FK_Message_ToTable] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([ProfileId]),
    CONSTRAINT [FK_Message_Conversation] FOREIGN KEY ([ConversationId]) REFERENCES [dbo].[Conversation] ([ConversationId])
);

