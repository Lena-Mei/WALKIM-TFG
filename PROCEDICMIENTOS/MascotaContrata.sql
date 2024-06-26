USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[MascotaContrata]    Script Date: 26/05/2024 2:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[MascotaContrata] (@idMascota INT, @idContrato INT, @resutlado INT = 1 OUTPUT)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM MASCOTA WHERE idMascota = @idMascota) OR
	NOT EXISTS (SELECT * FROM CONTRATA WHERE idContrato = @idContrato)
	BEGIN
		SET @resutlado = -1
	END
	ELSE
	BEGIN
		INSERT CONTRATA_MASCOTA (idMascota, idContrata) VALUES (@idMascota, @idContrato)
	END
END
