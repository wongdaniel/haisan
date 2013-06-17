--获取下料单的SN编号
DROP FUNCTION getXialiaoSN
GO
CREATE FUNCTION getXialiaoSN(
@order_id int)
RETURNS varchar(20) AS
BEGIN
	DECLARE @str varchar(20)

	IF NOT EXISTS(SELECT sn FROM tb_xialiao_order WHERE order_id = @order_id) BEGIN

		SELECT @str = sn FROM tb_order WHERE id = @order_id
		SET @str = @str + '-x01'
	END ELSE BEGIN

		SELECT top 1 @str = sn FROM tb_xialiao_order WHERE order_id = @order_id ORDER BY id DESC
		SELECT @str = STUFF(@str, len(@str), 1, RIGHT(@str, 1) + 1)
	END
	
	RETURN @str
END