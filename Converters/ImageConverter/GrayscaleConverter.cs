using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Image;
using ConvertInterfaces;
using Converters.Helpers;

namespace Converters.ImageConverter
{
    public class GrayscaleConverter<T>
        : MyImageConverter<T>,
            IMyImageConverterWithParams<T> where T : IMyImage
    {
        public GrayscaleConverter():base()
        {
            Name = "Оттенки серого";
            ConverterType = ConvertInterfaces.Enum.ConverterEnum.GrayScale;
        }

        public T Convert(T source, params object[] prms)
        {
            if (prms.Length != 0)
                throw new Exception("Не должно быть параметров");
            var dist = new MyImage(source.Width, source.Height);
            object srcimg = source;
            var tasks = new List<Task>();
            var ranges = DivRange.Divide(0, 0, source.Width, source.Height, 16);
            foreach(var range in ranges)
            {
                tasks.Add(Task.Run(() =>
                    ChangeToGrayscale((MyImage) srcimg, dist, range[0], range[1], range[2], range[3])));
            }
            Task.WaitAll(tasks.ToArray());
            object img = dist;
            return (T) img;
        }

        private void ChangeToGrayscale(IMyImage source, IMyImage dist, int x1, int y1, int x2, int y2)
        {
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    int newcolor = (int) (0.299 * source[i, j].R +
                                          0.587 * source[i, j].G +
                                          0.114 * source[i, j].B);
                    dist[i, j] = Color.FromArgb(newcolor, newcolor, newcolor);
                }
            }
        }

    }
}
