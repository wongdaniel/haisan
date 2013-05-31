CREATE PROCEDURE get_sequence
AS
BEGIN
      -- 声明新Sequence值变量
      DECLARE @NewSeqValue INT

      -- 设置插入、删除操作后的条数显示取消
      SET NOCOUNT ON

      -- 插入新值到SeqT_0101001表
      INSERT INTO tb_sequence ([value]) values (0)   

      -- 设置新Sequence值为插入到SeqT_0101001表的标识列内的最后一个标识值  
      SET @NewSeqValue = scope_identity()   

      -- 删除SeqT_0101001表(不显示被锁行)
      DELETE FROM get_sequence WITH (READPAST)

-- 返回新Sequence值
RETURN @NewSeqValue
END