USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetAllTicket]    Script Date: 26/05/2024 2:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetAllTicket](@idUsuario INT = NULL, @idServidor INT = NULL, @tipoCuenta VARCHAR(10) = NULL)
AS
BEGIN
	IF @idUsuario IS NOT NULL
	BEGIN
		SELECT * FROM TICKET WHERE idUsuario = @idUsuario
	END

	ELSE IF @idServidor IS NOT NULL
	BEGIN
		SELECT * FROM TICKET WHERE idServidor = @idServidor
	END
	ELSE IF @tipoCuenta IS NOT NULL
	BEGIN
		SELECT * FROM TICKET WHERE tipoCuenta = @tipoCuenta
	END
	ELSE
	BEGIN
		SELECT * FROM TICKET 
	END

END
