USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[ActEstadoContrato]    Script Date: 26/05/2024 2:36:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ActEstadoContrato] (@idEstado INT, @idContrato INT, @resultado INT OUTPUT)
AS
BEGIN
SET @resultado =1
	IF NOT EXISTS (SELECT * FROM ESTADO WHERE idEstado = @idEstado) OR
	NOT EXISTS (SELECT * FROM CONTRATA WHERE idContrato =@idContrato)
	BEGIN
		SET @resultado =-1
	END
	ELSE
	BEGIN
		UPDATE CONTRATA 
		SET idEstado=@idEstado 
		WHERE idContrato =@idContrato
	END
END
