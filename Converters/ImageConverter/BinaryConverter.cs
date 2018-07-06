using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Converters.Exceptions;
using Converters.Image;
using ConvertInterfaces.Enum;
using ConvertInterfaces;

namespace Converters.ImageConverter
{
    public class BinaryConverter<T> : MyImageConverter<T>, IMyImageConverterWithParams<T> where T:IMyImage
    {
        public BinaryConverter()
        {
            NumberOfParams = 1;
            TypeOfParams = typeof(int);
            Name = "Оттенки серого";
        }
        public T Convert(T source, params object[] prms)
        {
            if (prms.Length != 1)
                throw new Exception("Неверное количсетво параметров");
            if (!(prms[0] is int))
                throw new Exception("Параметр должен быть int");
            int bound = (int)prms[0];
            if (bound < 0 || bound > 255)
                throw new BoundException<int>(bound, "Параметр должен быть в диапазоне от 0 до 255");
            var converter = new GrayscaleConverter<T>();
            var dst = new MyImage(source.Width, source.Height);
            object img = dst;
            img = converter.Convert(source);
            dst = (MyImage)img;
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    int rescolor = dst[i, j].R > bound ? 255 : 0;
                    dst[i, j] = Color.FromArgb(rescolor, rescolor, rescolor);
                }
            }
            img = dst;
            return (T)img;
        }
    }
}
