USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InstMascotaContrato]    Script Date: 26/05/2024 2:38:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InstMascotaContrato] (@idMascota INT, @idContrato INT)
AS
BEGIN
	INSERT CONTRATA_MASCOTA (idContrata, idMascota) VALUES (@idContrato, @idMascota)
END
