USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetAllContrato]    Script Date: 26/05/2024 2:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[GetAllContrato](@idEstado INT = NULL, 
										 @idServicio INT = NULL, 
										 @idUsuario INT = NULL,
										 @resultado INT OUTPUT)
AS
BEGIN
			SET @resultado = 1

	IF @idEstado IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM CONTRATA WHERE idEstado = @idEstado)
		BEGIN
			SET @resultado = -1
		END
		ELSE
		BEGIN
			SELECT * FROM CONTRATA WHERE idEstado = @idEstado
		END
	END
	IF @idServicio IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM CONTRATA WHERE idServicio = @idServicio)
		BEGIN
			SET @resultado = -1
		END
		ELSE
		BEGIN
			SELECT * FROM CONTRATA WHERE idServicio = @idServicio
		END
	END
	IF @idUsuario IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM CONTRATA WHERE idUsuario = @idUsuario)
		BEGIN
				SET @resultado = -1
		END
		ELSE
		BEGIN
			SELECT * FROM CONTRATA WHERE idUsuario = @idUsuario
		END
	END
	ELSE
	BEGIN
		SELECT * FROM CONTRATA
	END
END
