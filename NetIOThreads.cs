using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;

namespace StreamTunnel
{
    public class NetIOThreads
    {
        public NetIOThreads()
        {
        }
        public void reqIO(object trans)
        {
            byte[] bufe = new byte[2048];
            Transformer tran = (Transformer)trans;
            try
            {
                /*
                using (BinaryReader reader = new BinaryReader(tran.client.GetStream()))
                {
                    using (BinaryWriter writer = new BinaryWriter(tran.target.GetStream()))
                    {
                        while ((bufe = reader.ReadBytes(1)).Length > 0)
                        {
                            writer.Write(bufe);
                        }
                    }
                }
                */
                do
                {
                    int buffsize = tran.client.GetStream().Read(bufe, 0, 2048);
                    if (buffsize > 0)
                    {
                        tran.target.GetStream().Write(bufe, 0, buffsize);
                    }
                } while (tran.client.Connected && tran.target.Connected);
            }
            catch (Exception)
            {
                if (!tran.client.Connected)
                {
                    tran.target.Close();
                }
                if (!tran.target.Connected)
                {
                    tran.client.Close();
                }
            }
            if (!tran.client.Connected)
            {
                tran.target.Close();
            }
            if (!tran.target.Connected)
            {
                tran.client.Close();
            }
        }
        public void respIO(object trans)
        {
            byte[] bufe = new byte[2048];
            Transformer tran = (Transformer)trans;
            try
            {
                /*
                using (BinaryReader reader = new BinaryReader(tran.target.GetStream()))
                {
                    using (BinaryWriter writer = new BinaryWriter(tran.client.GetStream()))
                    {
                        while ((bufe = reader.ReadBytes(1)).Length > 0)
                        {
                            writer.Write(bufe);
                        }
                    }
                }
                */
                do
                {
                    int buffsize = tran.target.GetStream().Read(bufe, 0, 2048);
                    if (buffsize > 0)
                    {
                        tran.client.GetStream().Write(bufe, 0, buffsize);
                    }
                } while (tran.client.Connected && tran.target.Connected);
            }
            catch (Exception)
            {
                if (!tran.client.Connected)
                {
                    tran.target.Close();
                }
                if (!tran.target.Connected)
                {
                    tran.client.Close();
                }
            }
            if (!tran.client.Connected)
            {
                tran.target.Close();
            }
            if (!tran.target.Connected)
            {
                tran.client.Close();
            }
        }
    }
    public class Transformer
    {
        public TcpClient client;
        public TcpClient target;
        public Transformer(TcpClient cl, TcpClient tg)
        {
            client = cl;
            target = tg;
        }
    }
}
