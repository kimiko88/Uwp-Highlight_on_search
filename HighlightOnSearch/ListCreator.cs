using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HighlightOnSearch
{
    public class ListIndexCreator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            var obj = value as ExampleObject;

            if (obj != null)
            {
                return $"Indice: {obj.Index}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            return null;
        }

    }


    public class ListAboutCreator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            var obj = value as ExampleObject;

            if (obj != null)
            {
                return $"\n {obj.About}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            return null;
        }

    }
}
