﻿CREATE TABLE [User]
(
	ID			INT PRIMARY KEY IDENTITY(1,1),
	Email		NVARCHAR(50)	NOT NULL,
	Username	NVARCHAR(50)	NOT NULL,
	Password	NVARCHAR(50)	NOT NULL,
)

CREATE TABLE [CardSet]
(
	ID					INT PRIMARY KEY IDENTITY(1,1),
	Name				NVARCHAR(50)	NOT NULL,
			
	/*Foreign Keys (below)---*/
	UserOwnerId		INT			NOT NULL,

	CONSTRAINT [FK_CardSet_ID] FOREIGN KEY ([UserOwnerId]) REFERENCES [Dbo].[User]([ID])
)


CREATE TABLE Cards
(
	ID				INT PRIMARY KEY IDENTITY(1,1),
	FrontCard		NVARCHAR(300)		NOT NULL,
	BackCard		NVARCHAR(300)		NOT NULL,

	CardSetId		INT					NOT NULL,

	CONSTRAINT [FK_Cards_ID] FOREIGN KEY ([CardSetId]) REFERENCES [Dbo].[CardSet]([ID])
)