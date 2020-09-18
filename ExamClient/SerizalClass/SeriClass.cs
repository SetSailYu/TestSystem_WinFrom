using System;
using System.Collections.Generic;
using System.Text;
//
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Seri
{
    public class SeriClass
    {
        
        /// <summary>
        /// ��������ת���ɶ�����
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MemoryStream SerializeBinary(object request) //��������ת���ɶ�������
        {
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();    //����һ���ڴ����洢��
            serializer.Serialize(memStream, request);//���������л�Ϊ�ڴ�����
            return memStream;
        }
        /// <summary>
        /// ����������ת���ɶ���
        /// </summary>
        /// <param name="memStream"></param>
        /// <returns></returns>
        public object DeSerializeBinary(MemoryStream memStream) //����������ת���ɶ���
        {
            memStream.Position = 0;
            BinaryFormatter deserializer = new BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);//���ڴ��������л�Ϊ����
            memStream.Close();  //�ر��ڴ��������ͷ�
            return newobj;
        }
    }
}
