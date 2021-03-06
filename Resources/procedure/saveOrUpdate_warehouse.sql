CREATE PROCEDURE [dbo].[saveOrUpdate_warehouse]
@id int,
@name varchar(20),
@is_locked bit,
@description varchar(20),
@message nvarchar(50) output
AS

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_warehouse WHERE id = @id) BEGIN
		SET @message = '该仓库已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_warehouse WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '该仓库名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_warehouse SET [name] = @name, [description] = @description, is_locked = @is_locked
					 WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_warehouse WHERE [name] = @name) BEGIN
			SET @message = '该仓库名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_warehouse([name], [description],is_locked)
				 VALUES(@name, @description, @is_locked)
			RETURN 0
		END
END
