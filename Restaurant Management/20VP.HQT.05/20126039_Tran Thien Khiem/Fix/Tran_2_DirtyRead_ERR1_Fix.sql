USE [QL_DH_GH_ONLINE]
GO
CREATE OR ALTER PROCEDURE XEM_DS_MAHD_FIX @MAHD CHAR(5), @MADT CHAR(5)
AS
BEGIN TRAN
	SET TRAN ISOLATION LEVEL READ COMMITTED
	SELECT * FROM HOPDONG WHERE MAHD = @MAHD AND MADT = @MADT
COMMIT TRAN
GO
EXEC XEM_DS_MAHD_FIX 'HD469', 'DT541'
