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
		SET @car=str(@number, 18, 3);--�ַ���������0���ܳ���Ϊ30λ��С�����10λ 
		SET @i=1; 
		--��ȥ��ߵĿո�
		WHILE substring(@car,@i,1)=' ' BEGIN
			SET @i=@i+1 
		END 
		--�ж�����ߵ��Ƿ�Ϊ'.' 
		IF (substring(@car,@i,1) = '.') BEGIN 
			SET @i=@i-1
		END
		SET @car=substring(@car,@i, len(@car) - @i); 
		RETURN (@car + @unit)
	END
	RETURN ''
END