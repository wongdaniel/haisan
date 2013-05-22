using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;
using haisan.dao;
using haisan.util;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace haisan.dao
{
    class UserDaoImpl:UserDao
    {
        private static Database database = Database.getInstance();
        private static UserDaoImpl userDaoImpl = null;

        private UserDaoImpl() 
        { 
        }
        public static UserDaoImpl getInstance()
        {
            if(null == userDaoImpl)
                userDaoImpl = new UserDaoImpl();
            return userDaoImpl;
        }

        public MessageLocal login(User user)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									    database.MakeInParam("@username",  SqlDbType.VarChar, 20, user.Username),
                						database.MakeInParam("@password",  SqlDbType.VarChar, 20, user.Password),
                                        new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                        new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[2].Direction = ParameterDirection.Output;
            prams[3].Direction = ParameterDirection.ReturnValue;

            DataSet dataset = database.RunProcReturn("login", prams, "tb_user", CommandType.StoredProcedure);
            if (null == dataset)
            {
                msg.Message = "连接数据库时，出现错误！";
            }
            if ("0".Equals(prams[3].Value.ToString()))
            {
                user.Id = int.Parse(dataset.Tables[0].Rows[0]["id"].ToString());
                user.Email = dataset.Tables[0].Rows[0]["email"].ToString();
                user.Phone = dataset.Tables[0].Rows[0]["phone"].ToString();

                msg.IsSucess = true;
            }
             msg.Message = prams[2].Value.ToString();


            return msg;
        }

        public int loginOut(User user)
        {
            SqlParameter[] prams = {
									    database.MakeInParam("@id",  SqlDbType.Int, 0, user.Id),
			};

           return database.RunProc("UPDATE tb_user SET isOnline = 0 WHERE id = @id", prams);
        }

        public MessageLocal updatePwd(User user)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, user.Id),
                                    database.MakeInParam("@password",  SqlDbType.VarChar, 20, user.Password),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 20),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            DataSet dataset = database.RunProcReturn("update_pwd_user", prams, "tb_user", CommandType.StoredProcedure);

            if ("0".Equals(prams[prams.Length - 1].Value.ToString()))
            {
                msg.IsSucess = true;
            }
            msg.Message = prams[prams.Length - 2].Value.ToString();

            return msg;
        }

    }
}
