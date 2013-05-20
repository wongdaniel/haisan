CREATE PROCEDURE update_product
@id int,
@name nvarchar(50),
@code varchar(50),
@spec varchar(50),
@provider int,
@category int,
@address nvarchar(20),
@unit nvarchar(10),
@purchase_price money,
@sale_price money,
@wholesale_price money,
@description nvarchar(100),
@image image,
@message nvarchar(50) output
AS

IF NOT EXISTS(SELECT * FROM tb_product WHERE id = @id) BEGIN
	SET @message = '该产品已被删除，无法更新'
	RETURN -1	
END

IF EXISTS(SELECT * FROM tb_company WHERE id = @provider) BEGIN
	IF EXISTS(SELECT * FROM tb_categoryOfProduct WHERE id = @category) BEGIN
		IF EXISTS(SELECT * FROM tb_product WHERE [name] = @name AND id != @id) BEGIN
			SET @message = '该产品名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
			UPDATE tb_product SET [name] = @name, code = @code, spec = @spec, provider = @provider, category = @category,
				[address] = @address, unit = @unit, purchase_price = @purchase_price,sale_price = @sale_price,
				wholesale_price = @wholesale_price, [description] = @description, [image] = @image
				WHERE id = @id
			RETURN 0
		END
	END
	ELSE BEGIN
		SET @message = '该产品选择的类型，已不存在于系统'
		RETURN -1
	END
END
ELSE BEGIN
	SET @message = '该产品选择的供应商，已不存在于系统'
	RETURN -1
END