using System;

public class MainClass
{
    static string ip = "127.0.0.1";
    //static string ip = "192.168.31.195";
    //static string ip = "192.168.31.182";
    static int port = 7777;

    public static void Main()
    {
        Util.Out("输入0开启服务器，其他开启客户端连接：");
        string str = Console.ReadLine();

        if(str == "0")
        {
            //服务端
            Server server = new Server();

            server.Init(ip, port);

            while (true)
            {
                str = Console.ReadLine();

                if (str == "quit")
                {
                    return;

                }
            }
        }
        else
        {
            //客户端
            Client client = new Client();

            client.Connect(ip, port);

            while (true)
            {
                str = Console.ReadLine();

                if (str == "quit")
                {
                    client.socket.Close();
                    return;
                }
                else
                    client.Send(str);
            }
        }



    }

}
