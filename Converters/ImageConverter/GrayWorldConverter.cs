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
    public class GrayWorldConverter<T> : MyImageConverter<T>, IMyImageConverterWithParams<T> where T : IMyImage, new()
    {
        public GrayWorldConverter()
        {
            NumberOfParams = 0;
            TypeOfParams = null;
            Name = "Серый мир";
            ConverterType = ConvertInterfaces.Enum.ConverterEnum.GrayWorld;
        }
        public T Convert(T source, params object[] prms)
        {
            if (prms.Length > 0)
                throw new Exception("Не должно быть параметров");
            var dist = new MyImage(source.Width, source.Height);
            double Ra = 0, Ba = 0, Ga = 0;
            double Avg = 0;
            object srcimg = source;
            var tasks = new List<Task<double[]>>();
            var ranges = DivRange.Divide(0, 0, source.Width, source.Height, 16);
            foreach (var range in ranges)
            {
                tasks.Add(Task<double[]>.Factory.StartNew(() => GetSumRgb((MyImage) srcimg, range[0], range[1],
                    range[2], range[3])));
            }
            foreach (var ts in tasks)
            {
                Ra += ts.Result[0];
                Ba += ts.Result[1];
                Ga += ts.Result[2];
            }

            Ra = Ra / (source.Width * source.Height);
            Ba = Ba / (source.Width * source.Height);
            Ga = Ga / (source.Width * source.Height);

            Avg = (Ra + Ba + Ga) / 3;

            Parallel.For(0, source.Width, i =>
            {
                Parallel.For(0, source.Height, j =>
                {
                    Color color = source[i, j];
                    Color newcolor =
                        Color.FromArgb(
                            Norm(color.R * Avg / Ra),
                            Norm(color.G * Avg / Ga),
                            Norm(color.B * Avg / Ba));
                    dist[i, j] = newcolor;
                });
            });
            object img = dist;
            return (T)img;
        }

        private static double[] GetSumRgb(IMyImage source, int x1, int y1, int x2, int y2)
        {
            var res = new double[3];
            for (var i = x1; i < x2; i++)
            {
                for (var j = y1; j < y2; j++)
                {
                    var color = source[i, j];
                    res[0] += color.R;
                    res[1] += color.B;
                    res[2] += color.G;
                }
            }
            return res;
        }
    }
}
