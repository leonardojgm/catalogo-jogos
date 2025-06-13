USE [CatalogoJogos]
GO

/****** Object:  Table [dbo].[jogos]    Script Date: 13/06/2025 19:59:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[jogos](
	[Id] [uniqueidentifier] NULL,
	[Nome] [nchar](100) NULL,
	[Produtora] [nchar](100) NULL,
	[Preco] [float] NULL
) ON [PRIMARY]
GO

DROP TABLE [jogos]
