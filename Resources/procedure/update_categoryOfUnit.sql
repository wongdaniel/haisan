USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[update_categoryOfUnit]    脚本日期: 05/18/2013 09:42:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[update_categoryOfUnit]
@id int,
@name nvarchar(20),
@message nvarchar(20) output
AS
IF EXISTS (SELECT * FROM tb_categoryOfUnit WHERE (id = @id)) BEGIN
	UPDATE tb_categoryOfUnit SET name = @name WHERE (id = @id)
	SET @message = '更新成功'
	RETURN 0
END
ELSE BEGIN
	SET @message = '该条记录已不存在于数据库！'
	RETURN -1;
END