CREATE PROCEDURE save_product
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
IF EXISTS(SELECT * FROM tb_company WHERE id = @provider) BEGIN
	IF EXISTS(SELECT * FROM tb_categoryOfProduct WHERE id = @category) BEGIN
		IF EXISTS(SELECT * FROM tb_product WHERE [name] = @name) BEGIN
			SET @message = '该产品名已被使用'
			RETURN -1	
		END
		ELSE BEGIN
			INSERT tb_product([name],code,spec,provider,category,[address],unit,purchase_price,sale_price,
				wholesale_price,[description],[image]) VALUES(@name,@code,@spec,@provider,@category,@address,
				@unit,@purchase_price,@sale_price,@wholesale_price,@description,@image)
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