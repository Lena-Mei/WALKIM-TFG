USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[CambiarContrasenya]    Script Date: 26/05/2024 2:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[CambiarContrasenya] (@idUsuario INT, 
									 @contrasenya VARCHAR(50),
									 @resultado INT OUTPUT)
									 
AS
BEGIN
	IF @idUsuario%2 = 0 --Si el idUsuario es múltiplo de 2 es USUARIO, si no, es SERVIDOR
	BEGIN
		IF NOT EXISTS (SELECT * FROM USUARIO WHERE idUsuario = @idUsuario)
		BEGIN 
			SET @resultado = -1
		END
		ELSE IF EXISTS (SELECT * FROM USUARIO WHERE idUsuario = @idUsuario AND contrasenya = @contrasenya)
		BEGIN
			SET @resultado = -2
		END
		ELSE
		BEGIN
			UPDATE USUARIO
			SET contrasenya = @contrasenya
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
		ELSE IF EXISTS (SELECT * FROM SERVIDOR WHERE idServidor = @idUsuario AND contrasenya = @contrasenya)
		BEGIN
			SET @resultado = -2
		END
		ELSE
		BEGIN
			UPDATE SERVIDOR
			SET contrasenya = @contrasenya
			WHERE idServidor = @idUsuario

			SET @resultado = 1
		END
	END
END
