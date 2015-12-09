IF OBJECT_ID('Setting') IS NOT NULL
	DROP TABLE Setting; 
GO

CREATE TABLE Setting(
	[Key]			varchar(200) NOT NULL,
	[Category]		varchar(200) NOT NULL,
	[Type]			varchar(200) NOT NULL,
	[Value]			varchar(4000) NOT NULL,
	[IsDeleted]		bit NOT NULL
)
GO

ALTER TABLE Setting
	ADD CONSTRAINT [PK_Setting] PRIMARY KEY ([Key], [Category])
GO

