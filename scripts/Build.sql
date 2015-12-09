IF OBJECT_ID('Build') IS NOT NULL
	DROP TABLE Build; 
GO

CREATE TABLE Build(
	[Id]			varchar(200) NOT NULL,
	[AliasTitle]	varchar(200) NOT NULL,
	[IsDeleted]		bit NOT NULL
)
GO

ALTER TABLE Build
	ADD CONSTRAINT [PK_Build] PRIMARY KEY ([Id])
GO