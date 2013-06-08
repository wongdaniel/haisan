DROP PROCEDURE [dbo].[get_reportOfOrderStats]
GO
CREATE PROCEDURE [dbo].[get_reportOfOrderStats]
@order int
AS
SELECT tos.image AS processingDiagram, tos.unit AS unitStats,
tos.total_number AS numberStats, tos.unit_price AS unitPriceStats,
tos.amount_of_money AS costStats,
 tt.name + '(' + tos.thickness + ')' AS processingName FROM tb_order_stats AS tos, tb_typeOfProcess AS tt 
                WHERE tos.[order] = @order AND tos.type_of_process = tt.id