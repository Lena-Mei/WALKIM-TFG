USE [WALKIM]
GO
/****** Object:  StoredProcedure [dbo].[GetServicio]    Script Date: 26/05/2024 2:38:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetServicio] (@resultado INT OUTPUT, @idServicio INT)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM SERVICIO WHERE idServicio=@idServicio) 
	BEGIN  
		SET @resultado = -1  
	END  
	ELSE 
	BEGIN 
		SELECT * FROM SERVICIO WHERE idServicio=@idServicio 
		SELECT * FROM SERVICIO_TIPOANIMAL WHERE idServicio = @idServicio
		SELECT * FROM CONTRATA WHERE idServicio = @idServicio

				SET @resultado = 1  

		
	END
END
