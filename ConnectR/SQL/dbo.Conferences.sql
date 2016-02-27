CREATE TABLE [dbo].[Conferences] (
    [ConferenceId] INT            NOT NULL IDENTITY,
    [ProfileId]    INT            NOT NULL,
    [Content]      NVARCHAR (MAX) NULL,
    [Image]        BINARY (2048)  NULL,
    [Title]        NVARCHAR (50)  NULL,
    [Date]         DATETIME       NULL,
    [Location]     NVARCHAR (150) NULL,
    CONSTRAINT [PK_Conferences] PRIMARY KEY CLUSTERED ([ConferenceId] ASC),
    CONSTRAINT [FK_Conference_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profiles] ([ProfileId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Conference_Profile]
    ON [dbo].[Conferences]([ProfileId] ASC);

