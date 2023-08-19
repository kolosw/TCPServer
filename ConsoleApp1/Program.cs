using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Packet;
using System.Numerics;
//namespace Packet  //Используется такая структура в качестве пакета
//{
//    [Serializable]
//    public struct packet
//    {
//        public int ser { get; set; }

//        [JsonIgnore]
//        public int nonser { get; set; }
//        [JsonIgnore]
//        public string str { get; set; }

//        public string str1 { get; set; }
//    }
//}
namespace TCPApp
{


    class test
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  //Поддержка кириллицы в консоли
            Console.OutputEncoding = Encoding.GetEncoding(866);
            Console.InputEncoding = Encoding.GetEncoding(866);
            TCPServer server = new TCPServer();
            bool ServerIsUp = false;
            while (true)
            {
                Console.WriteLine("1.Запустить сервер 2.Отправить пакет 0.Закрыть приложение");
                String choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            if (!ServerIsUp)
                            {
                                server.start();    //Запуск сервера
                                ServerIsUp = true;
                            }
                            else
                                Console.WriteLine("Сервер уже запущен");
                            break;
                        }
                    case "2":
                        if (ServerIsUp)
                        {
                            packet b = TCPClient.Send(CreatePacket(),TCPServer.address,TCPServer.port);    //Создание и отправка покета на сервер
                            Console.WriteLine();
                            Console.WriteLine("Полученный пакет:");
                            Console.WriteLine("Сериализуемое число:" + b.ser + " Сериализуемая строка:" + b.str1 +
                                " Не сериализуемое число:" + b.nonser + " Не сериализуемая строка:" + b.str);
                        }
                        else
                            Console.WriteLine("Сервер не запущен");
                        break;
                    case "0":
                        {
                            Environment.Exit(0);
                            return;
                        }
                    default: Console.WriteLine("Некоректный ввод"); break; ;
                }
            }
        }
        public static packet CreatePacket()    //Создание и заполнение пакета
        {
            
            packet pack = new packet();
            Console.WriteLine("Ввведите строку которая будет сериализована");
            pack.str1 = Console.ReadLine();
            Console.WriteLine("Ввведите строку которая не будет сериализована");
            pack.str = Console.ReadLine();
            Console.WriteLine("Ввведите число которое не будет сериализовано");
            pack.nonser = int.Parse(Console.ReadLine());
            Console.WriteLine("Ввведите число которое будет сериализовано");
            pack.ser = int.Parse(Console.ReadLine());
            packet b = pack;
            Console.WriteLine("Исходный пакет:");
            Console.WriteLine("Сериализуемое число:" + b.ser + " Сериализуемая строка:" + b.str1 +
                                " Не сериализуемое число:" + b.nonser + " Не сериализуемая строка:" + b.str);
            return pack;
        }
    }
}