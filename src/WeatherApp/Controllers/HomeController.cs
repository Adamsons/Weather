using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using UnitsNet.Units;
using WeatherApp.Service.Extensions;
using WeatherApp.Service.ResultTypes;
using WeatherApp.Service.Services.Abstract;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}