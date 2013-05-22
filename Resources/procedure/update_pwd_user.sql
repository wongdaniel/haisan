DROP PROCEDURE update_pwd
GO
CREATE PROCEDURE update_pwd
@id int,
@password varchar(20),
@message nvarchar(20) output
AS
IF NOT EXISTS(SELECT * FROM tb_user WHERE id = @id) BEGIN
	SET @message = '该用户已不存在于系统'
	return -1;
END 
ELSE BEGIN
	UPDATE tb_user SET [password] = @password WHERE id = @id
	return 0
END