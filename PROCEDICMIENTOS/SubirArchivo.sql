USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[SubirArchivo]    Script Date: 26/05/2024 2:38:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SubirArchivo] (@nomArchivo VARCHAR(500), 
			      @archivo VARBINARY (MAX), 
				  @ext VARCHAR(50),
				  @fecha DATETIME,
				  @idServidor INT)
AS
BEGIN
	INSERT ARCHIVO (nombreArchivo, archivo, extensionArchivo, idServidor, fechaEntrada) 
	VALUES (@nomArchivo, @archivo, @ext, @idServidor,  @fecha)
END
