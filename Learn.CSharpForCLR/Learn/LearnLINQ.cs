using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.Learn
{
    public class LearnLINQ
    {
        public static void Test1()
        {
            int[] scores = { 90, 71, 82, 93, 75, 82 };
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                orderby score descending
                select score;

            int maxVal = scoreQuery.Max();

            Console.WriteLine(maxVal);

            foreach (int testScore in scoreQuery)
            {
                Console.WriteLine(testScore);
            }
        }

        public static void JoinTest()
        { 
        }

        public static void LetTest()
        {
            string[] names = { "Svetlana Omelchenko", "Claire O'Donnell", "Sven Mortensen", "Cesar Garcia" };
            IEnumerable<string> queryFirstNames =
                from name in names
                let firstName = name.Split(new char[] { ' ' })[0]
                select firstName;
            foreach (string s in queryFirstNames)
                Console.Write(s + " ");
        }
    }
}
