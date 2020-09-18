using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataModel;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Seri;
using System.Timers;

namespace ExamDAL
{
    public class DataAccess
    {
        //数据访问层， 跟服务器对接

        #region 登入
        public static Student GetUser(Student ks, IPAddress ip, int port)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iep = new IPEndPoint(ip, port);
            byte[] bData = new SeriClass().SerializeBinary(ks).ToArray();
            udpClient.Send(bData, bData.Length, iep);

            while (true)
            {
                try
                {

                    byte[] bytes = udpClient.Receive(ref iep);
                    Student ks1 = new SeriClass().DeSerializeBinary(new MemoryStream(bytes)) as Student;
                    return ks1;
                   
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {

                    udpClient.Close();
                }

            }
        }
        #endregion

        #region 抽题
        public static List<TK> GetTiMu(Student ks,IPAddress ip, int port)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iep = new IPEndPoint(ip, port);          
            byte[] bData = new SeriClass().SerializeBinary(ks).ToArray();
            udpClient.Send(bData, bData.Length, iep);

            while (true)
            {
                try
                {
                    byte[] bytes = udpClient.Receive(ref iep);
                    List<TK> tm = new SeriClass().DeSerializeBinary(new MemoryStream(bytes)) as List<TK>;
                    return tm;
                }
                catch (Exception)
                {

                    return null;
                }
                finally
                {
                    udpClient.Close();
                }
            }
        }
        #endregion

        #region 交卷
        public static string UpResult(Student ks,IPAddress ip,int port)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iep = new IPEndPoint(ip,port);
            byte[] bData = new SeriClass().SerializeBinary(ks).ToArray();
            udpClient.Send(bData,bData.Length,iep);
            while (true)
            {
                try
                {
                    byte[] bytes = udpClient.Receive(ref iep);
                    string msg = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    return msg;
                }
                catch
                {
                    return null;
                }
                finally
                {
                    udpClient.Close();
                }
            }
        }
        #endregion
    }
}
