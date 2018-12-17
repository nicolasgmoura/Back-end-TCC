
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/14/2018 22:03:41
-- Generated from EDMX file: C:\Users\Nicolas\Documents\Visual Studio 2015\Projects\EMarketing\EMarketing\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EMarketing];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Acompanhamento_Consumidor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acompanhamento] DROP CONSTRAINT [FK_Acompanhamento_Consumidor];
GO
IF OBJECT_ID(N'[dbo].[FK_Acompanhamento_Empresa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acompanhamento] DROP CONSTRAINT [FK_Acompanhamento_Empresa];
GO
IF OBJECT_ID(N'[dbo].[FK_Cidade_Estado]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cidade] DROP CONSTRAINT [FK_Cidade_Estado];
GO
IF OBJECT_ID(N'[dbo].[FK_Comentario_Publicacao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentario] DROP CONSTRAINT [FK_Comentario_Publicacao];
GO
IF OBJECT_ID(N'[dbo].[FK_Comentario_Resposta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentario] DROP CONSTRAINT [FK_Comentario_Resposta];
GO
IF OBJECT_ID(N'[dbo].[FK_Denuncia_Comentario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncia] DROP CONSTRAINT [FK_Denuncia_Comentario];
GO
IF OBJECT_ID(N'[dbo].[FK_Denuncia_Publicacao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncia] DROP CONSTRAINT [FK_Denuncia_Publicacao];
GO
IF OBJECT_ID(N'[dbo].[FK_Empresa_Segmento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Empresa] DROP CONSTRAINT [FK_Empresa_Segmento];
GO
IF OBJECT_ID(N'[dbo].[FK_Publicacao_Usuario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Publicacao] DROP CONSTRAINT [FK_Publicacao_Usuario];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Cidade]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Cidade];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Consumidor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Consumidor];
GO
IF OBJECT_ID(N'[dbo].[FK_Usuario_Empresa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuario] DROP CONSTRAINT [FK_Usuario_Empresa];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Acompanhamento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Acompanhamento];
GO
IF OBJECT_ID(N'[dbo].[Cidade]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cidade];
GO
IF OBJECT_ID(N'[dbo].[Comentario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comentario];
GO
IF OBJECT_ID(N'[dbo].[Consumidor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consumidor];
GO
IF OBJECT_ID(N'[dbo].[Denuncia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Denuncia];
GO
IF OBJECT_ID(N'[dbo].[Empresa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Empresa];
GO
IF OBJECT_ID(N'[dbo].[Estado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Estado];
GO
IF OBJECT_ID(N'[dbo].[Publicacao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Publicacao];
GO
IF OBJECT_ID(N'[dbo].[Segmento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Segmento];
GO
IF OBJECT_ID(N'[dbo].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Acompanhamento'
CREATE TABLE [dbo].[Acompanhamento] (
    [IdAcompanhamento] int IDENTITY(1,1) NOT NULL,
    [IdEmpresa] int  NOT NULL,
    [IdConsumidor] int  NOT NULL
);
GO

-- Creating table 'Comentario'
CREATE TABLE [dbo].[Comentario] (
    [IdComentario] int IDENTITY(1,1) NOT NULL,
    [IdPublicacao] int  NOT NULL,
    [Descricao] varchar(100)  NOT NULL,
    [IdResposta] int  NOT NULL,
    [DataComentario] datetime  NOT NULL
);
GO

-- Creating table 'Denuncia'
CREATE TABLE [dbo].[Denuncia] (
    [IdDenuncia] int IDENTITY(1,1) NOT NULL,
    [IdPublicacao] int  NOT NULL,
    [IdComentario] int  NOT NULL,
    [Mensagem] varchar(100)  NOT NULL,
    [DataDenuncia] datetime  NOT NULL
);
GO

-- Creating table 'Publicacao'
CREATE TABLE [dbo].[Publicacao] (
    [IdPublicacao] int IDENTITY(1,1) NOT NULL,
    [IdUsuario] int  NOT NULL,
    [ImagemPublicacao] varbinary(max)  NOT NULL,
    [DataPublicacao] datetime  NOT NULL,
    [Titulo] varchar(50)  NULL,
    [AreaPublicacao] varchar(50)  NULL,
    [QuantidadeComentarios] int  NOT NULL,
    [QuantidadeDenuncias] int  NOT NULL,
    [QuantidadeVisualizacoes] int  NOT NULL
);
GO

-- Creating table 'Cidade'
CREATE TABLE [dbo].[Cidade] (
    [IdCidade] int  NOT NULL,
    [CodigoIBGE] char(10)  NOT NULL,
    [IdEstado] int  NOT NULL,
    [Descricao] varchar(40)  NOT NULL
);
GO

-- Creating table 'Consumidor'
CREATE TABLE [dbo].[Consumidor] (
    [IdConsumidor] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(30)  NOT NULL,
    [DataNascimento] datetime  NOT NULL,
    [QuantidadeSeguimentos] int  NOT NULL
);
GO

-- Creating table 'Empresa'
CREATE TABLE [dbo].[Empresa] (
    [IdEmpresa] int IDENTITY(1,1) NOT NULL,
    [IdSegmento] int  NOT NULL,
    [CNPJ] varchar(14)  NOT NULL,
    [RazaoSocial] varchar(30)  NOT NULL,
    [Fantasia] varchar(30)  NOT NULL,
    [Endereco] varchar(50)  NOT NULL,
    [Numero] int  NOT NULL,
    [Bairro] varchar(30)  NOT NULL,
    [CEP] char(8)  NOT NULL,
    [Logo] varbinary(max)  NULL,
    [QuantidadeSeguidores] int  NOT NULL
);
GO

-- Creating table 'Estado'
CREATE TABLE [dbo].[Estado] (
    [IdEstado] int  NOT NULL,
    [CodigoIBGE] char(2)  NOT NULL,
    [Sigla] char(2)  NOT NULL,
    [Descricao] varchar(20)  NOT NULL
);
GO

-- Creating table 'Segmento'
CREATE TABLE [dbo].[Segmento] (
    [IdSegmento] int IDENTITY(1,1) NOT NULL,
    [Descricao] varchar(30)  NOT NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] int IDENTITY(1,1) NOT NULL,
    [NomeUsuario] varchar(30)  NOT NULL,
    [Senha] varchar(20)  NOT NULL,
    [Email] varchar(30)  NOT NULL,
    [IsEmpresa] bit  NOT NULL,
    [IdEmpresa] int  NULL,
    [IdConsumidor] int  NULL,
    [IdCidade] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAcompanhamento] in table 'Acompanhamento'
ALTER TABLE [dbo].[Acompanhamento]
ADD CONSTRAINT [PK_Acompanhamento]
    PRIMARY KEY CLUSTERED ([IdAcompanhamento] ASC);
GO

-- Creating primary key on [IdComentario] in table 'Comentario'
ALTER TABLE [dbo].[Comentario]
ADD CONSTRAINT [PK_Comentario]
    PRIMARY KEY CLUSTERED ([IdComentario] ASC);
GO

-- Creating primary key on [IdDenuncia] in table 'Denuncia'
ALTER TABLE [dbo].[Denuncia]
ADD CONSTRAINT [PK_Denuncia]
    PRIMARY KEY CLUSTERED ([IdDenuncia] ASC);
GO

-- Creating primary key on [IdPublicacao] in table 'Publicacao'
ALTER TABLE [dbo].[Publicacao]
ADD CONSTRAINT [PK_Publicacao]
    PRIMARY KEY CLUSTERED ([IdPublicacao] ASC);
GO

-- Creating primary key on [IdCidade] in table 'Cidade'
ALTER TABLE [dbo].[Cidade]
ADD CONSTRAINT [PK_Cidade]
    PRIMARY KEY CLUSTERED ([IdCidade] ASC);
GO

-- Creating primary key on [IdConsumidor] in table 'Consumidor'
ALTER TABLE [dbo].[Consumidor]
ADD CONSTRAINT [PK_Consumidor]
    PRIMARY KEY CLUSTERED ([IdConsumidor] ASC);
GO

-- Creating primary key on [IdEmpresa] in table 'Empresa'
ALTER TABLE [dbo].[Empresa]
ADD CONSTRAINT [PK_Empresa]
    PRIMARY KEY CLUSTERED ([IdEmpresa] ASC);
GO

-- Creating primary key on [IdEstado] in table 'Estado'
ALTER TABLE [dbo].[Estado]
ADD CONSTRAINT [PK_Estado]
    PRIMARY KEY CLUSTERED ([IdEstado] ASC);
GO

-- Creating primary key on [IdSegmento] in table 'Segmento'
ALTER TABLE [dbo].[Segmento]
ADD CONSTRAINT [PK_Segmento]
    PRIMARY KEY CLUSTERED ([IdSegmento] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdPublicacao] in table 'Comentario'
ALTER TABLE [dbo].[Comentario]
ADD CONSTRAINT [FK_Comentario_Publicacao]
    FOREIGN KEY ([IdPublicacao])
    REFERENCES [dbo].[Publicacao]
        ([IdPublicacao])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comentario_Publicacao'
CREATE INDEX [IX_FK_Comentario_Publicacao]
ON [dbo].[Comentario]
    ([IdPublicacao]);
GO

-- Creating foreign key on [IdResposta] in table 'Comentario'
ALTER TABLE [dbo].[Comentario]
ADD CONSTRAINT [FK_Comentario_Resposta]
    FOREIGN KEY ([IdResposta])
    REFERENCES [dbo].[Comentario]
        ([IdComentario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Comentario_Resposta'
CREATE INDEX [IX_FK_Comentario_Resposta]
ON [dbo].[Comentario]
    ([IdResposta]);
GO

-- Creating foreign key on [IdComentario] in table 'Denuncia'
ALTER TABLE [dbo].[Denuncia]
ADD CONSTRAINT [FK_Denuncia_Comentario]
    FOREIGN KEY ([IdComentario])
    REFERENCES [dbo].[Comentario]
        ([IdComentario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Denuncia_Comentario'
CREATE INDEX [IX_FK_Denuncia_Comentario]
ON [dbo].[Denuncia]
    ([IdComentario]);
GO

-- Creating foreign key on [IdPublicacao] in table 'Denuncia'
ALTER TABLE [dbo].[Denuncia]
ADD CONSTRAINT [FK_Denuncia_Publicacao]
    FOREIGN KEY ([IdPublicacao])
    REFERENCES [dbo].[Publicacao]
        ([IdPublicacao])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Denuncia_Publicacao'
CREATE INDEX [IX_FK_Denuncia_Publicacao]
ON [dbo].[Denuncia]
    ([IdPublicacao]);
GO

-- Creating foreign key on [IdConsumidor] in table 'Acompanhamento'
ALTER TABLE [dbo].[Acompanhamento]
ADD CONSTRAINT [FK_Acompanhamento_Consumidor]
    FOREIGN KEY ([IdConsumidor])
    REFERENCES [dbo].[Consumidor]
        ([IdConsumidor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acompanhamento_Consumidor'
CREATE INDEX [IX_FK_Acompanhamento_Consumidor]
ON [dbo].[Acompanhamento]
    ([IdConsumidor]);
GO

-- Creating foreign key on [IdEmpresa] in table 'Acompanhamento'
ALTER TABLE [dbo].[Acompanhamento]
ADD CONSTRAINT [FK_Acompanhamento_Empresa]
    FOREIGN KEY ([IdEmpresa])
    REFERENCES [dbo].[Empresa]
        ([IdEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Acompanhamento_Empresa'
CREATE INDEX [IX_FK_Acompanhamento_Empresa]
ON [dbo].[Acompanhamento]
    ([IdEmpresa]);
GO

-- Creating foreign key on [IdEstado] in table 'Cidade'
ALTER TABLE [dbo].[Cidade]
ADD CONSTRAINT [FK_Cidade_Estado]
    FOREIGN KEY ([IdEstado])
    REFERENCES [dbo].[Estado]
        ([IdEstado])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cidade_Estado'
CREATE INDEX [IX_FK_Cidade_Estado]
ON [dbo].[Cidade]
    ([IdEstado]);
GO

-- Creating foreign key on [IdCidade] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Cidade]
    FOREIGN KEY ([IdCidade])
    REFERENCES [dbo].[Cidade]
        ([IdCidade])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Cidade'
CREATE INDEX [IX_FK_Usuario_Cidade]
ON [dbo].[Usuario]
    ([IdCidade]);
GO

-- Creating foreign key on [IdConsumidor] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Consumidor]
    FOREIGN KEY ([IdConsumidor])
    REFERENCES [dbo].[Consumidor]
        ([IdConsumidor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Consumidor'
CREATE INDEX [IX_FK_Usuario_Consumidor]
ON [dbo].[Usuario]
    ([IdConsumidor]);
GO

-- Creating foreign key on [IdSegmento] in table 'Empresa'
ALTER TABLE [dbo].[Empresa]
ADD CONSTRAINT [FK_Empresa_Segmento]
    FOREIGN KEY ([IdSegmento])
    REFERENCES [dbo].[Segmento]
        ([IdSegmento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Empresa_Segmento'
CREATE INDEX [IX_FK_Empresa_Segmento]
ON [dbo].[Empresa]
    ([IdSegmento]);
GO

-- Creating foreign key on [IdEmpresa] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Empresa]
    FOREIGN KEY ([IdEmpresa])
    REFERENCES [dbo].[Empresa]
        ([IdEmpresa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Empresa'
CREATE INDEX [IX_FK_Usuario_Empresa]
ON [dbo].[Usuario]
    ([IdEmpresa]);
GO

-- Creating foreign key on [IdUsuario] in table 'Publicacao'
ALTER TABLE [dbo].[Publicacao]
ADD CONSTRAINT [FK_Publicacao_Usuario]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuario]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Publicacao_Usuario'
CREATE INDEX [IX_FK_Publicacao_Usuario]
ON [dbo].[Publicacao]
    ([IdUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------