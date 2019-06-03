using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSimulator.Models;
using System.Net;

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
        public ActionResult display(string ip, int port, int times_per_second = -1)
        {
            IPAddress ipAddress;
            bool is_ip = IPAddress.TryParse(ip, out ipAddress);

            // close the last connection
            CommandModel.Instance.Close();

            // if there is ip, connect
            if (is_ip)
            {
                // connect again
                CommandModel.Instance.Connect(ip, port);
                // draw only point
                if (times_per_second == -1)
                {
                    ViewBag.drawPath = false;
                }
                // draw the airplane path
                else
                {
                    ViewBag.drawPath = true;
                }
                ViewBag.samplingRate = times_per_second;
                ViewBag.mode = "simulator";
            }
            // if there is no ip, read from file
            else
            {
                ViewBag.drawPath = true;
                ViewBag.fileName = ip;
                ViewBag.samplingRate = port;
                ViewBag.mode = "readFromFile";
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetLonLat()
        {
            double rudder, throttle, lon, lat;
            // get the required info
            lon = CommandModel.Instance.GetData("get position/longitude-deg \r\n");
            lat = CommandModel.Instance.GetData("get position/latitude-deg \r\n");
            rudder = CommandModel.Instance.GetData("get controls/flight/rudder \r\n");
            throttle = CommandModel.Instance.GetData("get /controls/engines/current-engine/throttle \r\n");

            FlightData info = new FlightData
            {
                Lon = lon,
                Lat = lat,
                Rudder = rudder,
                Throttle = throttle
            };

            return Json(info, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult WriteToFile(string file_name, string info)
        {
            string path = "~/" + file_name + ".json";

            // save the data to the file
            using (StreamWriter writer = new StreamWriter(Server.MapPath(path), false))
            {
                writer.WriteLine(info);
            }

            return Json(HttpStatusCode.OK);
        }


        [HttpGet]
        public ActionResult save(string ip, int port, int times_per_second, int seconds, string file_name)
        {
            // close the last connection
            CommandModel.Instance.Close();
            // connect again
            CommandModel.Instance.Connect(ip, port);
            ViewBag.drawPath = true;
            ViewBag.mode = "saveToFile";
            ViewBag.samplingRate = times_per_second;
            ViewBag.seconds = seconds;
            ViewBag.fileName = file_name;

            return View("display");
        }


        [HttpGet]
        public ActionResult ReadFromFile(string file_name)
        {
            string path = "~/" + file_name + ".json";
            // read the info from the file
            using (StreamReader reader = new StreamReader(Server.MapPath(path), true))
            {
                return Json(reader.ReadToEnd(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}