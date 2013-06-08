DROP PROCEDURE [dbo].[saveOrUpdate_department]
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_department]
@id int,
@name varchar(20),
@description varchar(20),
@message nvarchar(50) output
AS

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_department WHERE id = @id) BEGIN
		SET @message = '�ò����ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_department WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '�ò������ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_department SET [name] = @name, [description] = @description
					 WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_department WHERE [name] = @name) BEGIN
			SET @message = '�ò������ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_department([name], [description])
				 VALUES(@name, @description)
			RETURN 0
		END
END
