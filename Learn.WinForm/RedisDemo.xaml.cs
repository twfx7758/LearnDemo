using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn.WinForm
{
    /// <summary>
    /// RedisDemo.xaml 的交互逻辑
    /// </summary>
    public partial class RedisDemo : Page
    {
        public RedisDemo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<UserInfo> list = new List<UserInfo>();
            list.Add(new UserInfo { UserId = 1, UserName = "wenbin1", Mobile = "13718312531" });
            list.Add(new UserInfo { UserId = 2, UserName = "wenbin2", Mobile = "13718312532" });
            list.Add(new UserInfo { UserId = 3, UserName = "wenbin3", Mobile = "13718312533" });
            list.Add(new UserInfo { UserId = 4, UserName = "wenbin4", Mobile = "13718312534" });
            //必须配置一台Redis服务器
            using (var client = RedisManager.GetClient())
            {
                client.Set<List<UserInfo>>("userinfolist", list);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var client = RedisManager.GetClient())
            {
                var list = client.Get<List<UserInfo>>("userinfolist");
                this.lblShowRedisVal.Content = list[0].UserName + "_" + list[1].UserName + "_" + list[2].UserName + "_" + list[3].UserName;
            }
        }

    }
    sealed class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
    }
}
