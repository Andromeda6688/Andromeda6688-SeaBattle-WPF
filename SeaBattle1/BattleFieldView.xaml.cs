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
    /// Interaction logic for BattleField.xaml
    /// </summary>
    public partial class BattleFieldView : UserControl
    {

        BattleField _BattleField;

        public BattleFieldView()
        {
            InitializeComponent();

            _BattleField = (BattleField)this.DataContext;

          //  DrawCells(_BattleField);
        }

      /*    public void DrawCells(BattleField p_BattleField)
          {
              StackPanel BigContainer = new StackPanel();
              BigContainer.Orientation = Orientation.Vertical;
              BattleFieldCanvas.Children.Add(BigContainer);
              BattleFieldCanvas.Height = BattleField.DefaultFieldSize * CellView.CellSize;
              BattleFieldCanvas.Width = BattleField.DefaultFieldSize * CellView.CellSize;

              for (int i = 0; i < BattleField.DefaultFieldSize; i++)
              {
                  //Columns
                  StackPanel SmallContainer = new StackPanel();
                  SmallContainer.Orientation = Orientation.Horizontal;
                  BigContainer.Children.Add(SmallContainer);

                  for (int j = 0; j < BattleField.DefaultFieldSize; j++)
                  {                    
                      CellView CellV = new CellView();

                      CellV.DataContext = new CellViewModel(p_BattleField.Cells[i, j]);                    

                      CellV.Width = CellView.CellSize;
                      CellV.Height = CellView.CellSize;

                      SmallContainer.Children.Add(CellV);
                  }
              }
 
          }*/


    }
}
