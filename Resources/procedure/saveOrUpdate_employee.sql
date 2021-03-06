USE [db_sanhai]
DROP PROCEDURE [dbo].[saveOrUpdate_employee]
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_employee]
@id int,
@name nvarchar(20),
@code varchar(20),
@sex bit,
@birthday datetime,
@id_card varchar(20),
@phone	varchar(20),
@join_date datetime,
@contract_date datetime,
@department int,
@position nvarchar(20),
@address nvarchar(50),
@is_locked bit,
@message nvarchar(50) output
AS

IF NOT EXISTS(SELECT * FROM tb_department WHERE id = @department) BEGIN
	SET @message = '该员工选择的部门，已不存在于系统'
		RETURN -1
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_employee WHERE id = @id) BEGIN
		SET @message = '该员工已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_employee WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '该该员工名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_employee SET [name] = @name,code = @code,sex = @sex, birthday = @birthday,
				id_card = @id_card, phone = @phone, join_date = @join_date, contract_date = @contract_date, department = @department,
				position = @position, [address] = @address, is_locked = @is_locked
				 WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_employee WHERE [name] = @name) BEGIN
			SET @message = '该员工名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_employee([name],code,sex, birthday,id_card, phone, join_date, contract_date, department,
				position, [address], is_locked)
				 VALUES(@name,@code,@sex, @birthday,@id_card, @phone, @join_date, @contract_date, @department,
				@position, @address, @is_locked)
			RETURN 0
		END
END
