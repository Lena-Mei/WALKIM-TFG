USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetAllMascota]    Script Date: 26/05/2024 2:37:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllMascota](@idUsuario INT = NULL, @idAnimal INT = NULL)
AS
BEGIN
	IF @idUsuario IS NOT NULL
	BEGIN
		SELECT * FROM MASCOTA WHERE idUsuario = @idUsuario
	END
	ELSE IF @idAnimal IS NOT NULL
	BEGIN
		SELECT * FROM MASCOTA WHERE idTipoAnimal = @idAnimal
	END
	ELSE
	BEGIN
		SELECT * FROM MASCOTA 
	END
END
