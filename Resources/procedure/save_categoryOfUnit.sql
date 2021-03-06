USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[save_categoryOfUnit]    脚本日期: 05/18/2013 09:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[save_categoryOfUnit]
@parent int,
@name nvarchar(20),
@message nvarchar(20) output
AS
IF EXISTS (SELECT * FROM tb_categoryOfUnit WHERE (id = @parent)) BEGIN
	INSERT INTO tb_categoryOfUnit(name,parent) VALUES(@name,@parent)
	SET @message = '添加成功'
	RETURN 0
END
ELSE BEGIN
	SET @message = '该条记录已不存在于数据库！'
	RETURN -1;
END