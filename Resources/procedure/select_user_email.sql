/*create procedure select_user_email
@username nchar(20),
@email nchar(20) output
as
select @email = email from tb_user where username = @username
return @email
*/
declare  @email nchar(20), @email_ nchar(20)
exec select_user_email @username = 'haha', @email = @email_ output
select @email_ 'ÓÊ¼þ';
