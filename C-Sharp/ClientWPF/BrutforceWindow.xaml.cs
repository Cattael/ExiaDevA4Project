using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClientWPF
{
    /// <summary>
    /// Logique d'interaction pour BrutforceWindow.xaml
    /// </summary>
    public partial class BrutforceWindow : Window
    {
        private Controler controler;
        private MainWindow mainWindow;
        DispatcherTimer timer;
        int indexcombo = 0; 
        public BrutforceWindow(Controler controler, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.controler = controler;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += TimerTick;
            timer.Start();
            controler.initComboBox(); 
            setTokenCombo();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Show();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            int i =  controler.AskPourcent(indexcombo);
            if(i < 0) { return; }
            progressBar.Value = i;
            pourcent.Content = i + " %";
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (!listFiles.Items.Contains(openFileDialog.FileName))
                    listFiles.Items.Add(openFileDialog.FileName);
                else
                    MessageBox.Show("This file is already in the list");
            }
        }

        private void DeleteFile_Click(object sender, RoutedEventArgs e)
        {
            if(listFiles.SelectedItem != null)
                listFiles.Items.Remove(listFiles.SelectedItem);
        }

        private void Exec_Click(object sender, RoutedEventArgs e)
        {
            controler.LaunchDecrypt(listFiles.Items);

            setTokenCombo(); 
        }

        public void setTokenCombo()
        {
            comboToken.Items.Clear(); 
            foreach(string token in controler.tokensRequest)
            {
                comboToken.Items.Add(token);
            }
            comboToken.SelectedIndex = 0;
        }

        private void comboToken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            indexcombo = (sender as ComboBox).SelectedIndex;
        }

    }
}
