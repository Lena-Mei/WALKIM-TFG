USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetUsuario]    Script Date: 26/05/2024 2:38:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetUsuario] (@idUsuario INT, @resultado INT OUTPUT)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM USUARIO WHERE idUsuario =@idUsuario)
	BEGIN
		SET @resultado = -1
	END
	ELSE
	BEGIN
		SELECT * FROM USUARIO WHERE idUsuario = @idUsuario

		SELECT * FROM MASCOTA WHERE idUsuario = @idUsuario

		SET @resultado = 1
	END
END
