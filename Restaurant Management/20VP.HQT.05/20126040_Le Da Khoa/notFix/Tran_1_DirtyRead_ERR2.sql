﻿--DIRTY READ 
--T1
USE [QL_DH_GH_ONLINE]
GO

CREATE OR ALTER PROCEDURE DT_CAPNHAT_GIASP @MASP CHAR(5), @GIA MONEY
AS
BEGIN TRAN
	DECLARE @GIASP MONEY
	SET @GIASP = (SELECT DONGIA FROM SANPHAM WHERE MASP = @MASP)
	UPDATE SANPHAM
		SET DONGIA = @GIA 
		WHERE MASP = @MASP
	WAITFOR DELAY '00:00:10'

	IF (@GIA >= 5 * @GIASP)
	BEGIN
		ROLLBACK TRAN
		RETURN
	END
COMMIT TRAN
GO
EXEC DT_CAPNHAT_GIASP 'SP015', 200000