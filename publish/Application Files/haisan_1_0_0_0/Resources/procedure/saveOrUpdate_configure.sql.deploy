CREATE PROCEDURE [dbo].[saveOrUpdate_configure]
@name varchar(20),
@value nvarchar(50)
AS
IF EXISTS(SELECT * FROM tb_configure WHERE [name] = @name)BEGIN
	UPDATE tb_configure SET [value] = @value WHERE [name] = @name
END
ELSE BEGIN
	INSERT INTO tb_configure([name],[value]) VALUES(@name, @value)
END
