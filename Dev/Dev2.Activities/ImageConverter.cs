
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
// ReSharper disable CheckNamespace
namespace Unlimited.Applications.BusinessDesignStudio.Activities 
{
    public class ImagePathConverter : IValueConverter 
    {
        private string _imageDirectory = Directory.GetCurrentDirectory();
        public string ImageDirectory {
            get { return _imageDirectory; }
            set { _imageDirectory = value; }
        }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                BitmapImage image = new BitmapImage();
                if (value != null && value.ToString() != string.Empty) {
                    Uri imageUri = new Uri(value.ToString(), UriKind.RelativeOrAbsolute);

                    image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = imageUri;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }
                return image;
            }
            catch {
                return new BitmapImage(); 
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    } 
}
