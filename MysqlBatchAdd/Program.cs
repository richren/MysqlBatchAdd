using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysqlBatchAdd
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonAddHelper personAddHelper = new PersonAddHelper();

            Stopwatch watch = new Stopwatch();

            watch.Start();

            int amount = 100000;

            for (int i = 1; i <= amount; i++)
            {
                personAddHelper.BatchAdd(new Person() { name = i + "号" });
            }

            personAddHelper.ExecuteBatchAdd();

            watch.Stop();

            Console.WriteLine($"成功插入 {amount} 条数据，用时：{watch.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }
    }
}
