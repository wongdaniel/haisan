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
    class UserDaoImpl: CommonDaoImpl, UserDao
    {
        private static Database database = Database.getInstance();
        private static BaseDao baseDao = BaseDaoImpl.getInstance();

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

        public DataSet getAllUser(string queryStr)
        {
            string sql = "SELECT tu.id, tu.username, tu.password, tg.name AS group_name, tu.name, tu.email, tu.phone, tu.description, tu.isLock, tu.lastLoginTime, tu.isOnline, tu.createTime " +  
                            "FROM tb_user AS tu, tb_group AS tg WHERE " + 
                            "(tu.username LIKE @queryStr OR tu.name LIKE @queryStr) AND tu.[group] = tg.id";

            SqlParameter[] prams = {
                                    database.MakeInParam("@queryStr", SqlDbType.NVarChar, 20, "%" + queryStr + "%")
			};

            return database.RunProcReturn(sql, prams, "tb_user");
        }

        public MessageLocal saveOrUpdateUser(User user)
        {
            MessageLocal msg = new MessageLocal();
            SqlParameter[] prams = constructParamsForUser(user);

            return baseDao.runProcedure("saveOrUpdate_user", prams, "user");
        }

        public User getUserById(int id)
        {
            User user = new User(id);
            SqlParameter[] prams = {
                                    database.MakeInParam("@id", SqlDbType.Int, 0, id)
			};
            try
            {
                DataSet dataset = database.RunProcReturn("get_userById", prams, "tb_user", CommandType.StoredProcedure);
                if (null != dataset && dataset.Tables[0].Rows.Count > 0)
                {
                    user.Username = getValue(dataset, "username");
                    user.Password = getValue(dataset, "password");
                    user.Name = getValue(dataset, "name");
                    user.Email = getValue(dataset, "email");
                    user.Phone = getValue(dataset, "phone");
                    user.IsLock = bool.Parse(getValue(dataset, "isLock"));
                    user.Group = new Group(int.Parse(getValue(dataset, "group")), getValue(dataset, "group_name"));
                    user.Description = getValue(dataset, "description");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return user;
        }

        SqlParameter[] constructParamsForUser(User user)
        {
            MessageLocal msg = new MessageLocal();

            SqlParameter[] prams = {
									database.MakeInParam("@id",  SqlDbType.Int, 0, user.Id),
                                    database.MakeInParam("@username",  SqlDbType.VarChar, 20, user.Username),
                                    database.MakeInParam("@password",  SqlDbType.VarChar, 20, user.Password),
                                    database.MakeInParam("@group",  SqlDbType.Int, 0, user.Group.Id),
                                    database.MakeInParam("@name",  SqlDbType.NVarChar, 20, user.Name),
                                    database.MakeInParam("@email",  SqlDbType.VarChar, 50, user.Email),
                                    database.MakeInParam("@phone",  SqlDbType.VarChar, 20, user.Phone),
                                    database.MakeInParam("@description",  SqlDbType.NVarChar, 100, user.Description),
                                    database.MakeInParam("@isLock",  SqlDbType.Bit, 0, user.IsLock),
                                    database.MakeInParam("@isOnline",SqlDbType.Bit, 0, user.IsOnline),
                                    database.MakeInParam("@createTime",SqlDbType.DateTime, 0, DateTime.Now),
                                    new SqlParameter("@message", SqlDbType.NVarChar, 50),
                                    new SqlParameter("rval", SqlDbType.Int, 4) 
			};
            prams[prams.Length - 2].Direction = ParameterDirection.Output;
            prams[prams.Length - 1].Direction = ParameterDirection.ReturnValue;

            return prams;
        }


    }
}
