using DecrypteService.JavaDecryptService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecrypteService
{
    public static class JavaConnector
    {
        private static DecryptEndPointClient decryptEndPointClient;
        public static bool SendToJava(string toSend)
        {
            try
            {
                decryptEndPointClient.DecryptOperation(toSend);
                return true;
            }
            catch { return false; }

        }

        public static void init()
        {
            if (decryptEndPointClient != null)
                return; 
            decryptEndPointClient = new DecryptEndPointClient();
        }
    }
}
