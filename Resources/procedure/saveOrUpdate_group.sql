CREATE PROCEDURE [dbo].[saveOrUpdate_group]
@id int,
@name nvarchar(20),
@message nvarchar(50) output
AS

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_group WHERE id = @id) BEGIN
		SET @message = '该用户组已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_group WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '该该用户组名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_group SET [name] = @name WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_group WHERE [name] = @name) BEGIN
			SET @message = '该用户组名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_group([name]) VALUES(@name)
			RETURN 0
		END
END
