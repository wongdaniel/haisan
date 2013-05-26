CREATE PROCEDURE initPermissionOfModule
@module INT,
@query SMALLINT,
@add SMALLINT,
@modify SMALLINT,
@delete SMALLINT,
@print SMALLINT,
@run SMALLINT,
@save_table SMALLINT,
@check SMALLINT,
@anticheck SMALLINT
AS
DECLARE @group INT

DECLARE Temp_Table CURSOR FOR
        SELECT id FROM tb_group

    OPEN Temp_Table
    FETCH NEXT FROM Temp_Table INTO @group

    WHILE @@fetch_status = 0
    BEGIN
		INSERT INTO tb_group_module([group],module, query,[add], [modify],[delete],[print],run,save_table,[check],anticheck)
		VALUES(@group, @module, @query, @add, @modify, @delete, @print, @run, @save_table, @check, @anticheck)

        FETCH NEXT FROM Temp_Table into @group
    END
CLOSE Temp_Table
DEALLOCATE Temp_Table
