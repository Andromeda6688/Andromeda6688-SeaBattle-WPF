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

namespace SeaBattle
{
    /// <summary>
    /// Interaction logic for CellView.xaml
    /// </summary>
    public partial class CellView : UserControl
    {
        public static readonly int CellSize = 30;

        public CellView()
        {
            InitializeComponent();
            this.Width = CellSize;
            this.Height = CellSize;
        }

       /* private void CellButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }*/
    }
}
