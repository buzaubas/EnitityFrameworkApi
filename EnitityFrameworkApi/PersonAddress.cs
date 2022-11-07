using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnitityFrameworkApi
{
    public class PersonAddress
    {
        public int id { get; set; } 
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int post { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }
}
