DROP VIEW [dbo].[tb_xialiao_order_view]
GO
CREATE VIEW [dbo].[tb_xialiao_order_view]
AS
SELECT     dbo.tb_xialiao_order.id, dbo.tb_xialiao_order.sn, dbo.tb_company.name, dbo.tb_order.way_of_payment, dbo.tb_order.phone, dbo.tb_xialiao_order.total_number, 
                      dbo.tb_xialiao_order.total_packages, dbo.tb_xialiao_order.payment, dbo.tb_xialiao_order.processing_charges, dbo.tb_xialiao_order.total_cost, 
                      dbo.tb_user.username, dbo.tb_xialiao_order.createDate, dbo.tb_order.company, dbo.tb_xialiao_order.operator, dbo.tb_xialiao_order.order_id
FROM         dbo.tb_xialiao_order INNER JOIN
					  dbo.tb_order ON dbo.tb_xialiao_order.order_id = dbo.tb_order.id INNER JOIN
                      dbo.tb_company ON dbo.tb_order.company = dbo.tb_company.id INNER JOIN
                      dbo.tb_user ON dbo.tb_order.operator = dbo.tb_user.id