CREATE TABLE [dbo].[Conference]
(
	[ConferenceId] INT NOT NULL PRIMARY KEY, 
    [ProfileId] INT NOT NULL, 
    [Content] NVARCHAR(MAX) NULL, 
    [Image] BINARY(2048) NULL, 
    CONSTRAINT [FK_Conference_Profile] FOREIGN KEY (ProfileId) REFERENCES Profile(ProfileId)
)
