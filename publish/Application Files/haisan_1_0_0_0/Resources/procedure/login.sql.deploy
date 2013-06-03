DROP PROCEDURE [login];
GO
CREATE PROCEDURE [login]
@username varchar(20),
@password varchar(20),
@message nvarchar(20) output
AS
DECLARE @isOnline bit
DECLARE @isLock bit
IF EXISTS (SELECT * FROM tb_user WHERE (username = @username AND password = @password)) BEGIN
	SELECT @isOnline = isOnline, @isLock = isLock FROM tb_user WHERE (username = @username AND password = @password)
	IF(1 = @isOnline) BEGIN
		SET @message = '该用户已登录！'
		RETURN -1;
	END

	IF(1 = @isLock) BEGIN
		SET @message = '该用户已被锁定！'
		RETURN -1;
	END
	--UPDATE tb_user SET isOnline = 1 WHERE (username = @username AND password = @password)
	UPDATE tb_user SET lastLoginTime = (select getdate()) WHERE (username = @username AND password = @password)
	SELECT tu.id, tu.username, tu.password, tu.email, tu.phone, tu.[group], tg.name AS group_name
		 FROM tb_user tu, tb_group tg WHERE 
			(tu.username = @username AND tu.[password] = @password AND tu.[group] = tg.id)
		SET @message = '登录成功！'
	RETURN 0
END
ELSE BEGIN
	SET @message = '用户名或密码错误！'
	RETURN -1;
END