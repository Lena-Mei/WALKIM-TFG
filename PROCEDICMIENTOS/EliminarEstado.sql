USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[EliminarEstado]    Script Date: 26/05/2024 2:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EliminarEstado] (@idEstado INT, @resultado INT OUTPUT)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM ESTADO WHERE idEstado=@idEstado)
	BEGIN
		SET @resultado = -1
	END
	ELSE
	BEGIN
		DELETE FROM ESTADO 
		WHERE idEstado=@idEstado
		
		SET @resultado = 1
	END
END
