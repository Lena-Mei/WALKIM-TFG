USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[InsertTicket]    Script Date: 26/05/2024 2:38:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[InsertTicket] (@des VARCHAR(300),
							   @titulo VARCHAR(50),
							   @idUsuario INT = NULL,
							   @idServidor INT = NULL,
							   @tipoCuenta VARCHAR(10))

AS
BEGIN
               INSERT TICKET (descripcion, tituloProblema, fecha, idUsuario, idServidor, tipoCuenta) 
			   VALUES (@des, @titulo, GETDATE(), @idUsuario, @idServidor, @tipoCuenta)
END
