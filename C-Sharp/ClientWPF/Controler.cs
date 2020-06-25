using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientWPF
{

    public class Controler
    {
        MainWindow view;
        private string token;
        private string username; 
        IServer proxy; 
        public Controler(MainWindow view)
        {
            this.view = view;
            ChannelFactory<IServer> channelFactory = new ChannelFactory<IServer>("*");
            proxy = channelFactory.CreateChannel();
        }
        public void Connect(string username, string password)
        {
            this.username = username; 

            token = proxy.Connect(username, password);
            if (token == "") MessageBox.Show("User or password invalid");
        }

        public void CheckAuthentication()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("You are not athentificated...");
            }
            if (proxy.isAuthenticated(username, token))
            {
                MessageBox.Show("You're correctly authenticated"); 
            }
            else
            {
                MessageBox.Show("You are not athentificated...");
            }
        }
    }
}
