USE [CatalogoJogos]
GO

INSERT INTO [dbo].[jogos]
           ([Id]
           ,[Nome]
           ,[Produtora]
           ,[Preco])
     VALUES
           ('0ca314a5-9282-45d8-92c3-2985f2a9fd04'
           ,'Fifa 21'
           ,'EA'
           ,200)
GO

INSERT INTO [dbo].[jogos]
           ([Id]
           ,[Nome]
           ,[Produtora]
           ,[Preco])
     VALUES
           ('92576bd2-388e-4f5d-96c1-8bfda6c5a268'
           ,'Street Fighter V'
           ,'Capcom'
           ,80)
GO


INSERT INTO [dbo].[jogos]
           ([Id]
           ,[Nome]
           ,[Produtora]
           ,[Preco])
     VALUES
           ('c3c9b5da-6a45-4de1-b28b-491cbf83b589'
           ,'Grand Theft Auto V'
           ,'Rockstar'
           ,190)
GO

select * from [jogos]