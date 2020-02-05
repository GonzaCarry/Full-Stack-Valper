CREATE database Valper
USE [Valper]
GO
/****** Object:  Table [dbo].[Funciones]    Script Date: 03/02/2020 17:46:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funciones](
	[Id] [nchar](36) NOT NULL,
	[Servidor] [nvarchar](100) NOT NULL,
	[Accion] [nvarchar](15) NOT NULL
	CHECK( Accion IN('Función', 'Procedimiento') ),
	[Descripcion] [nvarchar](200) NULL,
 CONSTRAINT [PK_Funciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 03/02/2020 17:46:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [nchar](36) NOT NULL,
	[NombreCompleto] [nvarchar](100) NULL,
	[Imagen] [varbinary](max) NULL,
	[Permisos] [nvarchar](30) NOT NULL
	CHECK( Permisos IN('Usuario', 'Admin', 'Superadmin') ),
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios_Funciones]    Script Date: 03/02/2020 17:46:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios_Funciones](
	[IdUsuario] [nchar](36) NOT NULL,
	[idFuncion] [nchar](36) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[idFuncion] ASC,
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuarios_Funciones]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Funciones] FOREIGN KEY([idFuncion])
REFERENCES [dbo].[Funciones] ([Id])
GO
ALTER TABLE [dbo].[Usuarios_Funciones] CHECK CONSTRAINT [FK_Table_1_Funciones]
GO
ALTER TABLE [dbo].[Usuarios_Funciones]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Table_1] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Usuarios_Funciones] CHECK CONSTRAINT [FK_Table_1_Table_1]
GO
