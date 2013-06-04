DROP PROCEDURE [dbo].[get_reportOfOrderItem]
GO
CREATE PROCEDURE [dbo].[get_reportOfOrderItem]
@order int
AS
SELECT category_stone_name AS stoneName, product_name_name AS productName, width, [length], thickness, 
dbo.specification([length], width, thickness) AS specification,
package, unit, number, 
name1 AS workingName1, dbo.union_numberAndUnit(working_number_1, unit1) AS workingNumber1, working_diagram_1 AS workingDiagram1,
name2 AS workingName2,  dbo.union_numberAndUnit(working_number_2, unit2) AS workingNumber2, working_diagram_2 AS workingDiagram2,
name3 AS workingName3,  dbo.union_numberAndUnit(working_number_3, unit3) AS workingNumber3, working_diagram_3 AS workingDiagram3
FROM tb_order_item_view WHERE [order] = @order