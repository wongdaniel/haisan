DROP PROCEDURE saveOrUpdate_order_item
GO
CREATE PROCEDURE saveOrUpdate_order_item
@id int,
@order int,
@category_stone int,
@product_name int,
@length varchar(20),
@width varchar(20),
@thickness varchar(20),
@package int,
@unit nvarchar(10),
@number decimal(18,3),
@unit_price money,
@cost money,
@working_diagram_1 image,
@working_name_1 int,
@working_number_1 decimal(18,3),
@working_diagram_2 image,
@working_name_2 int,
@working_number_2 decimal(18,3),
@working_diagram_3 image,
@working_name_3 int,
@working_number_3 decimal(18,3),
@message nvarchar(50) output
AS 

IF NOT EXISTS(SELECT * FROM tb_order WHERE id = @order) BEGIN
	SET @message = '���¶�����ϸʧ�ܣ��ö�����'+CAST(@order AS varchar(20))+'���Ѳ�������ϵͳ'
		RETURN -1
END

IF NOT EXISTS(SELECT * FROM tb_category_stone WHERE id = @category_stone) BEGIN
	SET @message = '���¶�����ϸʧ�ܣ���ʯ�����͡�'+CAST(@category_stone AS varchar(20))+'���Ѳ�������ϵͳ'
		RETURN -1
END

IF NOT EXISTS(SELECT * FROM tb_product_name WHERE id = @product_name) BEGIN
	SET @message = '���¶�����ϸʧ�ܣ��ò�Ʒ��'+CAST(@product_name AS varchar(20))+'���Ѳ�������ϵͳ'
		RETURN -1
END

IF (@working_name_1 > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_typeOfProcess WHERE id = @working_name_1) BEGIN
	SET @message = '���¶�����ϸʧ�ܣ��üӹ�����1��'+CAST(@working_name_1 AS varchar(20))+'���Ѳ�������ϵͳ'
		RETURN -1
	END
END

IF (@working_name_2 > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_typeOfProcess WHERE id = @working_name_2) BEGIN
	SET @message = '���¶�����ϸʧ�ܣ��üӹ�����2��'+CAST(@working_name_2 AS varchar(20))+'���Ѳ�������ϵͳ'
		RETURN -1
	END
END

IF (@working_name_3 > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_typeOfProcess WHERE id = @working_name_3) BEGIN
	SET @message = '���¶�����ϸʧ�ܣ��üӹ�����3��'+CAST(@working_name_3 AS varchar(20))+'���Ѳ�������ϵͳ'
		RETURN -1
	END
END

IF (@id > 0) BEGIN
	IF NOT EXISTS(SELECT * FROM tb_order_item WHERE id = @id) BEGIN
		SET @message = '�ö�����ϸ�ѱ�ɾ�����޷�����'
		RETURN -1	
	END ELSE BEGIN
		UPDATE tb_order_item SET [order] = @order,category_stone = @category_stone,product_name = @product_name,
				[length] = @length,width = @width,thickness = @thickness,package = @package,unit = @unit,number = @number,unit_price = @unit_price,cost = @cost,
				working_diagram_1 = @working_diagram_1,working_name_1 = @working_name_1,working_number_1 = @working_number_1,
				working_diagram_2 = @working_diagram_2,working_name_2 = @working_name_2,working_number_2 = @working_number_2,
				working_diagram_3 = @working_diagram_3,working_name_3 = @working_name_3,working_number_3 = @working_number_3
			WHERE id = @id
		RETURN 0
	END
END ELSE BEGIN
		INSERT INTO tb_order_item([order],category_stone,product_name,[length],width,thickness,package,unit,number,unit_price,cost,
				working_diagram_1,working_name_1,working_number_1,working_diagram_2,working_name_2,working_number_2,working_diagram_3,working_name_3,working_number_3)
			VALUES(@order,@category_stone,@product_name,@length,@width,@thickness,@package,@unit,@number,@unit_price,@cost,
				@working_diagram_1,@working_name_1,@working_number_1,@working_diagram_2,@working_name_2,@working_number_2,@working_diagram_3,@working_name_3,@working_number_3)
		RETURN 0
END

