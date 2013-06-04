DROP FUNCTION union_numberAndUnit
GO
CREATE FUNCTION [dbo].[union_numberAndUnit](
@number decimal,
@unit nvarchar(10))
RETURNS varchar(60) AS
BEGIN
	DECLARE @car varchar(30)
	DECLARE @i int

	IF(@number > 0) BEGIN

		SET @i=0
		SET @car=str(@number, 18, 3);--字符串左右填0，总长度为30位，小数点后10位 
		SET @i=1; 
		--除去左边的空格
		WHILE substring(@car,@i,1)=' ' BEGIN
			SET @i=@i+1 
		END 
		--判断最左边的是否为'.' 
		IF (substring(@car,@i,1) = '.') BEGIN 
			SET @i=@i-1
		END
		SET @car=substring(@car,@i, len(@car) - @i); 
		RETURN (@car + @unit)
	END
	RETURN ''
END