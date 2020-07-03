using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DecrypteServiceInterfaces
{
    [ServiceContract]
    public interface IReturn
    {
        [OperationContract]
        void responseFile(string file);

    }
}
