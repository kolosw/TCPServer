using Newtonsoft.Json;
using Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCPApp
{
    public class TCPClient
    {
        static IPAddress address = IPAddress.Parse("127.0.0.1");
        static int port = 8080;
        public static packet Send(packet a, IPAddress ip,int port)
        {
            packet b = a;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            var stream = tcpClient.GetStream();
            string bytes = JsonConvert.SerializeObject(a);
            byte[] buffer = new byte[1024];
            buffer = Encoding.UTF8.GetBytes(bytes);
            stream.Write(buffer);
            buffer = new byte[1024];
            stream.Read(buffer);
            bytes = Encoding.UTF8.GetString(buffer);
            //Console.WriteLine(bytes);
            b = JsonConvert.DeserializeObject<packet>(bytes);
            b.str = a.str;
            b.nonser = a.nonser;
            return b;
        }
    }
}
