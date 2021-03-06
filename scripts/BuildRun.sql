IF OBJECT_ID('BuildRun') IS NOT NULL
	DROP TABLE BuildRun; 
GO

CREATE TABLE BuildRun(
	[Id]				varchar(200) NOT NULL,
	[BuildId]			varchar(200) NOT NULL,
	[Instance]			varchar(200) NOT NULL,
	[StartedTimeStamp]	datetime2 NOT NULL,
	[FinishedTimeStamp]	datetime2 NULL,
	[Status]			int NOT NULL,
	[Username]			varchar(200) NOT NULL,
	[IsDeleted]			bit NOT NULL)
GO

ALTER TABLE BuildRun
	ADD CONSTRAINT [PK_BuildRun] PRIMARY KEY ([Id])
GO