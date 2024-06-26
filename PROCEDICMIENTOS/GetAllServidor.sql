USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetAllServidor]    Script Date: 26/05/2024 2:37:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetAllServidor] (@idEstado INT = NULL, @resultado INT OUTPUT)
AS
BEGIN
	SET @resultado = 1

	IF @idEstado IS NULL
	BEGIN
		SELECT * FROM SERVIDOR
	END
	ELSE
	BEGIN
		IF NOT EXISTS (SELECT * FROM ESTADO WHERE idEstado = @idEstado)
		BEGIN
			SET @resultado = -1
		END
		SELECT * FROM SERVIDOR WHERE idEstado=@idEstado
	END
END
