DROP PROCEDURE saveOrUpdate_tb_product_name
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_tb_product_name]
@id int,
@name nvarchar(20),
@message nvarchar(50) output
AS

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_product_name WHERE id = @id) BEGIN
		SET @message = '�ò�Ʒ�����ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_product_name WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '�ò�Ʒ�����ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_product_name SET [name] = @name WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_product_name WHERE [name] = @name) BEGIN
			SET @message = '�ò�Ʒ�����ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_product_name([name]) VALUES(@name)
			RETURN 0
		END
END
