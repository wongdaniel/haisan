DROP PROCEDURE saveOrUpdate_tb_typeOfProcess
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_tb_typeOfProcess]
@id int,
@name nvarchar(20),
@unit nvarchar(10),
@message nvarchar(50) output
AS

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_typeOfProcess WHERE id = @id) BEGIN
		SET @message = '该名称已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_typeOfProcess WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '该产品名称已被使用'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_typeOfProcess SET [name] = @name,unit = @unit WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_typeOfProcess WHERE [name] = @name) BEGIN
			SET @message = '该产品名称已被使用'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_typeOfProcess([name],unit) VALUES(@name, @unit)
			RETURN 0
		END
END
