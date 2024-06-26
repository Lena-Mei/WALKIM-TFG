USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[CrearContrato]    Script Date: 26/05/2024 2:37:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[CrearContrato] (@idUsuario INT,
								@idServicio INT,
								@tiempo VARCHAR(50),
								@fecha DATETIME,
								@resultado INT OUTPUT)
								


AS
BEGIN
	SET @resultado = 1 
	IF NOT EXISTS (SELECT * FROM USUARIO WHERE idUsuario=@idUsuario) OR
	NOT EXISTS (SELECT * FROM SERVICIO WHERE idServicio=@idServicio) 
	BEGIN
		SET @resultado =-1
	END
	ELSE
	BEGIN
		            INSERT CONTRATA (idServicio, idUsuario, tiempo, fecha, idEstado) 
					VALUES (@idServicio, @idUsuario, @tiempo, @fecha, 1)
					SET @resultado = SCOPE_IDENTITY();
	END
END
