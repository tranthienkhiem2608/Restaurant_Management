﻿USE [QL_DH_GH_ONLINE]
GO
/*PHANTOM TH1*/
/* T1 */
CREATE OR ALTER PROCEDURE XEM_DS_LOGIN
AS
BEGIN TRAN
	DECLARE @COUNT INT
	SET @COUNT = 0
	SET @COUNT = (SELECT COUNT(*) FROM LOGIN)
	WAITFOR DELAY '00:00:10'
	
	IF ((SELECT COUNT(*) FROM LOGIN) <> @COUNT)
	BEGIN
		RAISERROR(N'Dữ liệu hai lần đọc khác nhau',16,1)
	END
	SELECT * FROM LOGIN
COMMIT TRAN
GO
EXEC XEM_DS_LOGIN