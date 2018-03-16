using System;
using System.Net;
using System.Net.Sockets;

namespace SocketProject
{
    class Client
    {
        public static void Main()
        {
            const int BUFFER_SIZE = 1024;

            IPAddress ip = IPAddress.Parse("127.0.0.1");

            IPEndPoint ipEndpoint = new IPEndPoint(ip, 1234);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipEndpoint);

            Console.WriteLine(ipEndpoint.ToString());

            string str = "Lemon";

            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            socket.Send(bytes);
            byte[] readBuff = new byte[BUFFER_SIZE];
            int count = socket.Receive(readBuff);

            str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);

            Console.WriteLine(str);

            socket.Close();
        }
    }
}