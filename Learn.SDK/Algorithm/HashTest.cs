using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Learn.SDK
{
    public class HashTest
    {
        public Dictionary<int, string> DictionaryTst()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "first");
            dic.Add(2, "second");
            dic.Add(3, "thrid");

            dic.Remove(2);

            dic.Add(4, "four");

            return dic;
        }

        public Hashtable HashtableTest()
        {
            Hashtable ht = new Hashtable();
            ht.Add(1, "first");
            ht.Add(2, "second");
            ht.Add(3, "thrid");
            return ht;
        }

        public HashSet<string> HashSet()
        {
            HashSet<string> set = new System.Collections.Generic.HashSet<string>();
            set.Add("wenbin1");
            set.Add("wenbin2");
            set.Add("wenbin3");
            set.Add("wenbin4");
            return set;
        }
    }
}
