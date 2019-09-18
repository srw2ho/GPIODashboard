﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIODashboard.Helpers
{

    using System;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;

    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;


    public static class ImageHelper
    {
        public static async Task<BitmapImage> ImageFromStringAsync(string data)
        {
            var byteArray = Convert.FromBase64String(data);
            var image = new BitmapImage();
            using (var stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(byteArray.AsBuffer());
                stream.Seek(0);
                await image.SetSourceAsync(stream);
            }

            return image;
        }

        public static BitmapImage ImageFromImagesFile(string fileName)
        {

            try
            {
              //       return null;
                var image = new BitmapImage(new Uri($"ms-appx:///images/{fileName}"));
                return image;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
