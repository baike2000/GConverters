using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters.Helpers
{
    public static class DivRange
    {
        public static List<int[]> Divide(int minX, int minY, int maxX, int maxY, int numofpieces)
        {
            var result = new List<int[]>();
            var width = Math.Abs(maxX - minX) / (int)Math.Sqrt(numofpieces);
            var height = Math.Abs(maxY - minY) / (int)Math.Sqrt(numofpieces);
            int curminX = minX;
            int curminY = minY;
            for (int i = 0; i < numofpieces; i++)
            {
                var a = new[]{ curminX , curminY , curminX + width, curminY + height };
                curminX += width;
                if (curminX >= maxX)
                {
                    curminX = minX;
                    curminY += height;
                }
                result.Add(a);
            }
            return result;
        }
    }
}
