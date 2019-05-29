using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppSimulator.Models
{
    public sealed class CommandModel
    {
        private readonly Client Client = new Client();
        private static readonly CommandModel instance = new CommandModel();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static CommandModel()
        {
        }

        private CommandModel()
        {
        }

        public static CommandModel Instance
        {
            get
            {
                return instance;
            }
        }

        public void Connect(string ip, int port)
        {
            Client.Connect(ip, port);
        }

        public double GetData(string Data)
        {
            return Client.GetMessage(Data);
        }

        public void SendMessage(string Message)
        {
            Client.SendToServer(Message);
        }

        public void Close()
        {
            Client.Close();
        }
    }
}