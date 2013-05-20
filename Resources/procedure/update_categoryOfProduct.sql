DROP PROCEDURE update_categoryOfProduct
GO
CREATE PROCEDURE update_categoryOfProduct
@id int,
@name nvarchar(20),
@message nvarchar(20) output
AS
IF EXISTS (SELECT * FROM tb_categoryOfProduct WHERE (id = @id)) BEGIN
	UPDATE tb_categoryOfProduct SET name = @name WHERE (id = @id)
	SET @message = '更新成功'
	RETURN 0
END
ELSE BEGIN
	SET @message = '该条记录已不存在于数据库！'
	RETURN -1;
END