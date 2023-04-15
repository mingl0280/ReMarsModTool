using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using ReMarsModTool.GlobalData;

namespace ReMarsModTool;

public class NotifiedItems : INotifyPropertyChanged, INotifyCollectionChanged
{
    private string _previewPicture;
    private string _modName;

    public NotifiedItems()
    {
        _previewPicture = "";
        _modName = "";
        _translations.Columns.Add("Language");
        _translations.Columns.Add("Trans");
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
                    foreach (DataRow translations_row in _translations.Rows)
                    {
                        if (translations_row["Language"].ToString() == possible_translation)
                            translations_row["Trans"] = value;
                    }
                }
                else
                {
                    //_translations.AddRowWithKeyAndValue(possible_translation, value);
                    var dr = _translations.NewRow();
                    dr["Language"] = possible_translation;
                    dr["Trans"]=value;
                    _translations.Rows.Add(dr);
                }
            }
            OnPropertyChanged(nameof(ModName));
            OnPropertyChanged(nameof(Translations));
        }
    }

    private DataTable _translations = new();

    public DataTable Translations
    {
        get => _translations;
        set
        {
            _translations = value;
            if (Translations.ContainsKey("English"))
            {
                if (ModName != _translations.GetValueWithKey("English"))
                {
                    _modName = _translations.GetValueWithKey("English") ?? string.Empty;
                    OnPropertyChanged(nameof(ModName));
                }
            }
            OnPropertyChanged(nameof(Translations));
        }
    }

    public bool HasPreviewPicture
    {
        get
        {
            return string.IsNullOrWhiteSpace(_previewPicture);
        }
    }

    public string ModID
    {
        get => _modName.ToLower().RemoveSpaces();
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