using System.Collections.Generic;
using System.Text;

namespace MysqlBatchAdd
{
    public abstract class BatchAddBase<T> where T : class, new()
    {
        /// <summary>
        /// 插入语句的头部
        /// </summary>
        protected abstract string InsertHead { get; }

        /// <summary>
        /// 出入语句的执行体
        /// </summary>
        protected List<string> InsertBodyList { get; set; } = new List<string>();
        /// <summary>
        /// 缓存的sql语句长度
        /// </summary>
        public int SqlCacheLengh { get; set; } = 1000 * 10;

        /// <summary>
        /// 批量添加的方法
        /// </summary>
        /// <param name="m"></param>
        public abstract void BatchAdd(T m);

        /// <summary>
        /// 执行添加
        /// </summary>
        public virtual void ExecuteBatchAdd()
        {
            StringBuilder sqlCache = new StringBuilder();

            foreach (string insertBody in InsertBodyList)
            {
                sqlCache.Append(insertBody + ",");

                if (sqlCache.Length >= SqlCacheLengh)
                {
                    sqlCache.Remove(sqlCache.Length - 1, 1);
                    MySqlHelper.ExecuteNonQuery(this.InsertHead + sqlCache.ToString());
                    sqlCache.Clear();
                }
            }

            if (sqlCache.Length > 0)
            {
                sqlCache.Remove(sqlCache.Length - 1, 1);
                MySqlHelper.ExecuteNonQuery(this.InsertHead + sqlCache.ToString());
                sqlCache.Clear();
            }
        }
        /// <summary>
        /// 清楚缓存
        /// </summary>
        public void ClearInsertBody()
        {
            this.InsertBodyList.Clear();
        }
    }
}