using Learn.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.Learn
{
    public class AssemblyLoadReflect
    {
        /// <summary>
        /// 获取程序集下所有对外公开的类型
        /// </summary>
        public static void LoadAssemblyAndShowPublicTypes()
        {
            string assemId = "System.Data, version=4.0.0.0, culture=neutral, PublicKeyToken=b77a5c561934e089";
            Assembly a = Assembly.Load(assemId);
            foreach (Type t in a.GetExportedTypes())
            {
                Console.WriteLine(t.FullName);
            }
        }

        /// <summary>
        /// 加载第三方程序集
        /// </summary>
        public static void LoadThirdSdk()
        {
            String AddInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            //假定加载项程序集和宿住EXE文件在同一个目录
            String[] AddInAssemblies = Directory.GetFiles(AddInDir, "*.dll");
            //创建所有可用的加载项Type的一个集合
            List<Type> AddInTypes = new List<Type>();
            //加载加载项程序集,查看哪些类型可以由宿主使用
            foreach (String file in AddInAssemblies)
            {
                Assembly AddInAssembly = Assembly.LoadFrom(file);
                foreach (Type t in AddInAssembly.GetExportedTypes())
                {
                    if (t.IsClass && typeof(IAddIn).IsAssignableFrom(t))
                    {
                        AddInTypes.Add(t);
                    }
                }
            }

            //初始化完成，宿主已发现了所有可用的加载项
            //下面示范宿主如何构造加载项对象并使用它们
            foreach (Type t in AddInTypes)
            {
                IAddIn ai = (IAddIn)Activator.CreateInstance(t);
                Console.WriteLine(ai.DoSomething(5));
            }
        }

        /// <summary>
        /// 发现类型成员
        /// </summary>
        public static void GetMemberInfos()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
