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
            InfoModel.Instance.ip = ip;
            InfoModel.Instance.port = port.ToString();

            //InfoModel.Instance.ReadData(ip, port);
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, int port, int times_per_second)
        {
            InfoModel.Instance.ip = ip;
            InfoModel.Instance.port = port.ToString();

            //InfoModel.Instance.ReadData(ip, port);
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, int port, int times_per_second, int seconds, string file_name)
        {
            return View();
        }

    }
}