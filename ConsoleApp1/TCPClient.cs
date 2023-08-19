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
        static IPAddress address = IPAddress.Parse("127.0.0.1");    //Используемый ip адрес для передачи
        static int port = 8080;    //Испоользуемый порт для передачи
        public static packet Send(packet a, IPAddress ip,int port)
        {
            packet b = a;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            var stream = tcpClient.GetStream();    //Получение потока для передачи
            string bytes = JsonConvert.SerializeObject(a);     //Сериализация объекта в json без 2 несериализуемых свойств
            byte[] buffer = new byte[1024];    
            buffer = Encoding.UTF8.GetBytes(bytes);    //Сериализация json в поток байт
            stream.Write(buffer);   //Отправка потока байт на сервер
            buffer = new byte[1024];
            stream.Read(buffer);    //Получение измененного потока байт от сервера
            bytes = Encoding.UTF8.GetString(buffer);    //Десериализация потока байт в json
            //Console.WriteLine(bytes);
            b = JsonConvert.DeserializeObject<packet>(bytes); //Десериализация json в объект
            b.str = a.str;    //Возвращение несериализуемых свойст обратно в объект
            b.nonser = a.nonser;
            return b;
        }
    }
}
