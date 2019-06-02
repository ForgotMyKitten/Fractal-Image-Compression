using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalImageCompression
{
    public class Coder
    {
        private const int numberOfRotations = 8;
        private const int numberOfParameters = 5;
        private int numberOfParts;
        private const int coefficientOfIncrease = 1;
        private RotateFlipType[] rotate = new RotateFlipType[] {
                RotateFlipType.RotateNoneFlipNone,
                RotateFlipType.Rotate90FlipNone,
                RotateFlipType.Rotate180FlipNone,
                RotateFlipType.Rotate270FlipNone,
                RotateFlipType.RotateNoneFlipX,
                RotateFlipType.Rotate270FlipX,
                RotateFlipType.Rotate180FlipX,
                RotateFlipType.Rotate90FlipX
            };

        //кодирование изображения
        public void Compress(Bitmap image, IProgress<int> progress = null, CancellationToken? token = null)
        {
            if (progress == null)
                progress = new Progress<int>();
            if (token == null)
                token = new CancellationToken();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Bitmap[,] parts = SplitImage(image, out numberOfParts);
            Bitmap[,,] domainBlocks;
            int partOffsetX = image.Width / Data.RankBlockSize / numberOfParts;
            int partOffsetY = image.Height / Data.RankBlockSize / numberOfParts;
            var bbOriginal = new BufferedBitmap(image);

            if (bbOriginal.IsGrey())
            {
                Data.ColorFlag = 1;
            }
            else
            {
                Data.ColorFlag = 3;
            }
            bbOriginal.Unlock();

            double[] contrast = new double[Data.ColorFlag];
            double[] brightness = new double[Data.ColorFlag];
            double[] metric = new double[Data.ColorFlag];
            double[] min = new double[Data.ColorFlag];
            double[,,,] compressedData = new double[Data.ColorFlag, bbOriginal.Width / Data.RankBlockSize,
                bbOriginal.Height / Data.RankBlockSize, numberOfParameters];
            double controlValue = (bbOriginal.Width / Data.RankBlockSize) * (bbOriginal.Height / Data.RankBlockSize) / 100.0;
            double count = 0;
            int value = 1;
            double mse = 0;

            for (int partX = 0; partX < numberOfParts; partX++)
            {
                for (int partY = 0; partY < numberOfParts; partY++)
                {
                    domainBlocks = CreateAllDomainBlocks(parts[partX, partY]);
                    bbOriginal = new BufferedBitmap(parts[partX, partY]);

                    for (int i = 0; i * Data.RankBlockSize != bbOriginal.Width; i++)
                    {
                        for (int j = 0; j * Data.RankBlockSize != bbOriginal.Height; j++)
                        {
                            if ((bool)token?.IsCancellationRequested)
                            {
                                progress.Report(0);
                                MessageBox.Show("Операция прервана");
                                return;
                            }

                            int indexX = i + partX * partOffsetX;
                            int indexY = j + partY * partOffsetY;
                            Bitmap rankBlock = SelectBlockFromImage(bbOriginal, i, j, Data.RankBlockSize);
                            if(min[0] != 0)
                            {
                                for (int k = 0; k < Data.ColorFlag; k++)
                                    mse += min[k];
                            }
                            for (int k = 0; k < Data.ColorFlag; k++)
                                min[k] = double.PositiveInfinity;
                            var meanRankBlock = Metric.Mean(rankBlock);

                            for (int p = 0; p < parts[partX, partY].Width / Data.DomainBlockSize; p++)
                            {
                                for (int q = 0; q < parts[partX, partY].Height / Data.DomainBlockSize; q++)
                                {
                                    var meanDomainBlock = Metric.Mean(domainBlocks[p, q, 0]);

                                    for (int rotation = 0; rotation < numberOfRotations; rotation++)
                                    {
                                        contrast = Metric.GetContrast(rankBlock, domainBlocks[p, q, rotation], meanRankBlock, meanDomainBlock);
                                        brightness = Metric.GetBrightness(contrast, meanRankBlock, meanDomainBlock);
                                        metric = Metric.GetMetric(rankBlock, domainBlocks[p, q, rotation], contrast, brightness);

                                        for (int m = 0; m < Data.ColorFlag; m++)
                                        {
                                            if (metric[m] < min[m])
                                            {
                                                min[m] = metric[m];
                                                double[] mas = new double[] { p + partX * partOffsetX / Data.Factor, q + partY * partOffsetY / Data.Factor, rotation, contrast[m], brightness[m] };

                                                for (int k = 0; k < mas.Length; k++)
                                                {
                                                    compressedData[m, indexX, indexY, k] = mas[k];
                                                }
                                            }
                                        }
                                        //if (Data.FastCompression && min[0] <= 100)
                                            //break;
                                    }
                                    //if (Data.FastCompression && min[0] <= 100)
                                        //break;
                                }
                                count += 1.0 / domainBlocks.GetLength(1);
                                if (count >= controlValue)
                                {
                                    if (value > 100)
                                        value = 100;
                                    progress.Report(value);
                                    value++;
                                    count -= controlValue;
                                    count = Math.Round(count, 2);
                                }
                                //if (Data.FastCompression && min[0] <= 100)
                                    //break;
                            }
                        }
                    }
                }
            }
            mse = mse / ((image.Width / Data.RankBlockSize) * (image.Height / Data.RankBlockSize));
            bbOriginal.Unlock();
            double psnr = 10 * Math.Log10(image.Width * image.Height * Math.Pow(byte.MaxValue, 2) * Data.ColorFlag / mse);
            WriteData(compressedData, image.Width, image.Height);
            sw.Stop();
            MessageBox.Show("Сжатие завершено\r\nВремя: " + (sw.ElapsedMilliseconds / 1000.0).ToString());
            progress.Report(0);
        }

        //разбиение изображения на области
        public Bitmap[,] SplitImage(Bitmap image, out int numberOfParts)
        {
            numberOfParts = Data.FastCompression ? 2 : 1;
            Bitmap[,] parts = new Bitmap[numberOfParts, numberOfParts];
            BufferedBitmap bb = new BufferedBitmap(image);
            for (int offsetX = 0; offsetX * (image.Width / numberOfParts) != image.Width; offsetX++)
            {
                for (int offsetY = 0; offsetY * (image.Height / numberOfParts) != image.Height; offsetY++)
                {
                    parts[offsetX, offsetY] = SelectBlockFromImage(bb, offsetX, offsetY, image.Width / numberOfParts);
                }
            }
            bb.Unlock();
            return parts;
        }

        //изменение размера изображения
        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        //создание доменных блоков
        public Bitmap[,,] CreateAllDomainBlocks(Bitmap image)
        {
            int offsetX = 0;
            int offsetY = 0;
            Bitmap[,,] domainBlocks = new Bitmap[image.Width / Data.DomainBlockSize, image.Height / Data.DomainBlockSize, numberOfRotations];
            Bitmap reducedImage = ResizeImage(image, image.Width / Data.Factor, image.Height / Data.Factor);
            var bbReduced = new BufferedBitmap(reducedImage);

            //создаем множество доменных блоков
            for (offsetX = 0; offsetX * Data.RankBlockSize != bbReduced.Width; offsetX++)
            {
                for (offsetY = 0; offsetY * Data.RankBlockSize != bbReduced.Height; offsetY++)
                {
                    //вычленяем доменный блок из сжатой картинки
                    domainBlocks[offsetX, offsetY, 0] = SelectBlockFromImage(bbReduced, offsetX, offsetY, Data.RankBlockSize);

                    //для текущего доменного блока создаем все возможные варианты поворотов
                    for (int i = 1; i < numberOfRotations; i++)
                    {
                        domainBlocks[offsetX, offsetY, i] = new Bitmap(domainBlocks[offsetX, offsetY, i - 1]);
                        if (i != 4)
                            domainBlocks[offsetX, offsetY, i].RotateFlip(RotateFlipType.Rotate90FlipNone);
                        else
                            domainBlocks[offsetX, offsetY, i].RotateFlip(RotateFlipType.Rotate90FlipX);
                    }
                }
            }
            bbReduced.Unlock();
            return domainBlocks;
        }

        //вычленение блока из изображения
        public Bitmap SelectBlockFromImage(BufferedBitmap bb, int offsetX, int offsetY, int blockSize)
        {
            int x, y;
            var bbTemp = new BufferedBitmap(new Bitmap(blockSize, blockSize));

            x = offsetX * blockSize;
            for (int xDomain = 0; xDomain < blockSize; xDomain++)
            {
                y = offsetY * blockSize;
                for (int yDomain = 0; yDomain < blockSize; yDomain++)
                {
                    bbTemp.SetPixel(xDomain, yDomain, bb.GetPixel(x, y));
                    y++;
                }
                x++;
            }

            bbTemp.Unlock();
            return bbTemp.Bitmap;
        }

        //декодирование изображения
        public Bitmap[] Decompress(string path, int numberOfIterations, bool imageFlag)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            int width, height;
            var compressedData = ReadData(path, out width, out height);
            Bitmap[] images = new Bitmap[numberOfIterations];

            if (!imageFlag)
            {
                try
                {
                    OpenFileDialog opf = new OpenFileDialog();
                    if (opf.ShowDialog() == DialogResult.OK)
                    {
                        images[0] = ResizeImage(new Bitmap(opf.FileName), width, height);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Файл не является картинкой");
                }
            }
            else
            {
                images[0] = CreateRandomImage(width, height);
            }

            Bitmap image = new Bitmap(images[0]);
            Bitmap reduced = new Bitmap(ResizeImage(image, width / 2, height / 2));
            Bitmap restored = new Bitmap(image);
            Bitmap temp = new Bitmap(image);
            var bbOriginal = new BufferedBitmap(image);
            var bbReduced = new BufferedBitmap(reduced);

            for (int nb = 1; nb < numberOfIterations; nb++)
            {
                for (int i = 0; i < compressedData.GetLength(0); i++)
                {
                    for (int x = 0; x < compressedData.GetLength(1); x++)
                    {
                        for (int y = 0; y < compressedData.GetLength(2); y++)
                        {
                            var offsetX = (int)compressedData[i, x, y, 0];
                            var offsetY = (int)compressedData[i, x, y, 1];
                            var indexRotation = (int)compressedData[i, x, y, 2];
                            var contrast = compressedData[i, x, y, 3];
                            var brightness = compressedData[i, x, y, 4];

                            var domainBlock = SelectBlockFromImage(bbReduced, offsetX, offsetY, Data.RankBlockSize);
                            domainBlock.RotateFlip(rotate[indexRotation]);
                            var bbDomainBlock = new BufferedBitmap(domainBlock);

                            for (int domainX = 0; domainX < Data.RankBlockSize; domainX++)
                            {
                                for (int domainY = 0; domainY < Data.RankBlockSize; domainY++)
                                {
                                    double pixel = bbDomainBlock.GetColorOfPixel(domainX, domainY, i);
                                    pixel = contrast * pixel + brightness;
                                    pixel = Math.Round(pixel);
                                    if (pixel < byte.MinValue)
                                        pixel = 0;
                                    if (pixel > byte.MaxValue)
                                        pixel = 255;

                                    if (i == 0)
                                    {
                                        bbOriginal.SetPixel(x * Data.RankBlockSize + domainX, y * Data.RankBlockSize + domainY,
                                            Color.FromArgb((int)pixel, (int)pixel, (int)pixel));
                                        continue;
                                    }
                                    var color = temp.GetPixel(x * Data.RankBlockSize + domainX, y * Data.RankBlockSize + domainY);
                                    if (i == 1)
                                    {
                                        bbOriginal.SetPixel(x * Data.RankBlockSize + domainX, y * Data.RankBlockSize + domainY,
                                            Color.FromArgb((int)pixel, (int)pixel, color.B));
                                        continue;
                                    }
                                    if (i == 2)
                                    {
                                        bbOriginal.SetPixel(x * Data.RankBlockSize + domainX, y * Data.RankBlockSize + domainY,
                                            Color.FromArgb((int)pixel, color.G, color.B));
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    bbOriginal.Unlock();

                    image = bbOriginal.Bitmap;
                    temp = new Bitmap(image);
                    bbOriginal = new BufferedBitmap(image);
                }
                bbOriginal.Unlock();
                bbReduced.Unlock();

                image = bbOriginal.Bitmap;
                reduced = new Bitmap(ResizeImage(image, width / 2, height / 2));
                images[nb] = new Bitmap(image);

                var a = new Bitmap(image);
                a.Save(nb + ".jpg");

                bbOriginal = new BufferedBitmap(image);
                bbReduced = new BufferedBitmap(reduced);
            }
            image.Save(Path.GetDirectoryName(Data.Path) + "//" + Path.GetFileNameWithoutExtension(Data.Path) + "-restored.png");
            sw.Stop();
            MessageBox.Show("Восстановление завершено\r\nВремя: " + (sw.ElapsedMilliseconds/ 1000.0).ToString());
            return images;
        }

        //создание случайного изображения
        public Bitmap CreateRandomImage(int width, int height)
        {
            BufferedBitmap bb = new BufferedBitmap(new Bitmap(width, height));
            Random r = new Random();
            byte color;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    color = (byte)r.Next(0, 255);
                    bb.SetPixel(i, j, Color.FromArgb(color, color, color));
                }
            }
            bb.Unlock();
            return bb.Bitmap;
        }

        //чтение из сжатого файла
        public double[,,,] ReadData(string path, out int width, out int height)
        {
            var data = File.ReadAllLines(path, Encoding.GetEncoding(1251));
            var str = data[0].Split(' ');
            Data.ColorFlag = int.Parse(str[0]);
            Data.RankBlockSize = int.Parse(str[1]) * coefficientOfIncrease;
            Data.DomainBlockSize = int.Parse(str[2]) * coefficientOfIncrease;
            width = int.Parse(str[3]) * coefficientOfIncrease;
            height = int.Parse(str[4]) * coefficientOfIncrease;

            int count = 1;
            var compressedData = new double[Data.ColorFlag, width / Data.RankBlockSize, height / Data.RankBlockSize, numberOfParameters];
            for (int i = 0; i < compressedData.GetLength(0); i++)
            {
                for (int x = 0; x < compressedData.GetLength(1); x++)
                {
                    for (int y = 0; y < compressedData.GetLength(2); y++)
                    {
                        str = data[count++].Split(' ');
                        for (int c = 0; c < compressedData.GetLength(3); c++)
                        {
                            compressedData[i, x, y, c] = double.Parse(str[c]);
                        }
                    }
                }
            }
            return compressedData;
        }

        //запись в сжатый файл
        public void WriteData(double[,,,] compressedData, int width, int height)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Data.ColorFlag + " " + Data.RankBlockSize + " " + Data.DomainBlockSize + " " + width + " " + height + "\r\n");

            for (int i = 0; i < compressedData.GetLength(0); i++)
            {
                for (int x = 0; x < compressedData.GetLength(1); x++)
                {
                    for (int y = 0; y < compressedData.GetLength(2); y++)
                    {
                        for (int c = 0; c < compressedData.GetLength(3); c++)
                        {
                            sb.Append(compressedData[i, x, y, c].ToString());
                            if (c != numberOfParameters - 1)
                                sb.Append(" ");
                            else
                                sb.Append("\r\n");
                        }
                    }
                }
            }

            File.WriteAllText(Path.GetDirectoryName(Data.Path) + "//" + Path.GetFileNameWithoutExtension(Data.Path) + ".file", sb.ToString(), Encoding.GetEncoding(1251));
        }

    }
}
