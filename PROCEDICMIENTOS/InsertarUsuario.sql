USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InsertarUsuario]    Script Date: 26/05/2024 2:38:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertarUsuario] (@nombre VARCHAR(50), 
								  @apellido1 VARCHAR(100),
								  @apellido2 VARCHAR (100),
								  @telefono VARCHAR(9),
								  @correo VARCHAR(200), 
								  @contrasenya VARCHAR(50),
								  @resultado INT OUTPUT)
AS
BEGIN
	IF EXISTS (SELECT * FROM USUARIO WHERE correo = @correo)
	BEGIN
		SET @resultado = -1
	END
	ELSE IF @nombre IS NULL OR @apellido1 IS NULL OR @correo IS NULL OR @contrasenya IS NULL OR @apellido2 IS NULL OR @telefono IS NULL
	BEGIN
		SET @resultado = -2
	END
	ELSE
	BEGIN
		INSERT INTO USUARIO (nombre, apellido1, apellido2, telefono, correo, contrasenya, imgPerfil) 
		VALUES (@nombre, @apellido1, @apellido2, @telefono, @correo, @contrasenya, 'sinFoto.jpg')
		SET @resultado = 1
	END
END
