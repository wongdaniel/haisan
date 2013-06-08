DROP procedure del_departmentById
go
CREATE PROCEDURE del_departmentById
@id int
AS
IF EXISTS(SELECT * FROM tb_employee where department = @id) BEGIN
	return -1
END ELSE BEGIN
	DELETE FROM tb_department WHERE id = @id
	return 0
END