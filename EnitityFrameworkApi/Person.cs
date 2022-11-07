using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnitityFrameworkApi
{
    public class Person
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string pictureUrlLarge { get; set; }
        public int personAddressId { get; set; }
        public virtual PersonAddress personAddress { get; set; }    
    }
}
