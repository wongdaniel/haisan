DROP PROCEDURE save_categoryOfProduct
GO
CREATE PROCEDURE save_categoryOfProduct
@parent int,
@name nvarchar(20),
@message nvarchar(20) output
AS
IF EXISTS (SELECT * FROM tb_categoryOfProduct WHERE (id = @parent)) BEGIN
	INSERT INTO tb_categoryOfProduct(name,parent) VALUES(@name,@parent)
	SET @message = '添加成功'
	RETURN 0
END
ELSE BEGIN
	SET @message = '该条记录已不存在于数据库！'
	RETURN -1;
END