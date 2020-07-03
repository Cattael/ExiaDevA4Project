using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DecrypteService
{
    public class Request
    {
        public string name { get; set; }
        public string body { get; set; }
        public string token { get; set; }
        public Request()
        {

        }
        public Request(string name, string body)
        {
            this.body = body;
            this.token = TokenGen.GetInstance.GenerateToken(15);
            this.name = name; 
        }
    }
}
