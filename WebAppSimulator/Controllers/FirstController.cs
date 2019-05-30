using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSimulator.Models;

namespace WebAppSimulator.Controllers

{
    public class FirstController : Controller

    {

        // GET: Display
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, int port)
        {

            CommandModel.Instance.Connect(ip, port);
            ViewBag.Lon = CommandModel.Instance.GetData("get position/longitude-deg \r\n");
            ViewBag.Lat = CommandModel.Instance.GetData("get position/latitude-deg \r\n");
            //CommandModel.Instance.Close();
            return View();
        }

        /*[HttpGet]
        public ActionResult display(string ip, int port, int times_per_second)
        {
            CommandModel.Instance.Connect(ip, port);
            while(true)
            {
                //display(ip, port);
                System.Threading.Thread.Sleep(times_per_second * 1000);
            }
        }*/

        // save to file the data in this format: (position) lon, lat, rudder, speed
        public void WriteToFile(string file_name, string info)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\" + file_name + ".txt";
            // if the file doesn't exist, create the file and write the info to the file
            if (!(System.IO.File.Exists(path)))
            {
                System.IO.File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine(info);
                tw.Close();
            }
            // if the file already exist, write the info to the file
            else
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(info);
                    tw.Close();
                }
            }
        }

        // todo - time_per_second!!!
        [HttpGet]
        public ActionResult save(string ip, int port, int times_per_second, int seconds, string file_name)
        {
            CommandModel.Instance.Connect(ip, port);
            double lon, lat, rudder, speed;
            lon = CommandModel.Instance.GetData("get position/longitude-deg \r\n");
            lat = CommandModel.Instance.GetData("get position/latitude-deg \r\n");
            rudder = CommandModel.Instance.GetData("get controls/flight/rudder \r\n");
            speed = CommandModel.Instance.GetData("get instrumentation/airspeed-indicator/indicated-speed-kt \r\n");
            ViewBag.Lon = lon;
            ViewBag.Lat = lat;
            
            // string info format: (position) lon, lat, rudder, speed
            string info = lon.ToString() + "," + lat.ToString() + "," + rudder.ToString() + "," + speed.ToString();
            WriteToFile(file_name, info);

            return View();
        }

        // todo - check how to check if the string is ip, and than to merge this function with the first one
        // time_per_second!!!
        [HttpGet]
        public ActionResult Display(string file_name, int times_per_second)
        {
            string path, line;
            path = AppDomain.CurrentDomain.BaseDirectory + @"\" + file_name + ".txt";

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                string[] split_info = line.Split(',');
                ViewBag.Lon = Convert.ToDouble(split_info[0]);
                ViewBag.Lat = Convert.ToDouble(split_info[1]);
            }

            file.Close();
            //CommandModel.Instance.Close();
            return View();
        }
    }
}