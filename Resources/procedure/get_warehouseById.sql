CREATE PROCEDURE get_warehouseById
@id int
AS
SELECT tw.id, tw.name, tw.description, tw.is_locked
FROM tb_warehouse tw WHERE tw.id = @id