USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[LoginUsuario]    Script Date: 26/05/2024 2:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROCEDURE [dbo].[LoginUsuario] (@correo VARCHAR(200), @contrasenya VARCHAR(50), @resultado INT OUTPUT)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM USUARIO WHERE correo = @correo AND contrasenya=@contrasenya) AND
	   NOT EXISTS (SELECT * FROM SERVIDOR WHERE correo = @correo AND contrasenya=@contrasenya) AND
	   NOT EXISTS (SELECT * FROM ADMINISTRADOR WHERE correo = @correo AND contrasenya=@contrasenya)
	BEGIN
		SET @resultado = -1;
	END
	ELSE IF EXISTS (SELECT * FROM USUARIO WHERE correo = @correo AND contrasenya=@contrasenya)
	BEGIN
			SET @resultado = (SELECT idUsuario FROM USUARIO WHERE correo = @correo AND contrasenya=@contrasenya)
	END

		ELSE IF EXISTS (SELECT * FROM SERVIDOR WHERE correo = @correo AND contrasenya=@contrasenya)
		BEGIN
			SET @resultado = (SELECT idServidor FROM SERVIDOR WHERE correo = @correo AND contrasenya=@contrasenya)
		END
		ELSE IF EXISTS (SELECT * FROM ADMINISTRADOR WHERE correo = @correo AND contrasenya=@contrasenya)
		BEGIN
			SET @resultado = (SELECT idAdministrador FROM ADMINISTRADOR WHERE correo = @correo AND contrasenya=@contrasenya)
		END
END
