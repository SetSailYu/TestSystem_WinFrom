using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Seri
{
    public class SeriClass
    {
        /// <summary>
        /// 将对象流转换成二进制
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MemoryStream SerializeBinary(object request) //将对象流转换成二进制流
        {
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();    //创建一个内存流存储区
            serializer.Serialize(memStream, request);//将对象序列化为内存流中
            return memStream;
        }
        /// <summary>
        /// 将二进制流转换成对象
        /// </summary>
        /// <param name="memStream"></param>
        /// <returns></returns>
        public object DeSerializeBinary(MemoryStream memStream) //将二进制流转换成对象
        {
            memStream.Position = 0;
            BinaryFormatter deserializer = new BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);//将内存流反序列化为对象
            memStream.Close();  //关闭内存流，并释放
            return newobj;
        }
    }
}
