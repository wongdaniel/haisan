DROP PROCEDURE get_sequence
GO
CREATE PROCEDURE get_sequence
AS
BEGIN
      -- 声明新Sequence值变量
      DECLARE @NewSeqValue INT

      -- 设置插入、删除操作后的条数显示取消
      SET NOCOUNT ON

      -- 插入新值到SeqT_0101001表
	  SELECT @NewSeqValue = sn FROM tb_sequence WHERE id = 1
	  SET @NewSeqValue = @NewSeqValue + 1
	  UPDATE tb_sequence SET sn = @NewSeqValue where id = 1  

-- 返回新Sequence值
RETURN @NewSeqValue
END