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
using System.Windows.Shapes;

namespace SeaBattle
{
    /// <summary>
    /// Interaction logic for LeadersWindow.xaml
    /// </summary>
    public partial class LeadersWindow : Window
    {
        LeadersWindowViewModel _VM;

        public LeadersWindow()
        {
            InitializeComponent();

            LeadersList.Items.Clear();
        }

        public LeadersWindow(LeadersWindowViewModel p_VM)
        {
            this.DataContext = p_VM;
            _VM = p_VM;

            InitializeComponent();

           // LeadersList.Items.Clear();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                _VM.NewLeaderName = ((TextBox)sender).Text;
            }
        }


        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

    }

    
}
