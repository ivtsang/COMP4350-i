CREATE TABLE [dbo].[Participant] (
    [ParticipantId]  INT NOT NULL IDENTITY,
    [ConversationId] INT NOT NULL,
    [ProfileId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ParticipantId] ASC),
    CONSTRAINT [FK_Participant_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([ProfileId]),
    CONSTRAINT [FK_Participant_Conversation] FOREIGN KEY ([ConversationId]) REFERENCES [dbo].[Conversation] ([ConversationId])
);

