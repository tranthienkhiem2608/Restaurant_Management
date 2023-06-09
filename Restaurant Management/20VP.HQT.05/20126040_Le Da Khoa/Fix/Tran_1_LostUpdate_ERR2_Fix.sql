﻿USE [QL_DH_GH_ONLINE]
GO
CREATE OR ALTER PROCEDURE DT_CAPNHATSL_FIX @MASP CHAR(5), @SL INT
AS
BEGIN TRAN
	SET TRAN ISOLATION LEVEL REPEATABLE READ
	SELECT MASP, SLTON 
	FROM SANPHAM WITH(UPDLOCK)
	WHERE MASP = @MASP
	WAITFOR DELAY '00:00:05'

	UPDATE SANPHAM
	SET SLTON = @SL
	WHERE MASP = @MASP
COMMIT TRAN
GO
EXEC DT_CAPNHATSL_FIX 'SP001', 49