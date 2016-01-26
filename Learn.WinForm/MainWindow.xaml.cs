﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Label5.Content = string.Format("UI线程：{0}", Thread.CurrentThread.ManagedThreadId);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = string.Format("线程：{0}", Thread.CurrentThread.ManagedThreadId);
            string name = await Processing();
            Label4.Content = string.Format("线程：{0},返回内容：{1}", Thread.CurrentThread.ManagedThreadId, name);
            MessageBox.Show("Button's event completed");
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button2's event completed");
        }

        public async Task<string> Processing()
        {
            Label2.Content = string.Format("线程：{0}", Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() =>
            {
                Task.Delay(5000);
            });
            Label3.Content = string.Format("线程：{0}", Thread.CurrentThread.ManagedThreadId);

            return "quwenbin";
        }
    }
}
