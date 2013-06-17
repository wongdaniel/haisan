DROP PROCEDURE [dbo].[saveOrUpdate_xialiao_order]
GO
CREATE PROCEDURE [dbo].[saveOrUpdate_xialiao_order]
@id int,
@sn varchar(20),
@order_id int,
@createDate datetime,
@operator int,
@total_number decimal(18,3),
@total_packages int,
@payment money,
@processing_charges money,
@total_cost money,
@message nvarchar(50) output
AS 
DECLARE @newsn varchar(20)
IF NOT EXISTS(SELECT * FROM tb_order WHERE id = @order_id) BEGIN
	SET @message = '�����۶���,ID��'+@order_id+'�����Ѳ�������ϵͳ'
		RETURN -1
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_xialiao_order WHERE id = @id) BEGIN
		SET @message = '�ö����ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_xialiao_order SET sn = @sn,
			createDate = @createDate,operator = @operator,total_number = @total_number,
			total_packages = @total_packages,payment = @payment,processing_charges = @processing_charges,
			total_cost = @total_cost
			WHERE id = @id
		RETURN 0
	END
END ELSE BEGIN
		SET @newsn = dbo.getXialiaoSN(@order_id);
		INSERT INTO tb_xialiao_order(sn,order_id,createDate,operator,total_number,total_packages,payment,processing_charges,total_cost)
			VALUES(@newsn, @order_id,@createDate,@operator,@total_number,@total_packages,@payment,@processing_charges,@total_cost)
		SET @message = @newsn
		RETURN 0
END
