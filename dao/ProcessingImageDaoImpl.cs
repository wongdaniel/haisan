using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using haisan.util;
using System.Data;
using System.Data.SqlClient;

namespace haisan.dao
{
    class ProcessingImageDaoImpl : ProcessingImageDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

        private static ProcessingImageDaoImpl processingImageDaoImpl = null;

        private ProcessingImageDaoImpl() { }

        public static ProcessingImageDaoImpl getInstance()
        {
            if (null == processingImageDaoImpl)
                processingImageDaoImpl = new ProcessingImageDaoImpl();
            return processingImageDaoImpl;
        }

        public MessageLocal saveOrUpdateProcessingImage(ProcessingImage processingImage)
        {
            MessageLocal msg = new MessageLocal();
            byte[] buffByte = Util.imageToByteArray(processingImage.Image);

            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, processingImage.Id),
                                    database.MakeInParam("@name", SqlDbType.NVarChar, 20, processingImage.Name),
                                    database.MakeInParam("@image", SqlDbType.Image, 0, buffByte),
                                    database.MakeInParam("@up", SqlDbType.Bit, 0, processingImage.Up),
                                    database.MakeInParam("@down", SqlDbType.Bit, 0, processingImage.Down),
                                    database.MakeInParam("@left", SqlDbType.Bit, 0, processingImage.Left),
                                    database.MakeInParam("@right", SqlDbType.Bit, 0, processingImage.Right),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};

            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return baseDao.runProcedure("saveOrUpdate_tb_image", prams, "tb_image");
        }
    }
}
