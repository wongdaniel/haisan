declare @username  varchar(20)
declare @password varchar(20)
declare @message_ nvarchar(20)

execute [login] @username = 'daniel',
@password = '123', @message = @message_ output
select @message_ as '·µ»ØÏûÏ¢'