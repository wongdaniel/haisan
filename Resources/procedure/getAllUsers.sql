DROP PROCEDURE getAllUsers;
GO
CREATE PROCEDURE getAllUsers
@username nchar(20),
@address int
AS
IF (@address < 0) BEGIN
SELECT * FROM view_user WHERE (username LIKE '%' + RTRIM(@username)+ '%')
END ELSE BEGIN
SELECT tb_user.username, tb_user.password, tb_user.phone, tb_user.email, tb_zone.[desc] 
       FROM  tb_user LEFT OUTER JOIN tb_zone ON tb_user.address = tb_zone.id 
       WHERE (tb_user.username like '%' + RTRIM(@username) + '%' AND tb_user.address = @address)
END

execute getAllUsers @username = 'a', @address = -1
--SELECT * FROM view_user WHERE (username LIKE '%%' + 'a'+ '%%')