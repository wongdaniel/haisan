CREATE PROCEDURE get_departmentById
@id int
AS
SELECT tw.id, tw.name, tw.description
FROM tb_department tw WHERE tw.id = @id