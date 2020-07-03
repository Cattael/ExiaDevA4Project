using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClientWPF
{

    public class Controler
    {
        MainWindow view;
        private User user;
        private string username;
        public List<string> tokensRequest; 
        IServer proxy; 
        public Controler(MainWindow view)
        {
            this.view = view;
            ChannelFactory<IServer> channelFactory = new ChannelFactory<IServer>("*");
            proxy = channelFactory.CreateChannel();
            tokensRequest = new List<string>();
        }

        public void initComboBox()
        {
            tokensRequest.Clear(); 
            tokensRequest.AddRange(proxy.getTokenRequests(user.Token, user.email)); 
        }

        public void Connect(string username, string password)
        {
            this.username = username; 

            user = proxy.Connect(username, password);
            if (user.Token == "") MessageBox.Show("User or password invalid");
        }

        public bool CheckAuthentication()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false; 
            }
            if (proxy.isAuthenticated(user.Token))
            {
                return true; 
            }
            else
            {
                return false;
            }
        }

        public int AskPourcent(int i)
        {
            if (i < tokensRequest.Count)
            {
                return proxy.getPourcentRequest(user.Token, tokensRequest[i]);
            }
            return 0;
        }
        public void LaunchDecrypt(ItemCollection items)
        {
            if (items is null || items.Count < 1)
            {
                MessageBox.Show("No File to decrypt");
                return;
            }
            string[] paths = new string[items.Count];
            for(int i = 0; i < items.Count; i++)
            {
                paths[i] = (string)items.GetItemAt(i);
            }

            string[] filesContent = new string[items.Count];
            StringBuilder sb = new StringBuilder(); 
            for (int i = 0; i < paths.Length; i ++)
            {
                string line= File.ReadAllText(paths[i]);
                filesContent[i] = line; 
            }
            for (int i = 0; i < paths.Length; i++) 
            {
                tokensRequest.Add(proxy.sendFiles(filesContent[i], Path.GetFileName(paths[i]), user.Token));
            } 
        }

    }
}
