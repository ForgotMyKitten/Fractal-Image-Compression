using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalImageCompression
{
    public static class Metric
    {
        //подсчет среднего значения пикселов в блоке
        public static double[] Mean(Bitmap block)
        {
            BufferedBitmap bbBlock = new BufferedBitmap(block);
            double[] mean = new double[Data.ColorFlag];

            for (int k = 0; k < Data.ColorFlag; k++)
            {
                mean[k] = 0;
                for (int i = 0; i < bbBlock.Height; i++)
                {
                    for (int j = 0; j < bbBlock.Width; j++)
                    {
                        mean[k] += bbBlock.GetColorOfPixel(i, j, k);
                    }
                }
                mean[k] = mean[k] / (Data.RankBlockSize * Data.RankBlockSize);
            }
            bbBlock.Unlock();
            return mean;
        }

        //подсчет значения метрики
        public static double[] GetMetric(Bitmap rankBlock, Bitmap domainBlock, double[] contrast, double[] brightness)
        {
            BufferedBitmap bbRankBlock = new BufferedBitmap(rankBlock);
            BufferedBitmap bbDomainBlock = new BufferedBitmap(domainBlock);
            double[] metric = new double[Data.ColorFlag];

            for (int k = 0; k < Data.ColorFlag; k++)
            {
                metric[k] = 0;
                for (int i = 0; i < rankBlock.Height; i++)
                {
                    for (int j = 0; j < rankBlock.Width; j++)
                    {
                        metric[k] += Math.Pow(contrast[k] * bbDomainBlock.GetColorOfPixel(i, j, k)
                            + brightness[k] - bbRankBlock.GetColorOfPixel(i, j, k), 2);
                    }
                }
            }
            bbRankBlock.Unlock();
            bbDomainBlock.Unlock();
            return metric;
        }

        //получение значения средней контрастности
        public static double[] GetContrast(Bitmap rankBlock, Bitmap domainBlock, double[] meanRankBlock, double[] meanDomainBlock)
        {
            double[] alpha = GetAlpha(rankBlock, domainBlock, meanRankBlock, meanDomainBlock);
            double[] beta = GetBeta(domainBlock, meanDomainBlock);
            double[] contrast = new double[Data.ColorFlag];

            for (int i = 0; i < Data.ColorFlag; i++)
            {
                contrast[i] = alpha[i] / beta[i];
            }
            return contrast;
        }

        //получение значения средней яркости
        public static double[] GetBrightness(double[] contrast, double[] meanRankBlock, double[] meanDomainBlock)
        {
            double[] brightness = new double[Data.ColorFlag];

            for (int i = 0; i < Data.ColorFlag; i++)
            {
                brightness[i] = meanRankBlock[i] - contrast[i] * meanDomainBlock[i];
            }
            return brightness;
        }

        //подсчет величины корреляции
        public static double[] GetAlpha(Bitmap rankBlock, Bitmap domainBlock, double[] meanRankBlock, double[] meanDomainBlock)
        {
            BufferedBitmap bbRankBlock = new BufferedBitmap(rankBlock);
            BufferedBitmap bbDomainBlock = new BufferedBitmap(domainBlock);
            double[] alpha = new double[Data.ColorFlag];
            double temp = 0;

            for (int k = 0; k < Data.ColorFlag; k++)
            {
                alpha[k] = 0;
                for (int i = 0; i < rankBlock.Height; i++)
                {
                    for (int j = 0; j < rankBlock.Width; j++)
                    {
                        temp = (bbDomainBlock.GetColorOfPixel(i, j, k) - meanDomainBlock[k]) *
                            (bbRankBlock.GetColorOfPixel(i, j, k) - meanRankBlock[k]);
                        alpha[k] += temp;
                    }
                }
            }
            bbRankBlock.Unlock();
            bbDomainBlock.Unlock();
            return alpha;
        }

        //подсчет дисперсии значений пикселов
        public static double[] GetBeta(Bitmap domainBlock, double[] mean)
        {
            BufferedBitmap bbDomainBlock = new BufferedBitmap(domainBlock);
            double[] beta = new double[Data.ColorFlag];

            for (int k = 0; k < Data.ColorFlag; k++)
            {
                beta[k] = 0;
                for (int i = 0; i < domainBlock.Height; i++)
                {
                    for (int j = 0; j < domainBlock.Width; j++)
                    {
                        beta[k] += Math.Pow(bbDomainBlock.GetColorOfPixel(i, j, k) - mean[k], 2);
                    }
                }
            }
            bbDomainBlock.Unlock();
            return beta;
        }
    }
}
