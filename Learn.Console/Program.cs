﻿using Learn.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.ConsolePlat
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.ReadLine();
        }

        //冒泡算法测试
        static void TestMethod()
        {
            int[] x = { 6, 2, 4, 1, 5, 9 };
            BaseAlgorithm.Bubble_Sort(x);
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }
        }
    }
}