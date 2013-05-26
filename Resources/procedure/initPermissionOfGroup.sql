DROP PROCEDURE initPermissionOfGroup
GO
CREATE PROCEDURE initPermissionOfGroup
@group INT
AS
DECLARE @module_id INT
DECLARE @str VARCHAR(500)
DECLARE @next INT

SET @str='module_custom,module_provider,module_employee,module_storehouse,module_doc,module_stock'
SET @next=1
WHILE @next<=dbo.Get_StrArrayLength(@str,',')
BEGIN
	SELECT @module_id  = id FROM tb_module WHERE [name] = dbo.Get_StrArrayStrOfIndex(@str,',',@next)
	INSERT INTO tb_group_module([group],module, query,[add], [modify],[delete],[print],run,save_table,[check],anticheck)
		VALUES(@group, @module_id, 1, 1, 1, 1, 1, 1, 1, 2, 2)
 SET @next=@next+1
END

SET @str = 'module_relogin,module_chgpwd,module_configure,module_user_mgr,module_group_mgr'
SET @next=1
WHILE @next<=dbo.Get_StrArrayLength(@str,',')
BEGIN
	SELECT @module_id  = id FROM tb_module WHERE [name] = dbo.Get_StrArrayStrOfIndex(@str,',',@next)
	INSERT INTO tb_group_module([group],module, query,[add], [modify],[delete],[print],run,save_table,[check],anticheck)
		VALUES(@group, @module_id, 2, 2, 2, 2, 2, 1, 2, 2, 2)
 SET @next=@next+1
END

