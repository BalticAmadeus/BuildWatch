IF OBJECT_ID('AppSetting') IS NOT NULL
	DROP TABLE AppSetting; 
GO

CREATE TABLE AppSetting(
	[AppSettingKey]	varchar(200) NOT NULL,
	[AppKey]		varchar(200) NOT NULL,
	[Type]			varchar(200) NOT NULL,
	[Value]			varchar(4000) NOT NULL,
	[IsDeleted]		bit NOT NULL
)
GO

ALTER TABLE AppSetting
	ADD CONSTRAINT [PK_AppSetting] PRIMARY KEY ([AppSettingKey], [AppKey])
GO

