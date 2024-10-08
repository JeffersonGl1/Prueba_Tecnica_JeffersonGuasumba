USE [master]
GO
/****** Object:  Database [Prueba_Tecnica]    Script Date: 01/10/2024 9:31:56 ******/
CREATE DATABASE [Prueba_Tecnica]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Prueba_Tecnica', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Prueba_Tecnica.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Prueba_Tecnica_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Prueba_Tecnica_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Prueba_Tecnica] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Prueba_Tecnica].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Prueba_Tecnica] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET ARITHABORT OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Prueba_Tecnica] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Prueba_Tecnica] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Prueba_Tecnica] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Prueba_Tecnica] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Prueba_Tecnica] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET RECOVERY FULL 
GO
ALTER DATABASE [Prueba_Tecnica] SET  MULTI_USER 
GO
ALTER DATABASE [Prueba_Tecnica] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Prueba_Tecnica] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Prueba_Tecnica] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Prueba_Tecnica] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Prueba_Tecnica] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Prueba_Tecnica', N'ON'
GO
ALTER DATABASE [Prueba_Tecnica] SET QUERY_STORE = OFF
GO
USE [Prueba_Tecnica]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Prueba_Tecnica]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 01/10/2024 9:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Contrasenia] [nvarchar](max) NOT NULL,
	[Estado] [bit] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Genero] [nvarchar](max) NOT NULL,
	[Edad] [int] NOT NULL,
	[Identificacion] [nvarchar](max) NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 01/10/2024 9:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[IdCuenta] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[NumeroCuenta] [bigint] NOT NULL,
	[TipoCuenta] [int] NOT NULL,
	[SaldoInicial] [float] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[IdCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 01/10/2024 9:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[IdMovimiento] [int] IDENTITY(1,1) NOT NULL,
	[FechaMovimiento] [datetime2](7) NOT NULL,
	[DescMovimiento] [nvarchar](max) NOT NULL,
	[TipoMovimiento] [int] NOT NULL,
	[ValorMovimiento] [float] NOT NULL,
	[Saldo] [float] NOT NULL,
	[NumeroCuenta] [bigint] NOT NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[IdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_ReporteFechasUsuario]    Script Date: 01/10/2024 9:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Jefferson Guasumba>
-- Create date: <29/09/2024>
-- Description:	<Reporte de estado de cuenta>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ReporteFechasUsuario]

@Identificacion varchar(50),
@FechaInicial DateTime,
@FechaFinal DateTime


AS
BEGIN
	
	select cl.IdCliente,mov.FechaMovimiento, cl.Nombre, cu.NumeroCuenta, cu.TipoCuenta, cl.Estado,mov.TipoMovimiento, mov.DescMovimiento ,mov.ValorMovimiento, mov.Saldo from dbo.Clientes cl 
	with(nolock) Join dbo.Cuentas cu on cl.IdCliente = cu.IdCliente Join dbo.Movimientos mov on cu.NumeroCuenta = mov.NumeroCuenta   where 
	mov.FechaMovimiento between @FechaInicial and @FechaFinal and cl.Identificacion = @Identificacion order by mov.FechaMovimiento asc


END

GO
USE [master]
GO
ALTER DATABASE [Prueba_Tecnica] SET  READ_WRITE 
GO
