using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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

                Console.WriteLine(token);

                return token;
            }
            return ""; 
        }

        public bool isAuthenticated(string username, string token)
        {
            return BDDConnection.CheckTokenUser(username, token); 
        }
    }
}
