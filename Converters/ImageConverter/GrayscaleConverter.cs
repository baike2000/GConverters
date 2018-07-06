using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Image;
using ConvertInterfaces;

namespace Converters.ImageConverter
{
    public class GrayscaleConverter<T>
        : MyImageConverter<T>,
            IMyImageConverterWithParams<T> where T : IMyImage
    {
        public GrayscaleConverter():base()
        {
            Name = "Оттенки серого";
        }
        public T Convert(T source, params object[] prms)
        {
            if (prms.Length != 0)
                throw new Exception("Не должно быть параметров");
            var dist = new MyImage(source.Width, source.Height);
            for (int i = 0; i < dist.Width; i++)
            for (int j = 0; j < dist.Height; j++)
            {
                int newcolor = (int)(0.299 * source[i, j].R +
                                     0.587 * source[i, j].G +
                                     0.114 * source[i, j].B);
                dist[i, j] = Color.FromArgb(newcolor, newcolor, newcolor);
            }
            object img = dist;
            return (T)img;
        }

    }
}
