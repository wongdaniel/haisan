DROP FUNCTION specification
GO
CREATE FUNCTION specification(
@length varchar(20),
@width varchar(20),
@thickness varchar(20))
RETURNS varchar(60) AS
BEGIN
	DECLARE @strReturn varchar(60)
	DECLARE @index int

	SET @strReturn = ''
	
	SET  @index= charindex('(', @length)
	IF(@index = 0)
		SET @strReturn = @strReturn +  @length
	ELSE
		SET @strReturn = @strReturn + substring(@length, 0, @index + 1)

	SET @index = charindex('(', @width)
	IF(@index = 0)
		SET @strReturn = @strReturn + 'X' + @width
	ELSE
		SET @strReturn = @strReturn + substring(@width, 0, @index + 1)

	SET @strReturn = @strReturn + 'X' + @thickness

	RETURN @strReturn
END