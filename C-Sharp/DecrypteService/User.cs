using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DecrypteService
{
    public class User
    {
        public int ID { get; set; }
        public string  Username { get; set; }
        public string Token { get; set; }
        public DateTime Last { get; set; }
    }
}
