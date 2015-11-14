CREATE TABLE Build(
	[Id]			int NOT NULL,
	[Name]			varchar NOT NULL,
	[Instance]		varchar NOT NULL,
	[TimeStamp]		datetime2 NOT NULL,
	[Status]		int NOT NULL,
	[Username]			varchar NOT NULL,

	CONSTRAINT [PK_Build] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]

GO