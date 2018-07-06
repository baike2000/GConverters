using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConvertInterfaces;

namespace Converters.Image
{
    public class MyImage : IMyImage
    {
        private readonly Color[,] _image;
        private Color _max = Color.FromArgb(0, 0, 0);
        private Color _min = Color.FromArgb(0, 0, 0);

        private void CheckMinMax(Color color)
        {
            int maxR = _max.R, minR = _min.R, maxG = _max.G, minG = _min.G, maxB = _max.B, minB = _min.B;
            if (color.R > _max.R)
                maxR = color.R;
            if (color.B > _max.B)
                maxB = color.B;
            if (color.G > _max.G)
                maxG = color.G;
            if (color.R < _min.R)
                minR = color.R;
            if (color.G < _min.G)
                minG = color.G;
            if (color.B < _min.B)
                minB = color.B;
            _max = Color.FromArgb(maxR, maxG, maxB);
            _min = Color.FromArgb(minR, minG, minB);
        }

        private void SetPixel(int x, int y, Color color)
        {
            _image[x, y] = color;
            CheckMinMax(color);
        }
        public MyImage(int width, int height)
        {
            Width = width;
            Height = height;
            _image = new Color[Width, Height];
        }
        public MyImage(Bitmap bitmap) : this(bitmap.Width, bitmap.Height)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    SetPixel(i, j, bitmap.GetPixel(i, j));
                }
            }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color this[int row, int column]
        {
            get => _image[row, column];
            set => SetPixel(row, column, value);
        }
        public Color Max => _max;
        public Color Min => _min;

        public void ConvertTo<T>(T bitmap) where T : System.Drawing.Image
        {
            object ig = bitmap;
            var img = (System.Drawing.Image)ig;
            for (int i = 0; i < img.Width; i++)
                for (int j = 0; j < img.Height; j++)
                {
                    if (img is Bitmap)
                        ((Bitmap)img).SetPixel(i, j, _image[i, j]);
                    else
                    {
                        throw new System.Exception("Не тот формат");
                    }
                }
        }


    }
}
