USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InsTipoAnimalServicio]    Script Date: 26/05/2024 2:38:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsTipoAnimalServicio] (@idServicio INT, @idTipoAnimal INT, @resultado INT =1 OUTPUT)
AS
BEGIN

		INSERT SERVICIO_TIPOANIMAL(idServicio, idTipoAnimal) VALUES (@idServicio, @idTipoAnimal)
	
END
