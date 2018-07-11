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
    public class NonLinearConverter<T> : MyImageConverter<T>,
        IMyImageConverterWithParams<T> where T : IMyImage, new()

    {
        public NonLinearConverter()
        {
            NumberOfParams = 2;
            TypeOfParams = typeof(double);
            ConverterType = ConvertInterfaces.Enum.ConverterEnum.NonLinear;
            Name = "Нелинейная коррекция";
            ParamNames = new List<string>() { "С", "Альфа" };
        }
        public T Convert(T source, params object[] prms)
        {
            if (prms.Length != 2)
                throw new NumberOfArgsException(NumberOfParams, "Необходимо ровно два параметра");
            foreach(var item in prms)
            {
                if (!(item is double))
                    throw new Exception("Параметры должны быть double");
            }
            var dst = new MyImage(source.Width, source.Height); ;
            for (var i = 0; i < dst.Width; i++)
            for (var j = 0; j < dst.Height; j++)
            {
                double dR = (double)prms[0] * Math.Pow(source[i, j].R, (double)prms[1]);
                double dG = (double)prms[0] * Math.Pow(source[i, j].G, (double)prms[1]);
                double dB = (double)prms[0] * Math.Pow(source[i, j].B, (double)prms[1]);
                dst[i, j] = Color.FromArgb(Norm(dR), Norm(dG), Norm(dB));
            }

            object img = dst;
            return (T)img;
        }
    }
}
