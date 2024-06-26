USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarServicio]    Script Date: 26/05/2024 2:36:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ActualizarServicio] (@resultado INT OUTPUT,
								   @idTipoServicio INT,
								   @idServidor INT,
								   @precio DECIMAL (8,2),
								   @descripcion VARCHAR(300),
								   @puntaje INT = 0,
								   @nombre VARCHAR (50),
								   @idServicio INT)
AS
BEGIN
	SET @resultado = 1

	IF NOT EXISTS (SELECT * FROM SERVICIO WHERE idServicio = @idServicio)
	BEGIN
		SET @resultado =-1
	END
	ELSE IF NOT EXISTS (SELECT * FROM TIPOSERVICIO WHERE idTipoServicio = @idTipoServicio) OR  
	NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor = @idServidor) OR
	EXISTS (SELECT * FROM SERVICIO WHERE nombre=@nombre AND idServicio!=@idServicio)
	BEGIN
		SET @resultado = -2
	END
	ELSE
	BEGIN
		DELETE SERVICIO_TIPOANIMAL WHERE idServicio = @idServicio
		UPDATE SERVICIO 
		SET precio=@precio, 
			nombre=@nombre, 
			descripcion=@descripcion, 
			idTipoServicio=@idTipoServicio 
		WHERE idServicio=@idServicio
	END
END
