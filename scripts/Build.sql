CREATE TABLE Build(
	[BuildDefinitionId] int NOT NULL,
	[Id]				int NOT NULL,
	[Name]				varchar(200) NOT NULL,
	[Instance]			varchar(200) NOT NULL,
	[TimeStamp]			datetime2 NOT NULL,
	[Status]			int NOT NULL,
	[Username]			varchar(200) NOT NULL,
	[IsDeleted]			bit NOT NULL,

	CONSTRAINT [PK_Build] PRIMARY KEY CLUSTERED ([BuildDefinitionId] ASC, [Id] DESC)
) ON [PRIMARY]

GO