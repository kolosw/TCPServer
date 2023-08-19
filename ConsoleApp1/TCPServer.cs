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
    public class TCPServer
    {
        public static IPAddress address = IPAddress.Parse("127.0.0.1");
        public static int port = 8080;
        Thread server;
        public void start()
        {
            server = new Thread(TCPServer.listen);
            server.Start();
        }
        public static void listen()
        {
            TcpListener tcpListener = new TcpListener(address, port);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                var stream = tcpClient.GetStream();
                byte[] buffer = new byte[1024];
                stream.Read(buffer);
                string json = Encoding.UTF8.GetString(buffer);
                packet a = JsonConvert.DeserializeObject<packet>(json);
                a.str1 += "_CHANGED";
                a.ser *= 2;
                json = JsonConvert.SerializeObject(a);
                buffer = Encoding.UTF8.GetBytes(json);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
