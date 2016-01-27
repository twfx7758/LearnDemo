using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Algorithm
{
    public class BaseAlgorithm
    {
        public static void Bubble_Sort(int[] arr)
        {
            for (int i = arr.Length; i > 1; i--)
            {
                for (int j = 0; j < i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }
                }
            }
        }
    }
}
