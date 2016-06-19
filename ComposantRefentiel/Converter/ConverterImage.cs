using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ComposantReferentiel.Converter
{
    /// <summary>
    /// Converter d'image utilisé pour les champs de type Image
    /// </summary>
    public class ConverterImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result;
            if (value != null && value is byte[])
            {
                byte[] buffer = value as byte[];
                MemoryStream streamSource = new MemoryStream(buffer);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = streamSource;
                bitmapImage.EndInit();
                result = bitmapImage;
            }
            else
            {
                result = null;
            }
            return result;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bitmapImage = value as BitmapImage;
            byte[] result = null;
            if (bitmapImage != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                new JpegBitmapEncoder
                {
                    Frames = 
                    {
                        BitmapFrame.Create(bitmapImage)
                    }
                }.Save(memoryStream);
                result = memoryStream.GetBuffer();
            }
            return result;
        }
    }
}
