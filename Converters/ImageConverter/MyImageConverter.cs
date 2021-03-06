﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converters.Image;
using ConvertInterfaces;
using ConvertInterfaces.Enum;

namespace Converters.ImageConverter
{
    public abstract class MyImageConverter<T> where T : IMyImage, new()
    {
        public string Name { get; protected set; }
        public int NumberOfParams { get; protected set; }
        public Type TypeOfParams { get; protected set; }
        public ConverterEnum ConverterType { get; protected set; }
        public List<string> ParamNames { get; protected set; }
        public List<object> Controls { get; private set; }

        protected MyImageConverter()
        {
            NumberOfParams = 0;
            TypeOfParams = null;
            Name = "Empty";
            ConverterType = ConverterEnum.None;
            ParamNames = new List<string>();
            Controls = new List<object>();
        }
        protected virtual int Norm(double x)
        {
            return x > 255 ? 255 : (int)x;
        }

/*        private void ProcessUsingLockbitsAndUnsafeAndParallel(Bitmap processedBitmap)
        {
            unsafe
            {
                BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        currentLine[x] = (byte)oldBlue;
                        currentLine[x + 1] = (byte)oldGreen;
                        currentLine[x + 2] = (byte)oldRed;
                    }
                });
                processedBitmap.UnlockBits(bitmapData);
            }
        }*/

    }
}
