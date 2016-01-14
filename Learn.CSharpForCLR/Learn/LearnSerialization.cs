using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.Learn
{
    public class LearnSerialization
    {
        public static void Serialization1()
        {
            //创建一个对象图，以便把它们序列化到流中。
            List<String> objectGraph = new List<string> { "Jeff", "Kristin", "Aidan", "Grant" };
            Stream stream = SerializeToMemory(objectGraph);
            //为了演示，将一切重置。
            stream.Position = 0;

            objectGraph = null;

            //反序列化对象，证明它能工作。
            objectGraph = (List<String>)DeserializeFromMemory(stream);

            foreach (var s in objectGraph) Console.WriteLine(s);
        }

        //利用序列化深拷贝一个对象
        public static object DeepClone(Object original)
        {
            //构造一个临时内存流
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Context = new StreamingContext(StreamingContextStates.Clone);
                //将对象图序列化到内存流中
                formatter.Serialize(stream, original);
                //反序列化前，定位到内存流的起始位置
                stream.Position = 0;
                //将对象图反序列化成一组新对象，
                //并且向调用者返回对象图（深拷贝）的根
                return formatter.Deserialize(stream);
            }
        }

        private static MemoryStream SerializeToMemory(object objectGraph)
        { 
            //构造一个流来容纳序列化的对象
            MemoryStream stream = new MemoryStream();

            //构造一个序列化格式化器，它负责所有辛苦的工作
            BinaryFormatter formatter = new BinaryFormatter();

            //告诉格式化器将对象序列化到一个流中
            formatter.Serialize(stream, objectGraph);

            //将序列化好的对象流返回给调用者
            return stream;
        }

        private static Object DeserializeFromMemory(Stream stream)
        {
            //构造一个序列化格式化器来做所有辛苦的工作
            BinaryFormatter formatter = new BinaryFormatter();
            //告诉格式化器从流中反序列化对象
            return formatter.Deserialize(stream);
        }
    }
}
