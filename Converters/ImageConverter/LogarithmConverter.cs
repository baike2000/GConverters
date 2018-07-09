using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Exceptions;
using Converters.Image;
using ConvertInterfaces;

namespace Converters.ImageConverter
{
    public class LogarithmConverter<T> : MyImageConverter<T>,
        IMyImageConverterWithParams<T> where T : IMyImage

    {
        public LogarithmConverter()
        {
            NumberOfParams = 1;
            TypeOfParams = typeof(double);
            Name = "Логарифмическая коррекция";
            ConverterType = ConvertInterfaces.Enum.ConverterEnum.Logaritm;
            ParamNames = new List<string>() {"С"};
        }

        public T Convert(T source, params object[] prms)
        {
            if (prms.Length != 1)
                throw new Exception("Неверное количсетво параметров");
            if (!(prms[0] is double))
                throw new Exception("Параметр должен быть double");
            double c = (double)prms[0];
            if (Math.Abs(c) < 10e-9)
                throw new BoundException<double>(c, "Параметер не может быть равен нулю");
            var dst = new MyImage(source.Width, source.Height); ;
            for (int i = 0; i < dst.Width; i++)
            for (int j = 0; j < dst.Height; j++)
            {
                double dR = c * Math.Log(1 + source[i, j].R);
                double dG = c * Math.Log(1 + source[i, j].G);
                double dB = c * Math.Log(1 + source[i, j].B);
                dst[i, j] = Color.FromArgb(Norm(dR), Norm(dG), Norm(dB));
            }

            object img = dst;
            return (T)img;
        }
    }
}
