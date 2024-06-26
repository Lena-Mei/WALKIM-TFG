USE [WALKIM]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ActAdmin] (@nombre VARCHAR(50), 
							@idAdmin INT,
							@apellido1 VARCHAR(100),
							@apellido2 VARCHAR (100),
							@telefono VARCHAR(9),
							@resultado INT OUTPUT )
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM ADMINISTRADOR WHERE idAdministrador = @idAdmin)
	BEGIN
		SET @resultado = -1;
	END
	ELSE 
	BEGIN
		UPDATE ADMINISTRADOR
		SET nombre = @nombre,
			apellido1 = @apellido1,
			apellido2 = @apellido2,
			telefono = @telefono
		WHERE idAdministrador = @idAdmin

		SET @resultado = 1;
			
	END
END
