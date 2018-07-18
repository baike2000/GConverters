using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertInterfaces
{
    public interface IMyImage
    {
        int Width { get; }
        int Height { get; }
        Color this[int row, int column] { get; set; }
        Color Min { get; }
        Color Max { get; }
        void ConvertTo<T>(T bitmap) where T : System.Drawing.Image;
    }
}
