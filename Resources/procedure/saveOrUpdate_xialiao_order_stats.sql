DROP PROCEDURE saveOrUpdate_xialiao_order_stats
GO
CREATE PROCEDURE saveOrUpdate_xialiao_order_stats
@id int,
@xialiao_order_id int,
@order_stats_id int,
@total_number decimal(18,3),
@amount_of_money money,
@message nvarchar(50) output
AS 

IF NOT EXISTS(SELECT * FROM tb_xialiao_order WHERE id = @xialiao_order_id) BEGIN
	SET @message = '更新下料订单明细失败，该订单【'+CAST(@xialiao_order_id AS varchar(20))+'】已不存在于系统'
	RETURN -1
END

IF NOT EXISTS(SELECT * FROM tb_order_stats WHERE id = @order_stats_id) BEGIN
	SET @message = '更新下料订单失败，该订单涉及的统计明细【'+CAST(@order_stats_id AS varchar(20))+'】已不存在于系统'
	RETURN -1
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_xialiao_order_stats WHERE id = @id) BEGIN
		SET @message = '该订单统计项已被删除，无法更新'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_xialiao_order_stats SET xialiao_order_id = @xialiao_order_id,order_stats_id = @order_stats_id,
			total_number = @total_number,amount_of_money = @amount_of_money
			WHERE id = @id
		SET @message = @id
		RETURN 0
	END
END ELSE BEGIN
		INSERT INTO tb_xialiao_order_stats(xialiao_order_id,order_stats_id,total_number,amount_of_money)
			VALUES(@xialiao_order_id,@order_stats_id,@total_number,@amount_of_money)
		SELECT @message = @@identity
		RETURN 0
END

