﻿USE [QL_DH_GH_ONLINE]
GO
/*UNREPEATABLE READ*/
/*T2*/
CREATE OR ALTER PROCEDURE CAPNHAT_MACN_NV @MANV CHAR(5), @MACN CHAR(5)
AS
BEGIN TRAN
	UPDATE NHANVIEN
	SET MACN = @MACN
	WHERE MANV = @MANV
COMMIT TRAN
GO
EXEC CAPNHAT_MACN_NV 'NV313', 'CN301'