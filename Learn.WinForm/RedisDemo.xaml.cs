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
            //必须配置一台Redis服务器
            using (var client = RedisManager.GetClient()) {
                client.SetValue("city", this.txtRedisVal.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var client = RedisManager.GetClient())
            {
                this.lblShowRedisVal.Content = client.Get<string>("city");
            }
        }
    }
}
