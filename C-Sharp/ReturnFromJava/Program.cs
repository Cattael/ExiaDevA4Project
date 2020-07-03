using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ReturnFromJava
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Je suis le service ReturnFromJava");
            using (ServiceHost host = new ServiceHost(typeof(Return)))
            {
                host.Open();
                Console.ReadLine();

            }
        }
    }
}
