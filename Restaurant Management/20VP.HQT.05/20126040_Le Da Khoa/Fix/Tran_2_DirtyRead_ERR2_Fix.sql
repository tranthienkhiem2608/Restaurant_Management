--DIRTY READ 
--T2 
USE [QL_DH_GH_ONLINE]
GO

CREATE OR ALTER PROCEDURE KH_XEMGIA_SP_FIX @MASP CHAR(5)
AS
BEGIN TRAN
	 SET TRAN ISOLATION LEVEL READ COMMITTED
     SELECT *
     FROM SANPHAM 
	 WHERE MASP = @MASP
COMMIT TRAN
GO
EXEC KH_XEMGIA_SP_FIX 'SP015'