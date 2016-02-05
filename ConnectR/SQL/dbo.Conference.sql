CREATE TABLE [dbo].[Conference] (
    [ConferenceId] INT            IDENTITY (1, 1) NOT NULL,
    [ProfileId]    INT            NOT NULL,
    [Content]      NVARCHAR (MAX) NULL,
    [Image]        BINARY (2048)  NULL,
    [Title] NVARCHAR(50) NULL, 
    [Date] DATETIME NULL, 
    [Location] NVARCHAR(150) NULL, 
    PRIMARY KEY CLUSTERED ([ConferenceId] ASC),
    CONSTRAINT [FK_Conference_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[Profile] ([ProfileId])
);

