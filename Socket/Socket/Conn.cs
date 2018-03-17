using System;
using System.Net;
using System.Net.Sockets;

public class Conn
{
    public const int BUFFER_SIZE = 1024;

    public Socket socket;

    public bool inUse = false;

    public byte[] readBuff = new byte[BUFFER_SIZE];

    public int buffCount = 0;

    public Conn()
    {
        readBuff = new byte[BUFFER_SIZE];

    }

    public void Init(Socket _socket)
    {
        this.socket = _socket;
        inUse = true;
        buffCount = 0;
    }

    public int BuffRemain()
    {
        return BUFFER_SIZE - buffCount;
    }

    public string GetAddress()
    {
        if (!inUse)
            return "尚未连接";

        return socket.RemoteEndPoint.ToString();
    }

    public void Close()
    {
        if (inUse)
        {
            Console.WriteLine("断开连接：" + GetAddress());
            socket.Close();
            inUse = false;
        }

    }
}
