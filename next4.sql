USE [master]
GO
/****** Object:  Database [next4]    Script Date: 21/02/2022 09:25:56 ******/
CREATE DATABASE [next4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'next4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS2019\MSSQL\DATA\next4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'next4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS2019\MSSQL\DATA\next4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [next4] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [next4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [next4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [next4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [next4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [next4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [next4] SET ARITHABORT OFF 
GO
ALTER DATABASE [next4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [next4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [next4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [next4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [next4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [next4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [next4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [next4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [next4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [next4] SET  DISABLE_BROKER 
GO
ALTER DATABASE [next4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [next4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [next4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [next4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [next4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [next4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [next4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [next4] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [next4] SET  MULTI_USER 
GO
ALTER DATABASE [next4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [next4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [next4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [next4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [next4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [next4] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [next4] SET QUERY_STORE = OFF
GO
USE [next4]
GO
/****** Object:  Table [dbo].[snna_hash_security_api]    Script Date: 21/02/2022 09:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[snna_hash_security_api](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[hash_security] [varchar](250) NOT NULL,
	[status] [tinyint] NOT NULL,
	[restriction] [varchar](45) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[snna_lead_crm]    Script Date: 21/02/2022 09:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[snna_lead_crm](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[lead_form_id] [bigint] NOT NULL,
	[gerente_comercial] [varchar](70) NULL,
	[accountid_crm] [varchar](200) NULL,
	[status_lead] [varchar](40) NULL,
	[enc_lead] [varchar](40) NULL,
	[filial_atendimento] [varchar](40) NULL,
	[empresa_crm] [varchar](70) NULL,
	[chave_crm] [varchar](100) NULL,
	[vertical] [varchar](70) NULL,
	[dt_rec_lead_gc] [datetime] NULL,
	[feedback_atend] [text] NULL,
	[funcionarios] [int] NULL,
	[filiais] [int] NULL,
	[faturamento] [varchar](70) NULL,
	[status_conta_crm] [varchar](40) NULL,
	[razao_status] [varchar](40) NULL,
	[modalidade_tucunare] [varchar](70) NULL,
	[probabilidade_fechamento] [varchar](20) NULL,
	[dt_estimativa_fechamento] [datetime] NULL,
	[receita_mensal_estimada] [varchar](40) NULL,
	[receita_tucunare] [varchar](40) NULL,
	[prazo_contrato_estimado] [int] NULL,
	[total_contrato] [varchar](70) NULL,
	[pilar_opp] [varchar](70) NULL,
	[numero_projeto_tucunare] [int] NULL,
	[numero_proposta_tucunare] [int] NULL,
	[lista_tucunare] [int] NULL,
	[dt_fechamento_negocio] [datetime] NULL,
	[dt_criacao_opp] [datetime] NULL,
	[estagio_processo] [varchar](40) NULL,
	[descricao_projeto] [text] NULL,
	[gerente_conta] [varchar](70) NULL,
	[unidade_negocios] [varchar](20) NULL,
	[oportunidade] [varchar](70) NULL,
	[tipo_conta] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[lead_form_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[snna_lead_form]    Script Date: 21/02/2022 09:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[snna_lead_form](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](70) NULL,
	[sobrenome] [varchar](100) NULL,
	[empresa] [varchar](70) NULL,
	[cnpj] [varchar](30) NULL,
	[tel_contato] [varchar](20) NULL,
	[email] [varchar](70) NULL,
	[pilar_negocio] [varchar](100) NULL,
	[qtde_equipamentos] [int] NULL,
	[vol_impressao] [int] NULL,
	[mensagem] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[snna_lead_rd]    Script Date: 21/02/2022 09:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[snna_lead_rd](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[lead_form_id] [bigint] NOT NULL,
	[dt_entrada_lead] [datetime] NOT NULL,
	[event_type] [varchar](45) NULL,
	[event_family] [varchar](45) NULL,
	[conversion_identifier] [varchar](200) NULL,
	[client_tracking_id] [varchar](60) NULL,
	[traffic_source] [varchar](45) NULL,
	[traffic_medium] [varchar](15) NULL,
	[traffic_campaign] [varchar](60) NULL,
	[traffic_value] [varchar](45) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[lead_form_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[snna_users]    Script Date: 21/02/2022 09:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[snna_users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_view] [varchar](100) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[dt_created] [datetime] NOT NULL,
	[dt_update] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[snna_hash_security_api] ADD  DEFAULT ('RD') FOR [restriction]
GO
USE [master]
GO
ALTER DATABASE [next4] SET  READ_WRITE 
GO
