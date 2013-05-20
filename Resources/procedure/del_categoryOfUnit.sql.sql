USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[del_categoryOfUnit]    脚本日期: 05/18/2013 09:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[del_categoryOfUnit]
@id int
AS
WITH tmp AS
(
 SELECT * FROM tb_categoryOfUnit WHERE id = @id
 UNION ALL
 SELECT tb_categoryOfUnit.* FROM tb_categoryOfUnit JOIN tmp 
	ON tb_categoryOfUnit.parent = tmp.id
	WHERE tb_categoryOfUnit.id != tmp.id
)
DELETE tb_categoryOfUnit
FROM tmp
WHERE tb_categoryOfUnit.id = tmp.id