CREATE PROCEDURE get_permissionByGroup
@group int
AS
SELECT tb.name, tgm.id, tgm.query,tgm.[add], tgm.[modify],tgm.[delete],
tgm.[print],tgm.run,tgm.save_table,tgm.[check],tgm.anticheck
FROM tb_group_module tgm, tb_module tb WHERE tb.is_terminal = 1 AND tgm.[group] = @group AND tgm.module = tb.id