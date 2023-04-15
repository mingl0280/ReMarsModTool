using ReMarsModTool.DataStructures;
using System.Data;
using System.Text.RegularExpressions;

namespace ReMarsModTool.GlobalData;

static class ExtendedMethods
{
    public static string RemoveSpaces(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Define a regular expression pattern for space and space-like characters
        string pattern = @"[\s\u00A0\u1680\u180E\u2000-\u200A\u2028\u2029\u202F\u205F\u3000]+";

        // Replace all continuous matching characters with a single underscore
        string result = Regex.Replace(input, pattern, "_");

        return result;
    }

    public static bool ContainsKey(this DataTable Dt, string Needle)
    {
        foreach (DataRow data_row in Dt.Rows)
        {
            var col_str = data_row[0].ToString();
            if (col_str == Needle)
            {
                return true;
            }
        }

        return false;
    }
    public static void UpdateValueByKey(this DataTable Table, string KeyStr, string ValueStr)
    {
        // Get the index of the key column
        const int Constkey_column_index = 0;

        // Find the row with the given key
        DataRow? row_to_update = null;

        foreach (DataRow? row in Table.Rows)
        {
            if (row != null && row[Constkey_column_index].Equals(KeyStr))
            {
                row_to_update = row;
                break;
            }
        }

        // Update the value of the row with the given key
        if (row_to_update != null)
        {
            row_to_update[1] = ValueStr;
        }
    }

    public static void AddRowWithKeyAndValue(this DataTable Table, string KeyStr, string ValueStr)
    {
        var new_row = Table.NewRow();
        new_row[0] = KeyStr;
        new_row[1] = ValueStr;
        Table.Rows.Add(new_row);
    }

    public static string? GetValueWithKey(this DataTable Table, string KeyStr)
    {
        foreach (DataRow data_row in Table.Rows)
        {
            var col_str = data_row[0].ToString();
            if (col_str == KeyStr)
            {
                return data_row[1].ToString();
            }
        }

        return null;
    }
    public static void UpdateAllEfficiency(this Employer? EmployerInstance, int Capacity, int Multiplier)
    {
        if (EmployerInstance == null)
            return;
        foreach (var property in EmployerInstance.GetType().GetProperties())
        {
            if (property.PropertyType.IsSubclassOf(typeof(BaseEmployerType)))
            {
                if (property.GetValue(EmployerInstance) is BaseEmployerType { Capacity: > 0 } employer_type)
                {
                    employer_type.Capacity = Capacity;
                    employer_type.WorkloadIncrease *= Multiplier;
                }
            }
        }
    }
}