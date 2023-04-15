using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReMarsModTool.DataStructures;

public class TranslationItem : INotifyPropertyChanged
{
    private string _languageCode;
    private string _itemValue;

    public string LanguageCode
    {
        get => _languageCode;
        set
        {
            _languageCode = value;
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