using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ReturnFromJava
{
    public class Return : IReturn
    {
        public void responseFile(string file)
        {
            Console.WriteLine(file);
            ChannelFactory<IServer> channelFactory = new ChannelFactory<IServer>("*");
            IServer proxy = channelFactory.CreateChannel();
            proxy.responseFile(file);
        }
    }
}
