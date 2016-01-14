using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.DesignPattern.Factory
{
    //基类
    public class Namer
    {
        protected string frName, lName;

        public string GetFrname() {
            return frName;
        }

        public string GetLname() {
            return lName;
        }
    }

    //继承的子类
    public class FirstFirst : Namer {
        public FirstFirst(string name) {
            int i = name.Trim().IndexOf(" ");
            if (i > 0)
            {
                frName = name.Substring(0, i).Trim();
                lName = name.Substring(i + 1).Trim();
            }
            else {
                lName = name;
                frName = "";
            }
        }
    }

    //继承的子类
    public class LastFirst : Namer
    {
        public LastFirst(string name)
        {
            int i = name.Trim().IndexOf(",");
            if (i > 0)
            {
                frName = name.Substring(0, i).Trim();
                lName = name.Substring(i + 1).Trim();
            }
            else
            {
                lName = name;
                frName = "";
            }
        }
    }

    //构建一个简单的工厂类
    public class NameFactory
    {
        public NameFactory() { }

        public Namer GetName(string name) {
            int i = name.IndexOf(",");
            if (i > 0)
                return new LastFirst(name);
            else
                return new FirstFirst(name);
        }
    }
}
