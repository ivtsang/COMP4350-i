CREATE TABLE [dbo].[Followers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FollowerId] INT NULL, 
    [FollowingId] INT NULL, 
    CONSTRAINT [FK_Follower_ToProfile] FOREIGN KEY ([FollowerId]) REFERENCES [Profiles]([ProfileId]), 
    CONSTRAINT [FK_Following_ToProfile] FOREIGN KEY ([FollowingId]) REFERENCES [Profiles]([ProfileId])
)
