using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void initGrid()
        {
            int width = int.Parse(WidthCounter.Text);
            int heigth = int.Parse(HeightCounter.Text);

            // Create the Grid

            Grid DynamicGrid = new Grid();

            DynamicGrid.Width = 400;

            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Center;

            DynamicGrid.VerticalAlignment = VerticalAlignment.Center;

            DynamicGrid.ShowGridLines = true;


            for(int i = 0; i < width; i++)
            {
                ColumnDefinition gridCol = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(gridCol);


            }

            for (int i = 0; i < heigth; i++)
            {
                RowDefinition gridRow = new RowDefinition();
                gridRow.Height = new GridLength(45);
                DynamicGrid.RowDefinitions.Add(gridRow);
            }


            RootWindow.Content = DynamicGrid;



        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            initGrid();
        }
    }
}
