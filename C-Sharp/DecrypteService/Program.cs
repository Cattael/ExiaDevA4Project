using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecrypteService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Server)))
            {
                BDDConnection.GetInstance.OpenConnection("Server=192.168.56.101;DataBase=ProjetDev;User id=sa;Password=root;");
                JavaConnector.init(); 
                host.Open();
                Console.WriteLine("Server is open");
                Console.WriteLine("Press enter to close the Server");
                string input;
                while ((input = Console.ReadLine()) != "exit")
                {
                    switch (input.Trim().ToLower())
                    {
                        case "addapp":
                            addApp();
                            break;
                        case "addaccount":
                            addAccount();
                            break;
                        case "resettokensuser":
                            resetTokensUser();
                            break;
                        case "resettokenapp":
                            resetTokenApp();
                            break;
                        case "sendmail":
                            sendMail();
                            break;
                        default:
                            Console.WriteLine("Need help ? :\n---- \nAddApp\n----\nAddAccount\n----\nResetTokensuser\n----\nResetTokenapp\n----\nSendMail");
                            break; 
                    }
                }
            }
        }

        private static void sendMail()
        {
            Console.Write("To : ");
            string to = Console.ReadLine();
            Console.Write("Content : ");
            string content = Console.ReadLine();
            try
            {
                MailSender.GetInstance.SendMail(to, content);
                Console.WriteLine("Mail Sended !");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to send the mail : " + ex.Message);
            }
        }

        private static void addApp()
        {
            Console.Write("Name : ");
            string name = Console.ReadLine();
            DateTime ExperitionDate = DateTime.Now + TimeSpan.FromDays(15);
            BDDConnection.GetInstance.AddApp(name, ExperitionDate);
        }

        private static void addAccount()
        {
            Console.Write("Username : ");
            string username = Console.ReadLine(); 
            Console.Write("Password : ");
            string password = Console.ReadLine(); 
            Console.Write("Email : ");
            string email = Console.ReadLine();
            BDDConnection.GetInstance.AddAccount(username, password, email); 
        }

        private static void resetTokensUser()
        {
            BDDConnection.GetInstance.ResetTokenUser();
        }
        private static void resetTokenApp()
        {
            Console.Write("Name of the app : ");
            BDDConnection.GetInstance.ResetTokenApp(Console.ReadLine()); 
        }
    }
}
