using DuAnCuoiKhoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnCuoiKhoa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DuAnCuoiKhoaEntities db = new DuAnCuoiKhoaEntities();
        public ActionResult Index()
        {

            return View();
        }
    }
}