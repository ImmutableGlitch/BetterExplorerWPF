using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeViewTest
{
    [ValueConversion(typeof(string), typeof(BitmapImage))]

    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            // check path is valid
            var path = (string)value;
            if (path == null)
                return null;

            // Get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            // Set default image to file.png
            var image = "Images/file.png";

            // null or empty path is most likely a drive
            if (string.IsNullOrEmpty(name))
                image = "Images/drive.png";
            // if path is a dir
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Images/folder.png";

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
