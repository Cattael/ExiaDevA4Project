using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public bool CheckAuthentication()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false; 
            }
            if (proxy.isAuthenticated(token))
            {
                return true; 
            }
            else
            {
                return false;
            }
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

            for (int i = 0; i < paths.Length; i ++)
            {
                StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<file>\n<name>" + Path.GetFileName(paths[i]) + "</name>\n<body>" ); 
                StreamReader sr = new StreamReader(paths[i]);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.Append(line);
                }
                sb.Append("</body>\n</file>");
                filesContent[i] = sb.ToString(); 
            }
            foreach (var file in filesContent)
            {
                proxy.sendFiles(file, this.token);
            }
        }
    }
}
