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
        string Connect(string username, string password);
        [OperationContract]
        bool isAuthenticated(string token);
        [OperationContract]
        void sendFiles(string file, string tokenUser);

    }
}
