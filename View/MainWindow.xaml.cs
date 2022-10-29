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
using LevelEditor.Model;
using System.Xml.Linq;
using System.Diagnostics;
using LevelEditor.ViewModel;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int gridRows;
        public int gridColumns;

        public int loadWidth;
        public int loadHeigth;

        
        public static string pathToSprite;
        public string fullPathToSprites;
        public string FullPathToBlankSprite;

        public Grid grid;
        public Button btn;

        public MainViewModel mvm;

        public MainWindow()
        {
            DataContext = new MainViewModel();
            var vm = (MainViewModel)this.DataContext;
            mvm = vm;
            FullPathToBlankSprite = Path.GetFullPath(@"..\..\..\Sprites\Blank.png");
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

            mvm.MyGrid = new Grid();

            mvm.MyGrid.Width = 350;

            mvm.MyGrid.Height = 350;

            mvm.MyGrid.HorizontalAlignment = HorizontalAlignment.Center;

            mvm.MyGrid.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < width; i++)
             {
                ColumnDefinition coldef = new ColumnDefinition();
                mvm.MyGrid.ColumnDefinitions.Add(coldef);
            }

             for (int i = 0; i < heigth; i++)
             {
                RowDefinition rowdef = new RowDefinition();
                mvm.MyGrid.RowDefinitions.Add(rowdef);
            }

             for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < heigth; j++)
                {
                    btn = new Button();
                    btn.Name = $"mapBtn{i}{j}";
                    btn.Click += new RoutedEventHandler(DrawSprite);
                    btn.Focusable = false;

                    mvm.MyGrid.Children.Add(btn);
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);

                    ImageBrush brush = new ImageBrush();
                    BitmapImage bitmap = new BitmapImage();

                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(FullPathToBlankSprite, UriKind.Absolute);
                    bitmap.EndInit();

                    brush.ImageSource = bitmap;
                    btn.Background = brush;
                    btn.Background = brush;

                    CellData data = new CellData();

                    data.row = Grid.GetRow(btn);
                    data.column = Grid.GetColumn(btn);
                    data.path = FullPathToBlankSprite;

                    mvm.CellList.Add(data);

                }
            }

            RootWindow.Content = mvm.MyGrid;

        }

        private void CopyImage(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            pathToSprite = img.Tag.ToString();
            SelectedSprite.Source = new BitmapImage(new Uri(pathToSprite, UriKind.RelativeOrAbsolute));
        }

        private void EraserSelected(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            pathToSprite = img.Tag.ToString();
            SelectedSprite.Source = new BitmapImage(new Uri("/Sprites/EraserIcon.png", UriKind.RelativeOrAbsolute));
        }

        private void DrawSprite(object sender, EventArgs e)
        {
            if(pathToSprite == null)
            {
                return;
            }

            var button = sender as Button;
            ImageBrush brush = new ImageBrush();
            BitmapImage bitmap = new BitmapImage();
            fullPathToSprites = Path.GetFullPath(@"..\..\" + pathToSprite);


            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullPathToSprites, UriKind.Absolute);
            bitmap.EndInit();

            brush.ImageSource = bitmap;
            button.Background = brush;

            CellData data = new CellData();

            data.row = Grid.GetRow(button);
            data.column = Grid.GetColumn(button);
            data.path = fullPathToSprites;

            mvm.CellList.Add(data);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            mvm.LoadFromJson();

            mvm.MyGrid = new Grid();

            mvm.MyGrid.Width = 350;

            mvm.MyGrid.Height = 350;

            mvm.MyGrid.HorizontalAlignment = HorizontalAlignment.Center;

            mvm.MyGrid.VerticalAlignment = VerticalAlignment.Center;

            btn = new();

            foreach (CellData data in mvm.CellList)
            {
                gridRows = data.row;
                gridColumns = data.column;

                RowDefinition rowdef = new RowDefinition();
                mvm.MyGrid.RowDefinitions.Add(rowdef);


                ColumnDefinition coldef = new ColumnDefinition();
                mvm.MyGrid.ColumnDefinitions.Add(coldef);

                btn = new Button();
                btn.Name = $"mapBtn{gridRows}{gridColumns}";
                btn.Click += new RoutedEventHandler(DrawSprite);
                btn.Focusable = false;


                mvm.MyGrid.Children.Add(btn);
                Grid.SetColumn(btn, gridColumns);
                Grid.SetRow(btn, gridRows);

                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                string fullPathToSprites = Path.GetFullPath(data.path);


                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullPathToSprites, UriKind.Absolute);
                bitmap.EndInit();

                brush.ImageSource = bitmap;
                btn.Background = brush;
            }
            RootWindow.Content = mvm.MyGrid;
        }
    }
}
