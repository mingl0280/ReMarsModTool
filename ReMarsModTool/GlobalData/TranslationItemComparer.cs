using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ReMarsModTool.DataStructures;

namespace ReMarsModTool.GlobalData;

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