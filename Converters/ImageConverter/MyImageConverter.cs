using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Image;

namespace Converters.ImageConverter
{
    public abstract class MyImageConverter<T> where T : IMyImage
    {
        public string Name { get; protected set; }
        public int NumberOfParams { get; protected set; }
        public Type TypeOfParams { get; protected set; }

        protected MyImageConverter()
        {
            NumberOfParams = 0;
            TypeOfParams = null;
            Name = "Empty";
        }
        protected virtual int Norm(double x)
        {
            return x > 255 ? 255 : (int)x;
        }

    }
}
