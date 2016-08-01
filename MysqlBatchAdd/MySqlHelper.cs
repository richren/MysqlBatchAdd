using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MysqlBatchAdd
{
    public static class MySqlHelper
    {
        #region 连接字符串
        static string connString = "server=127.0.0.1;user id=root;password=root;database=renjing;";

        /// <summary>
        /// 返回配置文件中指定的连接（已经打开连接）
        /// </summary>
        /// <returns>配置文件中指定的连接</returns>
        private static MySqlConnection GetConnection()
        {
            MySqlConnection connect = new MySqlConnection(connString);
            connect.Open();
            return connect;
        } 
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="sql">带参数的sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                return ExecuteNonQuery(conn, sql, paras);
            }
        }

        /// <summary>
        /// 根据给定连接，执行带参数的SQL语句
        /// </summary>
        /// <param name="conn">连接、使用前确保连接以打开。</param>
        /// <param name="sql">带参数的sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(MySqlConnection conn, string sql, params MySqlParameter[] paras)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句，返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>第一行第一列</returns>
        public static Object ExecuteScalar(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行带参数的sql语句，返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar(string sql, MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                return ExecuteScalar(conn, sql, paras);
            }
        }

        /// <summary>
        /// 根据Connection对象，执行带参数的sql语句，返回第一行第一列
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar(MySqlConnection conn, string sql, MySqlParameter[] paras)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteScalar();
            }
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行sql语句，返回一个MySqlDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>一个MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(string sql)
        {
            MySqlConnection conn = GetConnection();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                //conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }

        /// <summary>
        /// 执行带参数的sql语句，返回一个Reader对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>一个MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(string sql, params MySqlParameter[] paras)
        {
            MySqlConnection conn = GetConnection();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                return ExecuteReader(conn, sql, paras);
            }
        }

        /// <summary>
        /// 根据指定的连接，执行带参数的sql语句，返回一个Reader对象
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>一个MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(MySqlConnection conn, string sql, params MySqlParameter[] paras)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                //conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        #endregion

        #region ExecuteTable
        /// <summary>
        /// 执行sql语句，返回一个DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
        }

        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable(string sql, params MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                return ExecuteTable(conn, sql, paras);
            }
        }

        /// <summary>
        /// 根据连接，执行带参数的sql语句，返回一个DataTable
        /// </summary>
        /// <param name="conn">连接，切记连接已打开</param>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable(MySqlConnection conn, string sql, params MySqlParameter[] paras)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                string str = cmd.ToString();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
        }
        #endregion
    }
}
