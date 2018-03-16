using System;
using System.Net;
using System.Net.Sockets;


    class Server
    {
        public static void Main()
        {
            Console.WriteLine("Hello World!");

            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("127.0.0.1");

            IPEndPoint ipEndpoint = new IPEndPoint(ip, 1234);

            listener.Bind(ipEndpoint);

            listener.Listen(0);

            Console.WriteLine("服务器连接开始");

            while(true)
            {
                Socket conn = listener.Accept();
                Console.WriteLine("客户端连接成功");

                byte[] readBuff = new byte[1024];

                int count = conn.Receive(readBuff);

                string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
                Console.WriteLine("客户端接收：" + str);

                byte[] bytes = System.Text.Encoding.Default.GetBytes("服务器回应：" + str);

                conn.Send(bytes);

            }
        }
    }

