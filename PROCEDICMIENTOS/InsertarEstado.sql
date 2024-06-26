USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InsertarEstado]    Script Date: 26/05/2024 2:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertarEstado] (@nombre VARCHAR(20), @descripcion VARCHAR(50), @resultado INT OUTPUT)

AS
BEGIN
	IF @nombre IS NULL OR @descripcion IS NULL
	BEGIN
		SET @resultado = -1;
	END
	ELSE
	BEGIN
		INSERT INTO ESTADO VALUES (@nombre, @descripcion)
		SET @resultado = 1;
	END
END
