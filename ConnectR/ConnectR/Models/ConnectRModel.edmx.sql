
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/21/2016 22:37:21
-- Generated from EDMX file: C:\Users\Lukas\Source\Repos\COMP4350-i\ConnectR\ConnectR\Models\ConnectRModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-ConnectR-20160201042538];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Conference_Profile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Conferences] DROP CONSTRAINT [FK_Conference_Profile];
GO
IF OBJECT_ID(N'[dbo].[FK_Message_Conversation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Message_Conversation];
GO
IF OBJECT_ID(N'[dbo].[FK_Message_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Message_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Participant_Conversation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participants] DROP CONSTRAINT [FK_Participant_Conversation];
GO
IF OBJECT_ID(N'[dbo].[FK_Participant_Profile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participants] DROP CONSTRAINT [FK_Participant_Profile];
GO
IF OBJECT_ID(N'[dbo].[FK_FileProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_FileProfile];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Conferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conferences];
GO
IF OBJECT_ID(N'[dbo].[Profiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Profiles];
GO
IF OBJECT_ID(N'[dbo].[Conversations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Conversations];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[Participants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participants];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Conferences'
CREATE TABLE [dbo].[Conferences] (
    [ConferenceId] int  NOT NULL,
    [ProfileId] int  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [Image] binary(2048)  NULL,
    [Title] nvarchar(50)  NULL,
    [Date] datetime  NULL,
    [Location] nvarchar(150)  NULL
);
GO

-- Creating table 'Profiles'
CREATE TABLE [dbo].[Profiles] (
    [ProfileId] int  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Age] int  NULL,
    [Country] nvarchar(50)  NULL,
    [City] nvarchar(50)  NULL,
    [School] nvarchar(50)  NULL,
    [Degree] nvarchar(50)  NULL,
    [Image] binary(2048)  NULL
);
GO

-- Creating table 'Conversations'
CREATE TABLE [dbo].[Conversations] (
    [ConversationId] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [MessageId] int  NOT NULL,
    [ConversationId] int  NOT NULL,
    [ProfileId] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Text] nvarchar(max)  NULL
);
GO

-- Creating table 'Participants'
CREATE TABLE [dbo].[Participants] (
    [ParticipantId] int  NOT NULL,
    [ConversationId] int  NOT NULL,
    [ProfileId] int  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProfileId] int  NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [ContentType] nvarchar(max)  NOT NULL,
    [Content] tinyint  NOT NULL,
    [FileType] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ConferenceId] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [PK_Conferences]
    PRIMARY KEY CLUSTERED ([ConferenceId] ASC);
GO

-- Creating primary key on [ProfileId] in table 'Profiles'
ALTER TABLE [dbo].[Profiles]
ADD CONSTRAINT [PK_Profiles]
    PRIMARY KEY CLUSTERED ([ProfileId] ASC);
GO

-- Creating primary key on [ConversationId] in table 'Conversations'
ALTER TABLE [dbo].[Conversations]
ADD CONSTRAINT [PK_Conversations]
    PRIMARY KEY CLUSTERED ([ConversationId] ASC);
GO

-- Creating primary key on [MessageId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [ParticipantId] in table 'Participants'
ALTER TABLE [dbo].[Participants]
ADD CONSTRAINT [PK_Participants]
    PRIMARY KEY CLUSTERED ([ParticipantId] ASC);
GO

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProfileId] in table 'Conferences'
ALTER TABLE [dbo].[Conferences]
ADD CONSTRAINT [FK_Conference_Profile]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profiles]
        ([ProfileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Conference_Profile'
CREATE INDEX [IX_FK_Conference_Profile]
ON [dbo].[Conferences]
    ([ProfileId]);
GO

-- Creating foreign key on [ConversationId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Message_Conversation]
    FOREIGN KEY ([ConversationId])
    REFERENCES [dbo].[Conversations]
        ([ConversationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_Conversation'
CREATE INDEX [IX_FK_Message_Conversation]
ON [dbo].[Messages]
    ([ConversationId]);
GO

-- Creating foreign key on [ProfileId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Message_ToTable]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profiles]
        ([ProfileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Message_ToTable'
CREATE INDEX [IX_FK_Message_ToTable]
ON [dbo].[Messages]
    ([ProfileId]);
GO

-- Creating foreign key on [ConversationId] in table 'Participants'
ALTER TABLE [dbo].[Participants]
ADD CONSTRAINT [FK_Participant_Conversation]
    FOREIGN KEY ([ConversationId])
    REFERENCES [dbo].[Conversations]
        ([ConversationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participant_Conversation'
CREATE INDEX [IX_FK_Participant_Conversation]
ON [dbo].[Participants]
    ([ConversationId]);
GO

-- Creating foreign key on [ProfileId] in table 'Participants'
ALTER TABLE [dbo].[Participants]
ADD CONSTRAINT [FK_Participant_Profile]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profiles]
        ([ProfileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participant_Profile'
CREATE INDEX [IX_FK_Participant_Profile]
ON [dbo].[Participants]
    ([ProfileId]);
GO

-- Creating foreign key on [ProfileId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_FileProfile]
    FOREIGN KEY ([ProfileId])
    REFERENCES [dbo].[Profiles]
        ([ProfileId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileProfile'
CREATE INDEX [IX_FK_FileProfile]
ON [dbo].[Files]
    ([ProfileId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------