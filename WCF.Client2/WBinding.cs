using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WCF.Client2
{
    public class WBinding
    {
        public static void BindMain()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            ListAllBindingElements(binding);
            Console.WriteLine("-------------------------------------------------------");
            WebHttpBinding binding2 = new WebHttpBinding();//支持事务流转
            ListAllBindingElements(binding2);
        }

        static void ListAllBindingElements(Binding binding)
        {
            BindingElementCollection elements = binding.CreateBindingElements();
            for (int i = 0; i < elements.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, elements[i].GetType().FullName);
            }
        }
    }
}
