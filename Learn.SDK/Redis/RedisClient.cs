using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.SDK.Redis
{
    public class RedisClient
    {
        public void RedisTest()
        {
            //List<UserInfo> list = new List<UserInfo>();
            //list.Add(new UserInfo { UserId = 1, UserName = "wenbin1", Mobile = "13718312531" });
            //list.Add(new UserInfo { UserId = 2, UserName = "wenbin2", Mobile = "13718312532" });
            //list.Add(new UserInfo { UserId = 3, UserName = "wenbin3", Mobile = "13718312533" });
            //list.Add(new UserInfo { UserId = 4, UserName = "wenbin4", Mobile = "13718312534" });
            ////必须配置一台Redis服务器
            //using (var client = RedisManager.GetClient())
            //{
            //    client.Set<List<UserInfo>>("userinfolist", list);
            //}
            Random rm = new Random();
            int iVal = 0;
            using (var client = RedisManager.GetClient())
            {
                for (int i = 0; i < 1000000; i++)
                {
                    iVal = rm.Next(1, 20000);
                    client.Set<int>(i.ToString(), i);
                }
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
