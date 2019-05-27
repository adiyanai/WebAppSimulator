using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAppSimulator.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;

        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }

        public string ip { get; set; }
        public string port { get; set; }

        public InfoModel()
        {
        }

        public const string SCENARIO_FILE = "~/App_Data/{0}.txt";           // The Path of the Secnario

        public void ReadData(string ip, int port)
        {
           
        }

    }
}