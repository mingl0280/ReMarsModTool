using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Path = System.IO.Path;
using System.Diagnostics.CodeAnalysis;
using ReMarsModTool.GlobalData;

namespace ReMarsModTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _notified_items;
            GlobalItems.PossibleTranslations = new List<string>();
            GlobalItems.PossibleTranslations.AddRange(new[] {"English", "ChineseSimplified" });
        }


        private void OnNewClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "New" is clicked
        }

        private void OnOpenClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Open" is clicked
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Save" is clicked
        }

        private void OnSaveAsClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Save As" is clicked
        }

        private void OnExportClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Export" is clicked
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Close" is clicked
        }

        private void OnModConfigurationsClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Mod Configurations" is clicked
        }

        private void OnAddBuildingClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Building" is clicked
        }

        private void OnAddUnitClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Unit" is clicked
        }

        private void OnAddResearchClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Research" is clicked
        }

        private void OnAddItemClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Item" is clicked
        }

        private void OnAddAstroidClicked(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for when "Add Astroid" is clicked
        }

        NotifiedItems _notified_items = new NotifiedItems();
        
        private void BtnBrowsePreviewPic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                _notified_items.PreviewPicture = dlg.FileName;
                TextBoxPreviewPicPath.Text = dlg.FileName;
            }
        }

        private void TextBox_PreviewPicPath_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            var filePath = textBox.Text;

            if (IsValidPicture(filePath))
            {
                _notified_items.PreviewPicture = filePath;
            }
        }

        private bool IsValidPicture(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }

            if (!File.Exists(filePath))
            {
                return false;
            }

            var ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".jpg" || ext == ".png" || ext == ".bmp")
                return true;
            return false;
        }

        private void TextBoxModName_LostFocus(object sender, RoutedEventArgs e)
        {
            DataGridTranslations.InvalidateVisual();
            DataGridTranslations.Items.Refresh();
        }
    }

    public class NotifiedItems : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private string _previewPicture;
        private string _modName;

        public NotifiedItems()
        {
            _previewPicture = "";
            _modName = "";
        }

        public string PreviewPicture
        {
            get { return _previewPicture; }
            set
            {
                _previewPicture = value;
                OnPropertyChanged(nameof(PreviewPicture));
            }
        }

        public string ModName
        {
            get => _modName;
            set
            {
                _modName = value;
                foreach (var possible_translation in GlobalItems.PossibleTranslations)
                {
                    if (_translations.ContainsKey(possible_translation))
                    {
                        _translations[possible_translation] = value;
                    }
                    else
                    {
                        _translations.Add(possible_translation, value);
                    }
                }
                OnCollectionChanged(Translations);
                OnPropertyChanged(nameof(ModName));
            }
        }
        private Dictionary<string, string> _translations = new Dictionary<string, string>();

        public Dictionary<string, string> Translations
        {
            get => _translations;
            set {
                _translations = value;
                if (ModName != _translations["English"])
                    _modName = _translations["English"];
                OnCollectionChanged(Translations);
            }
        }

        public bool HasPreviewPicture
        {
            get
            {
                return string.IsNullOrWhiteSpace(_previewPicture);
            }
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected virtual void OnCollectionChanged(object Object)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, Object ));
        }
    }

    public class TranslationItem : INotifyPropertyChanged
    {
        private string _languageCode;
        private string _itemValue;
        
        public string LanguageCode
        {
            get => _languageCode;
            set
            { _languageCode = value;
            OnPropertyChanged(nameof(LanguageCode));
            }
        }

        public string ItemValue
        {
            get => _itemValue;
            set
            {
                _itemValue = value;
                OnPropertyChanged(nameof(ItemValue));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(string str)
        {
            return str == _languageCode;
        }

        public override string ToString()
        {
            return $"\"{_languageCode}\":\"{_itemValue}\"";
        }
    }

    class TranslationItemComparer : IEqualityComparer<TranslationItem>
    {
        public bool Equals(string? x, TranslationItem? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            if (x == y.LanguageCode) return true;
            return false;
        }

        public bool Equals(TranslationItem? x, string? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            if (x.LanguageCode == y) return true;
            return false;
        }
        public bool Equals(TranslationItem? x, TranslationItem? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            if (x.LanguageCode == y.LanguageCode) return true;
            return false;
        }

        public int GetHashCode([DisallowNull] TranslationItem obj)
        {
            return base.GetHashCode();
        }
    }

}
