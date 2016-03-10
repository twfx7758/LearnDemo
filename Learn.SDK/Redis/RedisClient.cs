using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.Redis
{
    public class RedisClient
    {
        public void RedisTest()
        {
            const int segment = 100;
            using (var client = RedisManager.GetClient())
            {
                //segment hash
                for (int i = 1; i <= 1000000; i++)
                {
                    string key = "f:" + (i % segment).ToString();
                    string strVal = "v:" + i.ToString();
                    string hashId = "0";
                    if ((i % segment) == 0)
                        hashId = ((i - 1) / segment).ToString();
                    client.SetEntryInHash(hashId, key, strVal);
                }

                //hash
                //string hashId = "testId";
                //for (int i = 1; i <= 1000000; i++)
                //{
                //    string key = "f:" + (i % segment).ToString();
                //    string strVal = "v:" + i.ToString();
                //    client.SetEntryInHash(hashId, key, strVal);
                //}

                //string key = "f:" + (i % segment).ToString();
                //Dictionary<string, string> dic = client.GetAllEntriesFromHash("0");
                // Console.WriteLine("i值{0}，val:{1}", dic.Keys., a);

                //List<string> list = client.GetHashValues((i / 100).ToString());
                //list.ForEach(a => { Console.WriteLine("i值{0}，val:{1}", i, a); });
                //Thread.Sleep(1000);

            }
        }
    }

    public sealed class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
    }
}
