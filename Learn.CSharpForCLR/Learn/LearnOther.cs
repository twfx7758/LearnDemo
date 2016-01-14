using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.Learn
{
    public class LearnOther
    {
        //引用传递实参
        public static void ModifyParam(string username)
        {
            username = "test2";
        }
    }

    public class Person
    {
        public string name { get; set; }
    }
}
