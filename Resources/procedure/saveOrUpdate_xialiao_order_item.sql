DROP PROCEDURE saveOrUpdate_xialiao_order_item
GO
CREATE PROCEDURE saveOrUpdate_xialiao_order_item
@id int,
@xialiao_order_id int,
@order_item_id int,
@original_package int,
@remain_package int,
@use_package int,
@working_number_1 decimal(18,3),
@working_number_2 decimal(18,3),
@working_number_3 decimal(18,3),
@message nvarchar(50) output
AS 

IF NOT EXISTS(SELECT * FROM tb_xialiao_order WHERE id = @xialiao_order_id) BEGIN
	SET @message = '更新下料订单明细失败，该订单【'+CAST(@xialiao_order_id AS varchar(20))+'】已不存在于系统'
	RETURN -1
END

IF NOT EXISTS(SELECT * FROM tb_order_item WHERE id = @order_item_id) BEGIN
	SET @message = '更新下料订单明细失败，该订单涉及的销售订单明细【'+CAST(@order_item_id AS varchar(20))+'】已不存在于系统'
	RETURN -1
END


IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_xialiao_order_item WHERE id = @id) BEGIN
		SET @message = '该下料订单明细已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_xialiao_order_item SET xialiao_order_id = @xialiao_order_id, order_item_id = @order_item_id, original_package = @original_package,
				remain_package = @remain_package, use_package = @use_package,working_number_1 = @working_number_1,
				working_number_2 = @working_number_2,working_number_3 = @working_number_3
			WHERE id = @id
		SET @message = @id
		RETURN 0
	END
END ELSE BEGIN
		INSERT INTO tb_xialiao_order_item(xialiao_order_id,order_item_id,original_package,remain_package,use_package,working_number_1,working_number_2,working_number_3)
			VALUES(@xialiao_order_id, @order_item_id, @original_package, @remain_package, @use_package, @working_number_1,@working_number_2,@working_number_3)
		SELECT @message = @@identity
		RETURN 0
END

