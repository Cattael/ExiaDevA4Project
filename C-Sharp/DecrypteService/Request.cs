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
        public Request(string name, string body, string token)
        {
            this.body = body;
            this.token = token;
            this.name = name; 
        }
        public Request(string file)
        {
            var xml = new XmlDocument();
            xml.LoadXml(file);
            XmlNodeList xmlnlist = xml.SelectNodes("/file"); 
            this.body = xml.SelectNodes("/file/body")[0].InnerText; 
            this.name = xml.SelectNodes("/file/name")[0].InnerText;
            this.token = TokenGen.GetInstance.GenerateToken(15);
        }
    }
}
