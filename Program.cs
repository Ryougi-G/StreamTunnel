using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

namespace StreamTunnel
{
    class Program
    {
        static void Main(string[] args)
        {
            RegOperation reg = new RegOperation();
            string[] paras = File.ReadAllLines("networkconfig.txt");
            if (paras[3] == "true")
            {
                if (reg.GetAutoStartUpRegKeyValue("Win32Networks") == null)
                {
                    reg.SetAutoStartUpReg(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, "Win32Networks");
                }
            }
            TcpListener server = new TcpListener(IPAddress.Any, Convert.ToInt32(paras[0]));
            server.Start(10000);
            while (true)
            {
                TcpClient cl = server.AcceptTcpClient();
                IPEndPoint ip = (IPEndPoint)cl.Client.RemoteEndPoint;
                Console.WriteLine(ip.Address.ToString());
                TcpClient target = new TcpClient(paras[1], Convert.ToInt32(paras[2]));
                Transformer tran = new Transformer(cl, target);
                NetIOThreads nit = new NetIOThreads();
                Thread red = new Thread(new ParameterizedThreadStart(nit.reqIO));
                Thread wri = new Thread(new ParameterizedThreadStart(nit.respIO));
                red.Start(tran);
                wri.Start(tran);
            }
        }
    }
}
