DROP PROCEDURE [login];
GO
CREATE PROCEDURE [login]
@username varchar(20),
@password varchar(20),
@message nvarchar(20) output
AS
DECLARE @isOnline bit
IF EXISTS (SELECT * FROM tb_user WHERE (username = @username AND password = @password)) BEGIN
	SELECT @isOnline = isOnline FROM tb_user WHERE (username = @username AND password = @password)
	IF(1 = @isOnline) BEGIN
		SET @message = '���û��ѵ�¼��'
		RETURN -1;
	END
	--UPDATE tb_user SET isOnline = 1 WHERE (username = @username AND password = @password)
	UPDATE tb_user SET lastLoginTime = (select getdate()) WHERE (username = @username AND password = @password)
	SELECT TOP 1 * FROM tb_user WHERE (username = @username AND password = @password)
		SET @message = '��¼�ɹ���'
	RETURN 0
END
ELSE BEGIN
	SET @message = '�û������������'
	RETURN -1;
END