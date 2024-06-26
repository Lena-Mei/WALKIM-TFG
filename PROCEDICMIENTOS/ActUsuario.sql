USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[ActUsuario]    Script Date: 26/05/2024 2:36:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ActUsuario] (@idUsuario INT,
							 @nombre VARCHAR(50), 
							 @apellido1 VARCHAR(100),
							 @apellido2 VARCHAR (100) = NULL,
							 @telefono VARCHAR(9) = NULL,
							 @direccion VARCHAR(200) = NULL,
							 @provincia VARCHAR(50) = NULL,
							 @ciudad VARCHAR(50) = NULL,
							 @codigoPostal VARCHAR(5) = NULL,
							 @descripcion VARCHAR(300) = NULL,
							 @resultado INT OUTPUT)
AS
BEGIN
	IF @idUsuario%2 = 0 --Si el id es par : usuario
	BEGIN
		IF NOT EXISTS (SELECT * FROM USUARIO WHERE idUsuario = @idUsuario) 
		BEGIN
			SET @resultado = -1
		END	
		ELSE
		BEGIN
			UPDATE USUARIO
			SET nombre = @nombre,
				apellido1 =@apellido1,
				apellido2 = @apellido2,
				telefono = @telefono,
				direccion =@direccion,
				provincia = @provincia,
				ciudad=@ciudad,
				codigoPostal = @codigoPostal,
				descripcion = @descripcion
				

			SET @resultado = 1
		END
	END
	ELSE --Es Servidor
	BEGIN
		IF NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor = @idUsuario) 
		BEGIN
			SET @resultado = -1
		END	
		ELSE
		BEGIN
			UPDATE SERVIDOR
			SET nombre = @nombre,
				apellido1 =@apellido1,
				apellido2 = @apellido2,
				telefono = @telefono,
				direccion =@direccion,
				provincia = @provincia,
				ciudad=@ciudad,
				codigoPostal = @codigoPostal,
				descripcion = @descripcion

			SET @resultado = 1
		END
	END
END


