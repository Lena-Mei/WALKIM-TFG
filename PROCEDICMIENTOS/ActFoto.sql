USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[ActFoto]    Script Date: 26/05/2024 2:36:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ActFoto] (@idUsuario INT, @imagen VARCHAR(MAX), @resultado INT OUTPUT)
AS
BEGIN
IF @idUsuario%2 = 0 --Si el idUsuario es múltiplo de 2 es USUARIO, si no, es SERVIDOR
	BEGIN
		IF NOT EXISTS (SELECT * FROM USUARIO WHERE idUsuario = @idUsuario)
		BEGIN 
			SET @resultado = -1
		END
		ELSE
		BEGIN
			UPDATE USUARIO
			SET imgPerfil = @imagen
			WHERE idUsuario = @idUsuario

			SET @resultado = 1
		END

	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor = @idUsuario)
		BEGIN 
			SET @resultado = -1
		END
		ELSE
		BEGIN
			UPDATE SERVIDOR
			SET imgPerfil = @imagen
			WHERE idServidor = @idUsuario

			SET @resultado = 1
		END
	END
END
