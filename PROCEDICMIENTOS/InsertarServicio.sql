USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InsertarServicio]    Script Date: 26/05/2024 2:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[InsertarServicio] (@resultado INT OUTPUT,
								   @idTipoServicio INT,
								   @idServidor INT,
								   @precio DECIMAL (8,2),
								   @descripcion VARCHAR(300),
								   @puntaje INT = 0,
								   @nombre VARCHAR (50))

AS
BEGIN
	      SET @resultado = 1 
		  IF NOT EXISTS (SELECT * FROM TIPOSERVICIO WHERE idTipoServicio = @idTipoServicio) OR  
		  NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor = @idServidor) 
		  BEGIN 
			SET @resultado =-1 
		  END 
		  ELSE IF EXISTS (SELECT * FROM SERVICIO WHERE nombre = @nombre)
		  BEGIN
			SET @resultado =-2
		  END
		  ELSE 
		  BEGIN  
				INSERT SERVICIO (idServidor, precio, nombre, descripcion, puntaje, idTipoServicio) 
				VALUES (@idServidor, @precio, @nombre, @descripcion, @puntaje, @idTipoServicio) 
				SET @resultado = SCOPE_IDENTITY();
		  END

END
