﻿USE [QL_DH_GH_ONLINE]
GO
/*PHANTOM - TH2*/
/* T2 */
CREATE OR ALTER PROCEDURE THEM_HD @MADT CHAR(5), @MACN CHAR(5), @NGAYKY DATE, @NGAYKT DATE
AS
BEGIN TRAN
	INSERT INTO HOPDONG(MADT,MACN,NGAYKY,NGAYKT, PHANTRAMHOAHONG, TINHTRANGHD)
	VALUES (@MADT,@MACN,@NGAYKY,@NGAYKT, 10,  N'Đang chờ duyệt')

	IF (@NGAYKY > @NGAYKT)
	BEGIN
		PRINT N'NGÀY KÝ VÀ NGÀY KT KHÔNG HỢP LỆ'
		ROLLBACK TRAN
	END
COMMIT TRAN
GO
EXEC THEM_HD 'DT195', 'CN184', '2023-03-16', '2024-03-16'