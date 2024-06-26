USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetAllServicio]    Script Date: 26/05/2024 2:37:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllServicio] (@idTipoServ  INT = NULL, 
										@idServidor INT = NULL,
										@resultado INT OUTPUT)
AS
BEGIN
		SET @resultado = 1
	IF @idTipoServ IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM SERVICIO WHERE idTipoServicio = @idTipoServ)
		BEGIN
			SET @resultado = -1
		END
		ELSE
		BEGIN
			SELECT * FROM SERVICIO WHERE idTipoServicio = @idTipoServ
		END
	END
	ELSE IF @idServidor IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor= @idServidor)
		BEGIN
					SET @resultado = -1
		END
		ELSE
		BEGIN
			SELECT * FROM SERVICIO WHERE idServidor = @idServidor
		END
	END
	ELSE
	BEGIN
		SELECT * FROM SERVICIO
	END

END
