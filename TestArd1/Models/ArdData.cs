using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestArd1.Models
{
    public class Rootobject
    {
        [JsonProperty("Podatki")]
        public List<Class1> Property1 { get; set; }
    }


    public partial class Class1
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("temperatura")]
        public decimal Temperatura { get; set; }
        [JsonProperty("time")]
        public System.DateTime Time { get; set; }

        [JsonProperty("vlaznost")]
        public decimal Vlaznost { get; set; }
    }

    public class NewArdContext : DbContext
    {
        public DbSet<Class1> Podatki { get; set; }
    }
}