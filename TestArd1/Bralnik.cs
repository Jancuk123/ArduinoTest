using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestArd1.Models;

namespace TestArd1
{
//ali je res dobra rešitev? --> test za git fork
    public class Bralnik
    {
        public static Rootobject Beri()
        {
            RestClient client = new RestClient("https://arduino_m0_pro_test.data.thethingsnetwork.org/api/v2/query");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "key ttn-account-v2.SmU4hKGJhi4d1-g7sHLzptQvNGwHefQAI9g-3h5Ju7k");
            var response = client.Execute(request);
            if (response.IsSuccessful && response.Content != "")
            {

                var json = response.Content;
                string jsonString = "{ \"Podatki\": " + json + " }";
                var data = JsonConvert.DeserializeObject<Rootobject>(jsonString);
                using (var db = new NewArdContext())
                {
                    
                    var rowcount = (from x in db.Podatki
                                   select x).Count();
                    if (rowcount != 0)
                    {
                        var vBazi = (from x in db.Podatki
                                     select x.Time).Max();
                        var lista = new List<Class1>();
                        lista.AddRange(data.Property1);
                        foreach (var x in lista)
                        {
                            if (x.Time.CompareTo(vBazi) > 0)
                            {
                                db.Podatki.Add(x);
                            }
                        }
                    }
                    else
                    {
                        foreach (var x in data.Property1)
                        {
                                db.Podatki.Add(x);
                        }
                    }
                    db.SaveChanges();
                }
                return data;
            }
            else
            { return null; }
        }
    }
}