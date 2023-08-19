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
                TcpClient tcpClient = tcpListener.AcceptTcpClient();    //Создание листенера
                var stream = tcpClient.GetStream();
                byte[] buffer = new byte[1024];
                stream.Read(buffer);    //Чтение потока байт от клиента
                string json = Encoding.UTF8.GetString(buffer);   //Десериализация из потока байт в json
                packet a = JsonConvert.DeserializeObject<packet>(json);    //Десериализация из json в объект
                a.str1 += "_CHANGED";   //Изменение свойств пакета
                a.ser *= 2;
                json = JsonConvert.SerializeObject(a);   //Сериализация пакета в json
                buffer = Encoding.UTF8.GetBytes(json);   //Сериализцаия json в поток байт
                stream.Write(buffer, 0, buffer.Length);  //Отправка измененного пакета клиенту
            }
        }
    }
}
