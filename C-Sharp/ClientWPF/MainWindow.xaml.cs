using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controler controler; 
        public MainWindow()
        {
            InitializeComponent();
            this.controler = new Controler(this); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controler.CheckAuthentication();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                Connect();
            }
        }

        private void Connect()
        {
            controler.Connect(loginTextBox.Text, passwordTextBox.Text);
            if (controler.CheckAuthentication())
            {
                BrutforceWindow bf = new BrutforceWindow(this.controler, this);
                bf.Show();
                this.Hide();
            }
        }
    }
}
