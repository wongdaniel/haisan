DROP PROCEDURE [dbo].[saveOrUpdate_order]
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_order]
@id int,
@sn varchar(20),
@company int,
@way_of_payment nvarchar(20),
@phone varchar(20),
@createDate datetime,
@operator int,
@total_number decimal(18,3),
@total_packages int,
@payment money,
@processing_charges money,
@total_cost money,
@advances_received money,
@message nvarchar(50) output
AS 

IF NOT EXISTS(SELECT * FROM tb_company WHERE id = @company) BEGIN
	SET @message = '�ö���ѡ��Ŀͻ���'+CAST(@company AS varchar(20))+'�����Ѳ�������ϵͳ'
		RETURN -1
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_order WHERE id = @id) BEGIN
		SET @message = '�ö����ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_order SET sn = @sn,company = @company,way_of_payment = @way_of_payment,phone = @phone,
			createDate = @createDate,operator = @operator,total_number = @total_number,
			total_packages = @total_packages,payment = @payment,processing_charges = @processing_charges,
			total_cost = @total_cost,advances_received = @advances_received
			WHERE id = @id
		RETURN 0
	END
END ELSE BEGIN
		INSERT INTO tb_order(sn,company,way_of_payment,phone,createDate,operator,total_number,total_packages,payment,processing_charges,total_cost,advances_received)
			VALUES(@sn,@company,@way_of_payment,@phone,@createDate,@operator,@total_number,@total_packages,@payment,@processing_charges,@total_cost,@advances_received)
		RETURN 0
END
