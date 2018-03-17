using System;
using System.Net;
using System.Net.Sockets;

public class Server
{
    static Socket listener;

    public Conn[] conns;

    public int maxConnNumber = 10;

    public int GetConn()
    {
        if (conns == null)
            return -1;

        for (int i = 0; i < conns.Length; i++)
        {
            if (conns[i] == null)
            {
                conns[i] = new Conn();
                return i;
            }
            else if (!conns[i].inUse)
            {
                return i;
            }
        }

        return -1;
    }

    public void Init(string _host, int _port)
    {
        conns = new Conn[maxConnNumber];

        for (int i = 0; i < maxConnNumber; i++)
        {
            conns[i] = new Conn();
        }

        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPAddress ip = IPAddress.Parse(_host);

        IPEndPoint ipEndpoint = new IPEndPoint(ip, _port);

        listener.Bind(ipEndpoint);

        listener.Listen(maxConnNumber);

        listener.BeginAccept(AcceptCB, null);

        Console.WriteLine("服务器连接开始，等待客户端接入...", 1);

    }

    //客户端接入后
    void AcceptCB(IAsyncResult _ar)
    {
        int index = GetConn();

        if(index < 0){
            Util.Out("未能取得可用连接", 1);
            return;
        }

        Socket socket = listener.EndAccept(_ar);
        Conn conn = conns[index];
        conn.Init(socket);

        string ip = conn.GetAddress();
        Util.Out("客户连接成功，ID：" + ip, 1);

        conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCB, conn);

        listener.BeginAccept(AcceptCB, null);
    }

    void ReceiveCB(IAsyncResult _ar)
    {
        Conn conn = (Conn)_ar.AsyncState;

        int count = conn.socket.EndReceive(_ar);

        if(count <= 0)
        {
            Util.Out("客户" + conn.GetAddress() + "断开连接", 1);
            conn.Close();
            return;
        }

        string str = System.Text.Encoding.UTF8.GetString(conn.readBuff, 0, count);

        Util.Out("收到" + conn.GetAddress() + "的消息：" + str, 1);

        str = conn.GetAddress() + "：" + str;

        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);



        foreach (Conn c in conns)
        {
            if (c == null || !c.inUse)
                continue;
            
            Util.Out("将消息广播给" + c.GetAddress());
            c.socket.Send(bytes);
        }

        conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCB, conn);


    }
}
    

