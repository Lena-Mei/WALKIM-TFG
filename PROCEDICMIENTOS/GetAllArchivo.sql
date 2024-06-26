USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetAllArchivo]    Script Date: 26/05/2024 2:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllArchivo] (@idServidor INT = NULL, @resultado INT OUTPUT)
AS
BEGIN
	
	SET @resultado = 1

	IF @idServidor IS NULL
	BEGIN
		SELECT * FROM ARCHIVO
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor=@idServidor) 
		BEGIN
			SET @resultado=-1
		END
		ELSE
		BEGIN
			SELECT * FROM ARCHIVO WHERE idServidor = @idServidor
		END
	END
END
