CREATE TABLE BuildDefinition(
	[Id]			int NOT NULL,
	[AliasTitle]	varchar NOT NULL,
	[OriginalTitle]	varchar NOT NULL,
	[IsDeleted]		bit NOT NULL,

	CONSTRAINT [PK_BuildDefinition] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]

GO