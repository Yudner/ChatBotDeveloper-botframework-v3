CREATE TABLE [dbo].[SqlBotDataEntities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BotStoreType] [int] NOT NULL,
	[BotId] [nvarchar](max) NULL,
	[ChannelId] [nvarchar](200) NULL,
	[ConversationId] [nvarchar](200) NULL,
	[UserId] [nvarchar](200) NULL,
	[Data] [varbinary](max) NULL,
	[ETag] [nvarchar](max) NULL,
	[ServiceUrl] [nvarchar](max) NULL,
	[Timestamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_SqlBotDataEntities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
