using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebAppSimulator.Models
{
    public class Client
    {
        private IPEndPoint ep;
        private TcpClient client;
        public Client()
        {
        }

        public void Connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.client = new TcpClient();
            this.client.Connect(ep);
        }

        public double GetMessage(string Data)
        {
            string Message;
            if (this.client == null)
            {
                //TODO: make sure the client connect before.
                return -1;
            }
            NetworkStream stream = this.client.GetStream();
            StreamReader reader = new StreamReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            {
                writer.Write(Data.ToArray());
                Message = reader.ReadLine();
            }
            string a = Message;
            double result = ExtractInt(Message);
            return result;
        }

        private double ExtractInt(string message)
        {
            int minus = 0;
            string b = string.Empty;
            double val = 0;

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == '-' && Char.IsDigit(message[i + 1]))
                {
                    minus = 1;
                }
                if (Char.IsDigit(message[i]) || message[i] == '.')
                {
                    b += message[i];
                }
            }

            if (b.Length > 0)
                val = double.Parse(b);
            if (minus == 1)
            {
                val = -val;
            }
            return val;
        }

        public void SendToServer(string Message)
        {
            if (this.client == null)
            {
                //TODO: make sure the client connect before.
                return;
            }
            NetworkStream stream = this.client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            {
                writer.Write(Message.ToArray());
            }

        }

        public void Close()
        {
            client?.Close();
        }
    }
}