USE [db_sanhai]
GO
/****** 对象:  StoredProcedure [dbo].[get_permissionByModule]    脚本日期: 05/24/2013 09:09:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[get_permissionByModule]
@module int,
@group int
AS
WITH tmp AS
(
 SELECT * FROM tb_module WHERE id = @module
 UNION ALL
 SELECT tb_module.* FROM tb_module JOIN tmp 
	ON tb_module.parent = tmp.id
	WHERE tb_module.id != tmp.id
)

SELECT tgm.id, tm.display_name, tgm.query,tgm.[add], tgm.[modify],tgm.[delete],
tgm.[print],tgm.run,tgm.save_table,tgm.[check],tgm.anticheck
FROM tb_group_module AS tgm, tb_module AS tm
WHERE tgm.[group] = @group AND tgm.module in (SELECT id FROM tmp WHERE is_terminal = 1) AND tgm.[module] = tm.id