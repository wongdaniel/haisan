USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[get_productById]    脚本日期: 05/19/2013 23:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_productById]
@id int
AS
SELECT tp.id, tp.name, tp.code, tp.spec, tp.provider, tp.category, tp.address, tp.unit, tp.purchase_price,
tp.sale_price, tp.wholesale_price, tp.description, tp.image, tc.name AS company_name, tco.name AS category_name
FROM tb_product as tp, tb_company as tc, tb_categoryOfProduct as tco 
WHERE tp.id = @id AND tp.provider = tc.id  AND tp.category = tco.id
