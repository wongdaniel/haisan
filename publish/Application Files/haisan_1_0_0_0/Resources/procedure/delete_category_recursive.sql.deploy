DROP PROCEDURE del_categoryOfProduct
GO
CREATE PROCEDURE del_categoryOfProduct
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