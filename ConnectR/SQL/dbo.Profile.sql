﻿CREATE TABLE [dbo].[Profile] (
    [ProfileId] INT            NOT NULL IDENTITY,
    [UserId]    NVARCHAR (128) NOT NULL,
    [FirstName] NVARCHAR (50)  NULL,
    [LastName]  NVARCHAR (50)  NULL,
    [Age]       INT            NULL,
    [Country]   NVARCHAR (50)  NULL,
    [City]      NVARCHAR (50)  NULL,
    [School]    NVARCHAR (50)  NULL,
    [Degree]    NVARCHAR (50)  NULL,
    [Image]     BINARY (2048)  NULL,
    PRIMARY KEY CLUSTERED ([ProfileId] ASC),
    CONSTRAINT [FK_Profile_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

