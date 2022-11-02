using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LevelEditor.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using Path = System.IO.Path;
using Newtonsoft.Json;

namespace LevelEditor.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public static string SaveAndLoadPath = @"SavedMaps\test.json";
        public static string relativeSaveAndLoadPath = Path.GetFullPath(@"..\..\..\" + SaveAndLoadPath);

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
            string jsonFile = relativeSaveAndLoadPath;
            string json = JsonConvert.SerializeObject(CellList);
            File.WriteAllText(jsonFile, json);
        }

        private void LoadFile()
        {
            string json = File.ReadAllText(relativeSaveAndLoadPath);
            CellList = JsonConvert.DeserializeObject<ObservableCollection<CellData>>(json);
        }

    }
}
