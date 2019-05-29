using System;
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
        public ActionResult display(string ip, int port,   int times_per_second)
        {
            CommandModel.Instance.Connect(ip, port);
            while(true)
            {
                //display(ip, port);
                System.Threading.Thread.Sleep(times_per_second * 1000);
            }
        }*/

        [HttpGet]
        public ActionResult save(string ip, int port, int times_per_second, int seconds, string file_name)
        {
            return View();
        }

    }
}