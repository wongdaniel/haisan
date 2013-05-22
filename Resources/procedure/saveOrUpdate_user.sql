CREATE PROCEDURE [dbo].[saveOrUpdate_user]
@id int,
@username varchar(20),
@password varchar(20),
@group int,
@name nvarchar(20),
@email varchar(50),
@phone varchar(20),
@description nvarchar(100),
@isLock bit,
@isOnline bit,
@createTime datetime,
@message nvarchar(50) output
AS

IF NOT EXISTS(SELECT * FROM tb_group WHERE id = @group) BEGIN
	SET @message = '���û�ѡ����飬�Ѳ�������ϵͳ'
		RETURN -1
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_user WHERE id = @id) BEGIN
		SET @message = '���û��ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_user WHERE username = @username AND id != @id) BEGIN
			SET @message = '�ø��û����ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
				UPDATE tb_user SET username = @username, [password] = @password, [group] = @group, [name] = @name,
					email = @email, phone = @phone, [description] = @description, isLock = @isLock, isOnline = @isOnline,
					createTime = @createTime WHERE id = @id
			RETURN 0
		END
	END
END ELSE BEGIN
		IF EXISTS(SELECT * FROM tb_user WHERE username = @username) BEGIN
			SET @message = '���û����ѱ�ʹ��'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT INTO tb_user(username, [password], [group], [name],email, phone, [description], isLock, isOnline,createTime)
				 VALUES(@username, @password, @group, @name, @email, @phone, @description, @isLock, @isOnline, @createTime)
			RETURN 0
		END
END
