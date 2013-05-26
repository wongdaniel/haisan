DROP PROCEDURE saveOrUpdate_tb_category_stone
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_tb_category_stone]
@id int,
@name nvarchar(20),
@message nvarchar(50) output
AS

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_category_stone WHERE id = @id) BEGIN
		SET @message = '��ʯ�������ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_category_stone WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '��ʯ���������ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_category_stone SET [name] = @name WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_category_stone WHERE [name] = @name) BEGIN
			SET @message = '��ʯ���������ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_category_stone([name]) VALUES(@name)
			RETURN 0
		END
END
