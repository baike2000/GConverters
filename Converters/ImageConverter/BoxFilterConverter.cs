using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Exceptions;
using Converters.Image;
using ConvertInterfaces;
using ConvertInterfaces.Enum;

namespace Converters.ImageConverter
{
    public class BoxFilterConverter<T> : MyImageConverter<T>, 
                                         IMyImageConverterWithParams<T> where T:MyImage, new ()
    {
        public BoxFilterConverter()
        {
            NumberOfParams = 1;
            TypeOfParams = typeof(int);
            Name = "Медианный фильтр";
            ConverterType = ConverterEnum.Median;
            ParamNames = new List<string>() { "Размер фильтра" };
        }
        public T Convert(T source, params object[] prms)
        {
            if (prms.Length != 1)
                throw new Exception("Неверное количсетво параметров");
            if (!(prms[0] is int))
                throw new Exception("Параметр должен быть int");
            int k = (int)prms[0];
            if (k % 2 == 0)
                throw new Exception("Значение параметрв должено быть нечетным");
            var dst = new MyImage(source.Width, source.Height); ;
            Parallel.For(0, dst.Width, i =>
                {
                    Parallel.For(0, dst.Height, j =>
                    {
                        var filter = new List<Color>();
                        for (int x = -k / 2; x < k / 2; x++)
                        {
                            for (int y = -k / 2; y < k / 2; y++)
                            {
                                int cx = i + x;
                                if (i + x < 0 || i + x >= dst.Width)
                                {
                                    cx = i;
                                }
                                int cy = j + y;
                                if (j + y < 0 || j + y >= dst.Height)
                                {
                                    cy = j;
                                }
                                filter.Add(source[cx, cy]);
                            }
                        }
                        if (i >= dst.Width || j >= dst.Height)
                            throw new Exception("Выход за пределы");
                        dst[i,j] = BoxFileter(filter.ToArray());
                    });
                });
            object img = dst;
            return (T)img;
        }
        private Color BoxFileter(Color[] source)
        {
            var dst = new int[source.Length];
            var dc = new Dictionary<Color, int>();
            for (int i = 0; i < source.Length; i++)
            {
                int newcolor = (int) (0.299 * source[i].R +
                                      0.587 * source[i].G +
                                      0.114 * source[i].B);
                if (!dc.ContainsKey(source[i]))
                    dc.Add(source[i], newcolor);
                dst[i] = newcolor;
            }
            Array.Sort(dst);
            var result = Color.Black;
            foreach (var item in dc)
            {
                if (item.Value == dst[(dst.Length / 2)+1])
                {
                    result = item.Key;
                    break;
                }
            }
            return result;
        }
    }
}
