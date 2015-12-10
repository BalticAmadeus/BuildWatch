IF OBJECT_ID('Event') IS NOT NULL
	DROP TABLE Event; 
GO

CREATE TABLE Event(
	[Id]			uniqueidentifier NOT NULL,
	[TimeStamp]		datetime2 NOT NULL,
	[FullTypeName]	varchar(200) NOT NULL,
	[Content]		ntext NOT NULL
)
GO

ALTER TABLE Event
	ADD CONSTRAINT [PK_Event] PRIMARY KEY ([Id])
GO