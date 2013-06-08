DROP PROCEDURE get_sequence
GO
CREATE PROCEDURE get_sequence
AS
BEGIN
      -- ������Sequenceֵ����
      DECLARE @NewSeqValue INT

      -- ���ò��롢ɾ���������������ʾȡ��
      SET NOCOUNT ON

      -- ������ֵ��SeqT_0101001��
	  SELECT @NewSeqValue = sn FROM tb_sequence WHERE id = 1
	  SET @NewSeqValue = @NewSeqValue + 1
	  UPDATE tb_sequence SET sn = @NewSeqValue where id = 1  

-- ������Sequenceֵ
RETURN @NewSeqValue
END