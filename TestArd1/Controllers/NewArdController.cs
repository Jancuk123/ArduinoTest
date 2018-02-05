using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestArd1.Models;

namespace TestArd1.Controllers
{
    public class NewArdController : Controller
    {
        // GET: NewArd
        public ActionResult Index()
        {
            var db = new NewArdContext();
            Rootobject rezultati = Bralnik.Beri();
            if (rezultati != null)
            {
                var podatki = from x in rezultati.Property1
                              orderby x.Time descending
                              select x;
                foreach (var y in podatki)
                {
                    y.Time = y.Time.AddHours(1);
                    y.Temperatura = y.Temperatura / 100;
                    y.Vlaznost = y.Vlaznost / 100;
                }

                return View(podatki);
            }
            else
            {
                List<Class1> kren = new List<Class1>();
                Class1 en = new Class1();
                en.Time = DateTime.Now;
                en.DeviceId = "fake1337";
                en.Raw = "a3jgu9d0";
                en.Temperatura = (decimal)20.20;
                en.Vlaznost = (decimal)40.40;
                kren.Add(en);

                return View(kren);
            }

        }

        public ActionResult Baza()
        {
            var db = new NewArdContext();
            var podatki1 = from x in db.Podatki
                           orderby x.Time descending
                           select x;
            foreach (var y in podatki1)
            {
                y.Time = y.Time.AddHours(1);
                y.Temperatura = y.Temperatura / 100;
                y.Vlaznost = y.Vlaznost / 100;
            }


            return View(podatki1);
        }
    }
}