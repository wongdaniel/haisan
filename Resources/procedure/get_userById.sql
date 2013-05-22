USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[get_productById]    脚本日期: 05/19/2013 23:09:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_userById]
@id int
AS
SELECT tu.id, tu.username, tu.password, tu.[group], tg.name AS group_name, 
tu.name, tu.email, tu.phone, tu.description, tu.isLock, tu.lastLoginTime, 
tu.isOnline, tu.createTime 
FROM tb_user tu, tb_group tg WHERE tu.id = @id AND tu.[group] = tg.id
