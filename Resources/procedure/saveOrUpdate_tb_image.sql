CREATE PROCEDURE saveOrUpdate_tb_image
@id int,
@name nvarchar(20),
@image image,
@up bit,
@down bit,
@left bit,
@right bit,
@message nvarchar(20) output
AS 
IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_image WHERE id = @id) BEGIN
		SET @message = '该加工图片已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_image SET [name] = @name, [image] = @image, up = @up, down = @down,
			[left] = @left, [right] = @right WHERE id = @id
		RETURN 0
	END
END ELSE BEGIN
		INSERT INTO tb_image([name],[image],up,down,[left],[right]) VALUES(@name, @image,@up,@down,@left,@right)
		RETURN 0
END