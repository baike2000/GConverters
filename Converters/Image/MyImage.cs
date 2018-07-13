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
        private void SetPixel(int x, int y, Color color)
        {
            _image[x, y] = color;
        }

        public MyImage()
        {
            Width = 0;
            Height = 0;
            _image = new Color[Width, Height];
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
        public void ConvertTo<T>(T bitmap) where T : System.Drawing.Image
        {
            object ig = bitmap;
            var img = (System.Drawing.Image)ig;
            if (!(img is Bitmap))
            {
                throw new System.Exception("Не тот формат");
            }
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    ((Bitmap)img).SetPixel(i, j, _image[i, j]);
                }
            }
        }
    }
}
