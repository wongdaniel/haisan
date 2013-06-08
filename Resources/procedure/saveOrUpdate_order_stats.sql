DROP PROCEDURE saveOrUpdate_order_stats
GO
CREATE PROCEDURE saveOrUpdate_order_stats
@id int,
@order int,
@type_of_process int,
@thickness varchar(20),
@image image,
@dwg image,
@unit nvarchar(10),
@total_number decimal(18,3),
@unit_price money,
@amount_of_money money,
@message nvarchar(50) output
AS 

IF NOT EXISTS(SELECT * FROM tb_order WHERE id = @order) BEGIN
	SET @message = '更新订单明细失败，该订单【'+CAST(@order AS varchar(20))+'】已不存在于系统'
		RETURN -1
END

IF (@type_of_process > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_typeOfProcess WHERE id = @type_of_process) BEGIN
	SET @message = '更新订单明细失败，该加工类型【'+CAST(@type_of_process AS varchar(20))+'】已不存在于系统'
		RETURN -1
	END
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_order_stats WHERE id = @id) BEGIN
		SET @message = '该订单统计项已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_order_stats SET [order] = @order,type_of_process = @type_of_process, thickness = @thickness, [image] = @image, dwg = @dwg, unit = @unit,
			total_number = @total_number,unit_price = @unit_price,amount_of_money = @amount_of_money
			WHERE id = @id
		RETURN 0
	END
END ELSE BEGIN
		INSERT INTO tb_order_stats([order],type_of_process, thickness, [image], dwg, unit,total_number,unit_price,amount_of_money)
			VALUES(@order,@type_of_process, @thickness, @image, @dwg, @unit,@total_number,@unit_price,@amount_of_money)
		RETURN 0
END

