﻿USE [QL_DH_GH_ONLINE]
GO
/*UNREPEATABLE READ*/
/*T1*/
CREATE OR ALTER PROCEDURE KTRA_TTGH_FIX @MAKH CHAR(5), @MADH CHAR(5)
AS
BEGIN TRAN
	SET TRAN ISOLATION LEVEL REPEATABLE READ
	IF NOT EXISTS(SELECT DGH.* FROM DONGIAOHANG DGH, DONHANG DH WHERE DGH.MADH = DH.MADH AND DH.MAKH = @MAKH AND DGH.MADH = @MADH)
	BEGIN
		PRINT N'ĐH không tồn tại'
		RETURN
	END

	DECLARE @TTGH NVARCHAR(40)
	SET @TTGH = 0
	SET @TTGH = (SELECT TTGIAOHANG FROM DONGIAOHANG WHERE MADH = @MADH)
	WAITFOR DELAY '00:00:05'

	IF ((SELECT TTGIAOHANG FROM DONGIAOHANG WHERE MADH = @MADH) <> @TTGH)
	BEGIN
		RAISERROR(N'2 lần đọc dữ liệu khác nhau',16,1)
	END
	SELECT *
	FROM DONGIAOHANG
	WHERE MADH = @MADH
COMMIT TRAN
GO
EXEC KTRA_TTGH_FIX 'KH215','DH004'
