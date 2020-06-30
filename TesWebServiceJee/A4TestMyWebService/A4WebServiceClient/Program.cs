using A4WebServiceClient.DecryptService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A4WebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
         //   MathService.MathServiceClient client = new MathService.MathServiceClient();

            DecryptService.DecryptEndPointClient client2 = new DecryptService.DecryptEndPointClient();
            List<stringArray> array2D = new List<stringArray>();
            
            array2D.Add(new stringArray(new string[] { "one", "twozgze gzegezgzegzegzegzegzegzegzegzeg" })); 
            array2D.Add(new stringArray(new string[] { "two", "twozgze hklgkiuyykui" })); 
            array2D.Add(new stringArray(new string[] { "three", "fer frzeaaeztgsdf" }));

            client2.DecryptOperation(array2D.ToArray(), "token");


            /*  var result =  client.multiply(9 , 7);
              client.Close();*/

            /*
             * 
               public stringArray() { }

        public stringArray(string[] item)
        {
            this.item = item; 
        }
             * */


            Console.Read();

        }
    }
}
