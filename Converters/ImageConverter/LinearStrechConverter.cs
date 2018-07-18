using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Helpers;
using Converters.Image;
using ConvertInterfaces;

namespace Converters.ImageConverter
{
    public class LinearStrechConverter<T> : MyImageConverter<T>, IMyImageConverterWithParams<T> where T : IMyImage, new()
    {
        public LinearStrechConverter()
        {
            NumberOfParams = 0;
            TypeOfParams = null;
            Name = "Линейное растяжение";
            ConverterType = ConvertInterfaces.Enum.ConverterEnum.LinearStrech;
        }
        public T Convert(T source, params object[] prms)
        {
            if (prms.Length > 0)
                throw new Exception("Не должно быть параметров");
            var dist = new MyImage(source.Width, source.Height);
            Parallel.For(0, source.Width, i =>
            {
                Parallel.For(0, source.Height, j =>
                {
                    Color color = source[i, j];
                    Color newcolor =
                        Color.FromArgb(
                            Norm((color.R - source.Min.R) * (255 / (source.Max.R - source.Min.R))),
                            Norm((color.G - source.Min.G) * (255 / (source.Max.G - source.Min.G))),
                            Norm((color.B - source.Min.B) * (255 / (source.Max.B - source.Min.B))));
                    dist[i, j] = newcolor;
                });
            });
            object img = dist;
            return (T)img;
        }
    }
}
