CREATE function Get_StrArrayLength
(
 @str varchar(1024),  --要分割的字符串
 @split varchar(10)  --分隔符号
)
returns int
as
 begin
  declare @location int
  declare @start int
  declare @length int
  set @str=ltrim(rtrim(@str))
  set @location=charindex(@split,@str)
  set @length=1
   while @location<>0
     begin
      set @start=@location+1
      set @location=charindex(@split,@str,@start)
      set @length=@length+1
     end
   return @length
 end
GO
CREATE function Get_StrArrayStrOfIndex
(
 @str varchar(1024),  --要分割的字符串
 @split varchar(10),  --分隔符号
 @index int --取第几个元素
)
returns varchar(1024)
as
begin
 declare @location int
 declare @start int
 declare @next int
 declare @seed int
 set @str=ltrim(rtrim(@str))
 set @start=1
 set @next=1
 set @seed=len(@split)
 set @location=charindex(@split,@str)
 while @location<>0 and @index>@next
   begin
    set @start=@location+@seed
    set @location=charindex(@split,@str,@start)
    set @next=@next+1
   end
 if @location =0 select @location =len(@str)+1

--这儿存在两种情况：1、字符串不存在分隔符号 2、字符串中存在分隔符号，跳出while循环后，@location为0，那默认为字符串后边有一个分隔符号。
 return substring(@str,@start,@location-@start)
end