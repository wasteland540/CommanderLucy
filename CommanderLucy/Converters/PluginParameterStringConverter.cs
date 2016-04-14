using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace CommanderLucy.Converters
{
    public class PluginParameterStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var parameters = value as string[];

                if (parameters != null && parameters.Length > 0)
                {
                    var strValue = new StringBuilder();

                    foreach (string p in parameters)
                    {
                        strValue.Append(string.Format("\"{0}\";", p));
                    }

                    return strValue.ToString().Substring(0, strValue.ToString().Length - 1);
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;

            if (!string.IsNullOrEmpty(strValue))
            {
                string[] splitedValue = strValue.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < splitedValue.Length; i++)
                {
                    splitedValue[i] = splitedValue[i].Replace("\"", "");
                }

                return splitedValue;
            }

            return null;
        }
    }
}