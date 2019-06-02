using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalImageCompression
{
    public unsafe class BufferedBitmap
    {
        Bitmap _bmp;
        BitmapData _bd;
        bool _locked;
        byte* pStart;

        public BufferedBitmap(Bitmap bmp, bool @lock)
        {
            _bmp = bmp;

            if (@lock)
                Lock();
        }

        public BufferedBitmap(Bitmap bmp)
            : this(bmp, true)
        {

        }

        public void Lock()
        {
            if (!_locked)
            {
                _bd = _bmp.LockBits(
                    new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                    ImageLockMode.ReadWrite,
                    _bmp.PixelFormat
                );

                pStart = (byte*)_bd.Scan0;
                _locked = true;
            }
        }

        public void Unlock()
        {
            if (!_locked)
                return;

            _bmp.UnlockBits(_bd);

            _locked = false;
        }

        public Bitmap Bitmap
        {
            get
            {
                return (_locked ? null : _bmp);
            }
            set
            {
                if (_locked)
                    throw new Exception("Image locked!");
                if (value == null)
                    throw new NullReferenceException();

                _bmp = value;
            }
        }

        public int Width
        {
            get
            {
                return _bmp.Width;
            }
        }

        public int Height
        {
            get
            {
                return _bmp.Height;
            }
        }

        public void SetPixel(int x, int y, Color clr)
        {
            if (!_locked)
                throw new Exception();

            switch (_bd.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    SetPixel24(x, y, clr);
                    break;
                case PixelFormat.Format32bppArgb:
                    SetPixel32(x, y, clr);
                    break;
            }
        }

        public void SetPixel24(int x, int y, Color clr)
        {
            var pMem = pStart + x * 3 + y * _bd.Stride;

            *pMem = clr.B;
            *(pMem + 1) = clr.G;
            *(pMem + 2) = clr.R;
        }

        public void SetPixel32(int x, int y, Color clr)
        {
            var pMem = pStart + x * 4 + y * _bd.Stride;

            *pMem = clr.B;
            *(pMem + 1) = clr.G;
            *(pMem + 2) = clr.R;
            *(pMem + 3) = clr.A;
        }

        public Color GetPixel(int x, int y)
        {
            if (!_locked)
                throw new Exception();

            switch (_bd.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    return GetPixel24(x, y);
                case PixelFormat.Format32bppArgb:
                    return GetPixel32(x, y);
                default:
                    throw new NotImplementedException();
            }
        }

        public Color GetPixel8(int x, int y)
        {
            var pMem = pStart + x + y * _bd.Stride;

            return Color.FromArgb(
                *pMem
            );
        }

        public Color GetPixel24(int x, int y)
        {
            var pMem = pStart + x * 3 + y * _bd.Stride;

            return Color.FromArgb(
                *(pMem + 2),
                *(pMem + 1),
                *pMem
            );
        }

        public Color GetPixel32(int x, int y)
        {
            var pMem = pStart + x * 4 + y * _bd.Stride;

            return Color.FromArgb(
                *(pMem + 3),
                *(pMem + 2),
                *(pMem + 1),
                *pMem
            );
        }

        public byte GetColorOfPixel(int x, int y, int index)
        {
            if (!_locked)
                throw new Exception();

            switch (_bd.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    return GetColorOfPixel(x, y, index, 3);
                case PixelFormat.Format32bppArgb:
                    return GetColorOfPixel(x, y, index, 4);
                default:
                    throw new NotImplementedException();
            }
        }

        private byte GetColorOfPixel(int x, int y, int index, byte pixelFormat)
        {
            var pMem = pStart + x * pixelFormat + y * _bd.Stride;

            return *(pMem + index);
        }

        public bool IsGrey()
        {
            var pEnd = pStart + _bd.Stride * _bd.Height;
            var pMem = pStart;
            byte index = 0;

            switch (_bd.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    index = 3;
                    break;
                case PixelFormat.Format32bppArgb:
                    index = 4;
                    break;
                default:
                    throw new NotImplementedException();
            }

            while (pMem != pEnd)
            {
                if (*pMem != *(pMem + 1) && 
                    *(pMem + 1) != *(pMem + 2))
                {
                    return false;
                }
                else
                {
                    pMem += index;
                }
            }
            return true;
        }

        public bool IsLocked
        {
            get
            {
                return _locked;
            }
        }
    }
}
