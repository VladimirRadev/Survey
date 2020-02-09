using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers {
    public class NeController : Controller {

        public ActionResult Index(FormCollection data) {
            //string fname = data["fname_name"];
            //string lname = data["lname_name"];
            //Response.Write("Your Full name is= " + fname + " " + lname);


            return View();
        }

        public ActionResult show(string fname_name, string lname_name) {

            Response.Write(fname_name + "AAAAAAAAAAAA");

            return null;
        }

    }
}
