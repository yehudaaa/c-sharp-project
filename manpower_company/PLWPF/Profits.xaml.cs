using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Profits.xaml
    /// </summary>
    public partial class Profits : UserControl
    {
        BL.IBL bl;
        public Profits()
        {
            InitializeComponent();
            bl = BL.FactoryBl.GetInstance();
            
            IEnumerable<IGrouping<string, float>> result = bl.groupingProfitsByMonth();
            int i = 0;
            int j = 0;
            foreach (var item in result)
            {
                TextBlock t =(TextBlock)TextBlockGrid.Children[j++];
                t.Text = item.Key;
                foreach (var c in item)
                {
                    Rectangle x =(Rectangle)RectangleGrid.Children[i++];
                    x.Height = c*3;
                    if (c < 10)
                        x.Fill = Brushes.Red;
                    else if (c < 20)
                        x.Fill = Brushes.Orange;
                }
            }
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle t = sender as Rectangle;
            t.ToolTip = ((double)(int)(t.Height  * 10) / 10).ToString() + " ש''ח";
        }
    }
}
