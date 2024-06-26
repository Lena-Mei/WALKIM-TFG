USE [WALKIM]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ActEstado] (@idEstado INT, @nombre VARCHAR(20), @descripcion VARCHAR(50), @resultado INT OUTPUT)
AS
BEGIN
	
	IF EXISTS (SELECT nombre FROM ESTADO WHERE nombre=@nombre) 
	BEGIN
		SET @resultado = -1
	END
	ELSE IF @nombre IS NULL OR @descripcion IS NULL OR @idEstado IS NULL OR @descripcion IS NULL 
	BEGIN
		SET @resultado = -2
	END
	ELSE
	BEGIN
		UPDATE ESTADO
		SET nombre = @nombre,
			descripcion = @descripcion
		WHERE idEstado = @idEstado

		SET @resultado = 1
	END
END
