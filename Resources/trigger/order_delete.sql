DROP TRIGGER order_delete
GO
CREATE TRIGGER order_delete 
ON tb_order
AFTER DELETE 
AS BEGIN

DELETE FROM tb_order_item 
WHERE [order] in (SELECT id FROM deleted)

DELETE FROM tb_order_stats
WHERE [order] in (SELECT id FROM deleted) 

END