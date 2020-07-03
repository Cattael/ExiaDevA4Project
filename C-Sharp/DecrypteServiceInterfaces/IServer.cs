using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace DecrypteServiceInterfaces
{
    [ServiceContract]
    public interface IServer
    {
        [OperationContract]
        User Connect(string username, string password);
        [OperationContract]
        bool isAuthenticated(string token);
        [OperationContract]
        string sendFiles(string file, string name, string tokenUser);
        [OperationContract]
        void responseFile(string file);
        [OperationContract]
        int getPourcentRequest(string token, string tokenRequest);
        [OperationContract]
        string[] getTokenRequests(string token, string email);

    }
}
