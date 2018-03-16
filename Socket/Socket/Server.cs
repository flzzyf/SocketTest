using System;
using System.Net;
using System.Net.Sockets;

namespace SocketProject
{
    class Server
    {
        static Socket listener;

        public Conn[] conns;

        public int maxConn = 10;

        public static void Main()
        {
            Console.WriteLine("Hello World!");

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ip = IPAddress.Parse("127.0.0.1");

            IPEndPoint ipEndpoint = new IPEndPoint(ip, 1234);

            listener.Bind(ipEndpoint);

            listener.Listen(0);

            Console.WriteLine("服务器连接开始，等待客户端接入");

            while (true)
            {
                //Socket conn = listener.BeginAccept(Accepts, null);
                Console.WriteLine("客户端连接成功");

                byte[] readBuff = new byte[1024];

                //int count = conn.Receive(readBuff);

                string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, 0);
                Console.WriteLine("从客户端接收的信息：" + str);

                str = System.DateTime.Now.ToString();
                byte[] bytes = System.Text.Encoding.Default.GetBytes("服务器回应信息：" + str);

                //conn.Send(bytes);

            }
        }

        void Accepts(IAsyncResult _ar)
        {
            Socket socket = listener.EndAccept(_ar);
        }
    }
}
    

