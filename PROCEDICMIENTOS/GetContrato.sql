USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetContrato]    Script Date: 26/05/2024 2:38:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetContrato] (@idContrato INT, @resultado INT OUTPUT)
AS
BEGIN
	SET @resultado = 1
	IF NOT EXISTS (SELECT * FROM CONTRATA WHERE idContrato=@idContrato)
	BEGIN
		SET @resultado = -1
	END
	ELSE
	BEGIN
		SELECT * FROM CONTRATA WHERE idContrato=@idContrato
		SELECT * FROM CONTRATA_MASCOTA WHERE idContrata=@idContrato
	END
END
