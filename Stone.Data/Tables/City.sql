﻿CREATE TABLE [dbo].[City]
(
	[ID] UNIQUEIDENTIFIER NOT NULL, 
    [Name] VARCHAR(100) NOT NULL,
	CONSTRAINT [PK_City] PRIMARY KEY ([ID])
)
