CREATE TRIGGER [dbo].[xialiao_order_delete] 
ON [dbo].[tb_xialiao_order]
AFTER DELETE 
AS BEGIN

DELETE FROM tb_xialiao_order_item 
WHERE xialiao_order_id in (SELECT id FROM deleted)

DELETE FROM tb_xialiao_order_stats
WHERE xialiao_order_id in (SELECT id FROM deleted) 

END