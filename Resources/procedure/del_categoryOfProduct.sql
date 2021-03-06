USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[del_categoryOfProduct]    脚本日期: 05/18/2013 09:46:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[del_categoryOfProduct]
@id int
AS
WITH tmp AS
(
 SELECT * FROM tb_categoryOfProduct WHERE id = @id
 UNION ALL
 SELECT tb_categoryOfProduct.* FROM tb_categoryOfProduct JOIN tmp 
	ON tb_categoryOfProduct.parent = tmp.id
	WHERE tb_categoryOfProduct.id != tmp.id
)
DELETE tb_categoryOfProduct
FROM tmp
WHERE tb_categoryOfProduct.id = tmp.id