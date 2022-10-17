using LevelEditor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;
using Accessibility;
using System.Net.Cache;
using Xceed.Wpf.AvalonDock.Layout;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string pathToSprite;

        Image CopiedImage = new Image();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            initGrid();
        }


        public void initGrid()
        {
            int width = int.Parse(WidthCounter.Text);
            int heigth = int.Parse(HeightCounter.Text);

            // Create the Grid

            Grid grid = new Grid();

            grid.Width = 350;

            grid.Height = 350;

            grid.HorizontalAlignment = HorizontalAlignment.Center;

            grid.VerticalAlignment = VerticalAlignment.Center;

            //grid.ShowGridLines = true;

            for (int i = 0; i < width; i++)
             {
                ColumnDefinition coldef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(coldef);
            }

             for (int i = 0; i < heigth; i++)
             {
                RowDefinition rowdef = new RowDefinition();
                grid.RowDefinitions.Add(rowdef);
            }

             for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < heigth; j++)
                {
                    Button rec = new Button();
                    //rec.Stroke = Brushes.Black;
                    //rec.StrokeThickness = 2;
                    rec.Name = $"mapBtn{i}{j}";
                    //rec.Tag = $"mapBtn{i}{j}";
                    rec.Click += new RoutedEventHandler(AddTest);
                    rec.Focusable = false;


                    grid.Children.Add(rec);
                    //put it in column 0, row 0
                    Grid.SetColumn(rec, i);
                    Grid.SetRow(rec, j);

                }
            }

            RootWindow.Content = grid;

        }

        private void CopyImage(object sender, MouseButtonEventArgs e)
        {
            //CopiedImage.Source = ((Image)sender).Source;
            Image img = (Image)sender;

            pathToSprite = img.Tag.ToString();
        }

        private void AddTest(object sender, EventArgs e)
        {
            var button = sender as Button;
            ImageBrush brush = new ImageBrush();
            BitmapImage bitmap = new BitmapImage();
            string fullPathToSprites = Path.GetFullPath(@"..\..\" + pathToSprite);


            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullPathToSprites, UriKind.Absolute);
            bitmap.EndInit();

            brush.ImageSource = bitmap;
            button.Background = brush;

            //button.Content = CopiedImage;           
        }
    }
}
