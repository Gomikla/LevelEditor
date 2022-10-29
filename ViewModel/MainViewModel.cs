using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LevelEditor.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace LevelEditor.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private int? widthValue;

        [ObservableProperty]
        private int? heighValue;

        [ObservableProperty]
        private ObservableCollection<CellData> cellList = new();

        [ObservableProperty]
        private Grid myGrid = new();

        [RelayCommand]
        public void SaveToJson()
        {
            SaveFile();
        }

        public void LoadFromJson()
        {
            LoadFile();
        }

        private void SaveFile()
        {
            string jsonFile = "C:\\Users\\jonas\\Desktop\\ToolsProg\\Eksamen\\LevelEditor\\LevelEditor\\SavedMaps\\test.json";
            string json = JsonSerializer.Serialize(CellList);
            File.WriteAllText(jsonFile, json);
        }

        private void LoadFile()
        {
            string json = File.ReadAllText("C:\\Users\\jonas\\Desktop\\ToolsProg\\Eksamen\\LevelEditor\\LevelEditor\\SavedMaps\\test.json");
            CellList = JsonSerializer.Deserialize<ObservableCollection<CellData>>(json);
        }

        

    }
}
