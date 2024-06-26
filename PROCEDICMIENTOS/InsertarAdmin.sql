USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InsertarAdmin]    Script Date: 26/05/2024 2:38:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertarAdmin] (@nombre VARCHAR(50), 
								@apellido1 VARCHAR(100),
								@apellido2 VARCHAR (100),
								@telefono VARCHAR(9),
								@correo VARCHAR(200), 
								@contrasenya VARCHAR(50),
								@resultado INT OUTPUT )

AS
BEGIN
	IF EXISTS (SELECT * FROM ADMINISTRADOR WHERE correo = @correo)
	BEGIN
		SET @resultado = -1
	END
	ELSE IF @nombre IS NULL OR @apellido1 IS NULL OR @correo IS NULL OR @contrasenya IS NULL OR @apellido2 IS NULL OR @telefono IS NULL
	BEGIN
		SET @resultado = -2
	END
	ELSE
	BEGIN
		INSERT INTO ADMINISTRADOR(nombre, apellido1, apellido2, telefono, correo, contrasenya) 
		VALUES (@nombre, @apellido1, @apellido2, @telefono, @correo, @contrasenya)
		SET @resultado = 1
	END
END
