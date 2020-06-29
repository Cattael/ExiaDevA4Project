using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DecrypteService
{
    public class Server : IServer
    {
        public string Connect(string username, string password)
        {
            if(string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(username))
            {
                return "";
            }
            if (BDDConnection.UserPasswordExist(username, password))
            {
                string token = TokenGen.GetInstance.GenerateToken(15);
                int IDUser = BDDConnection.getUserID(username);
                BDDConnection.UpdateUserToken(IDUser, token);

                User user = BDDConnection.getUserInfo(username);

                Console.WriteLine("The user " + user.Username + " with token " + user.Token + " logged in");

                return token;
            }
            return ""; 
        }

        public void sendFiles(string file, string tokenUser)
        {
            if (BDDConnection.CheckTokenUser(tokenUser))
            {
                User user = BDDConnection.getUserInfoByToken(tokenUser); 
                Request request = new Request(file);
                Console.WriteLine("The user " + user.Username + " with token " + user.Token + " ask for a decrypt");
                Console.WriteLine("file received : \n" + request.name + "\n" + request.body + "\n" + request.token);
                Decrypter decrypter = new Decrypter(request);
                var result = decrypter.startDecrypt(); 
                //sendJMS(resultToStringArray); 
            }
        }

        public bool isAuthenticated(string token)
        {
            return BDDConnection.CheckTokenUser(token); 
        }

        
    }
}
