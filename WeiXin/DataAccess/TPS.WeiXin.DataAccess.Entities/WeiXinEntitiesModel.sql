
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/12/2017 21:29:17
-- Generated from EDMX file: F:\Github\TPS-Project\WeiXin\DataAccess\TPS.WeiXin.DataAccess.Entities\WeiXinEntitiesModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TPS.WeiXin];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Reply_Message]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reply] DROP CONSTRAINT [FK_Reply_Message];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[CustomMenu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomMenu];
GO
IF OBJECT_ID(N'[dbo].[Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Message];
GO
IF OBJECT_ID(N'[dbo].[Reply]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reply];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Account'
CREATE TABLE [dbo].[Account] (
    [ID] uniqueidentifier  NOT NULL,
    [WeiXinNum] nvarchar(50)  NOT NULL,
    [AppID] char(18)  NULL,
    [AppSecret] varchar(64)  NULL,
    [Token] varchar(50)  NOT NULL,
    [EncodingAESKey] char(43)  NULL,
    [AgentID] int  NULL,
    [IsCorp] bit  NOT NULL
);
GO

-- Creating table 'CustomMenu'
CREATE TABLE [dbo].[CustomMenu] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(10)  NOT NULL,
    [Type] int  NOT NULL,
    [ParentMenuID] uniqueidentifier  NULL,
    [Key] varchar(20)  NULL,
    [View_Url] nchar(10)  NULL,
    [Status] int  NOT NULL,
    [AccountID] uniqueidentifier  NOT NULL,
    [Sort] int  NOT NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'Message'
CREATE TABLE [dbo].[Message] (
    [ID] uniqueidentifier  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [IsTemplate] bit  NOT NULL,
    [Type] int  NOT NULL
);
GO

-- Creating table 'Reply'
CREATE TABLE [dbo].[Reply] (
    [ID] uniqueidentifier  NOT NULL,
    [Key] nvarchar(20)  NOT NULL,
    [KeyType] int  NOT NULL,
    [Status] int  NOT NULL,
    [AccountID] uniqueidentifier  NOT NULL,
    [TypeFullName] varchar(200)  NULL,
    [MessageID] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Account'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [PK_Account]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CustomMenu'
ALTER TABLE [dbo].[CustomMenu]
ADD CONSTRAINT [PK_CustomMenu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Message'
ALTER TABLE [dbo].[Message]
ADD CONSTRAINT [PK_Message]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Reply'
ALTER TABLE [dbo].[Reply]
ADD CONSTRAINT [PK_Reply]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MessageID] in table 'Reply'
ALTER TABLE [dbo].[Reply]
ADD CONSTRAINT [FK_Reply_Message]
    FOREIGN KEY ([MessageID])
    REFERENCES [dbo].[Message]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Reply_Message'
CREATE INDEX [IX_FK_Reply_Message]
ON [dbo].[Reply]
    ([MessageID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------