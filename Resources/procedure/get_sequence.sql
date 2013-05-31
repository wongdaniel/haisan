CREATE PROCEDURE get_sequence
AS
BEGIN
      -- ������Sequenceֵ����
      DECLARE @NewSeqValue INT

      -- ���ò��롢ɾ���������������ʾȡ��
      SET NOCOUNT ON

      -- ������ֵ��SeqT_0101001��
      INSERT INTO tb_sequence ([value]) values (0)   

      -- ������SequenceֵΪ���뵽SeqT_0101001��ı�ʶ���ڵ����һ����ʶֵ  
      SET @NewSeqValue = scope_identity()   

      -- ɾ��SeqT_0101001��(����ʾ������)
      DELETE FROM get_sequence WITH (READPAST)

-- ������Sequenceֵ
RETURN @NewSeqValue
END