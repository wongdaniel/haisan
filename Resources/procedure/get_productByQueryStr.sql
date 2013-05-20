DROP PROCEDURE get_productByQueryStr
GO
CREATE PROCEDURE get_productByQueryStr
@category int,
@queryStr nvarchar(50)
AS

WITH tmp AS
(
 SELECT * FROM tb_categoryOfProduct WHERE id = @category
 UNION ALL
 SELECT tb_categoryOfProduct.* FROM tb_categoryOfProduct JOIN tmp 
	ON tb_categoryOfProduct.parent = tmp.id
	WHERE tb_categoryOfProduct.id != tmp.id
)

SELECT * INTO #local_tmp FROM tmp
SELECT id, [name], code, spec, Expr1, Expr2, [address], unit, purchase_price, sale_price, wholesale_price, [description]
	 FROM tb_product_view WHERE category in (SELECT id FROM #local_tmp) AND ([name] LIKE @queryStr OR code LIKE @queryStr OR spec LIKE @queryStr)
SELECT count(*) AS total
	 FROM tb_product_view WHERE category in (SELECT id FROM #local_tmp) AND ([name] LIKE @queryStr OR code LIKE @queryStr OR spec LIKE @queryStr)