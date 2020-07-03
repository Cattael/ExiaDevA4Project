using DecrypteService.JavaDecryptService;
using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Diagnostics;
using System.Xml;

namespace DecrypteService
{
    public class Server : IServer
    {
        public User Connect(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            if (BDDConnection.GetInstance.UserPasswordExist(username, password))
            {
                string token = TokenGen.GetInstance.GenerateToken(15);
                int IDUser = BDDConnection.GetInstance.getUserID(username);
                BDDConnection.GetInstance.UpdateUserToken(IDUser, token);

                User user = BDDConnection.GetInstance.getUserInfo(username);

                Console.WriteLine("The user " + user.Username + " with token " + user.Token + " logged in");

                return user;
            }
            return null;  
        }

        public string sendFiles(string file, string name, string tokenUser)
        {
            Request request = new Request(name, file);

            new Thread(() =>
            {
                if (isAuthenticated(tokenUser))
                {
                    User user = BDDConnection.GetInstance.getUserInfoByToken(tokenUser.ToString());
                    Console.WriteLine("The user " + user.Username + " with token " + user.Token + " ask for a decrypt");
                    Console.WriteLine("file received : \n" + request.name + "\n" + request.body + "\n" + request.token);
                    Decrypter decrypter = new Decrypter(request);
                    BDDConnection.GetInstance.RegisterRequest(request.token, user.email);
                    bool result = decrypter.startDecrypt();//456976
                    
                }
            }).Start();
            return request.token; 
        }

        public bool isAuthenticated(string token)
        {
            while (!Monitor.TryEnter(BDDConnection.GetInstance)) ;
            bool isok = BDDConnection.GetInstance.CheckTokenUser(token);
            Monitor.Exit(BDDConnection.GetInstance);
            return isok;
        }

        public void responseFile(string file)
        {
            string mail = ""; 
            string token = file.Split('\"')[7];
            mail = BDDConnection.GetInstance.getMailFromRequest(token);
            Console.WriteLine(mail);
            MailSender.GetInstance.SendMail(mail, file);
        }

        public int getPourcentRequest(string token, string tokenRequest)
        {
            if (isAuthenticated(token))
            {
                if (Monitor.TryEnter(BDDConnection.GetInstance))
                {
                    int i = BDDConnection.GetInstance.getPourcentRequest(tokenRequest);
                    Monitor.Exit(BDDConnection.GetInstance);
                    return i;
                }
                return -10;
            }
            return -10; 
        }

        public string[] getTokenRequests(string token, string email)
        {
            if (isAuthenticated(token))
            {
                while (!Monitor.TryEnter(BDDConnection.GetInstance)) ;
                string[] tokens = BDDConnection.GetInstance.getTokenRequestFromEmail(email);
                Monitor.Exit(BDDConnection.GetInstance);
                return tokens;
            }
            return null; 
        }
    }
}
