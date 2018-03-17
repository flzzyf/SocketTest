using System;
using System.Net;
using System.Net.Sockets;

class Client
{
    public const int BUFFER_SIZE = 1024;

    public byte[] readBuff = new byte[BUFFER_SIZE];

    public Socket socket;

    public void Connect(string _host, int _port)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.Connect(_host, _port);

        Util.Out("连接成功，客户端ip：" + socket.LocalEndPoint.ToString(), 1);
        Util.Out("现在可输入内容进行发送");

        socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCB, null);
    }

    void ReceiveCB(IAsyncResult _ar)
    {
        int count = socket.EndReceive(_ar);

        string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);

        Util.Out("接受信息：" + str);

        socket.BeginReceive(readBuff, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCB, null);
    }

    public void Send(string _str)
    {
        byte[] bytes = System.Text.Encoding.Default.GetBytes(_str);

        socket.Send(bytes);
    }

}