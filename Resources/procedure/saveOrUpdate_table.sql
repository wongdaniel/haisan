DROP PROCEDURE saveOrUpdate_table
GO
CREATE PROCEDURE saveOrUpdate_table
@user INT,
@table INT,
@original_index INT,
@display_index INT,
@width INT,
@sort INT
AS
IF EXISTS(SELECT * FROM tb_table_style where [user] = @user AND [table] = @table AND original_index = @original_index) BEGIN
	UPDATE tb_table_style SET display_index = @display_index, width = @width, sort = @sort
		WHERE [user] = @user AND [table] = @table AND original_index = @original_index
END
ELSE BEGIN
	INSERT INTO tb_table_style([user], [table], original_index, display_index, width, sort) 
		VALUES(@user, @table, @original_index, @display_index, @width, @sort) 
END