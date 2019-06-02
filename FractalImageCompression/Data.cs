using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalImageCompression
{
    public static class Data
    {
        public static int RankBlockSize { get; set; }
        public static int DomainBlockSize { get; set; }
        public static int Factor {
            get
            {
                return DomainBlockSize / RankBlockSize;
            }
        }
        public static string Path { get; set; }
        public static string FileName { get; set; }
        public static int ColorFlag { get; set; }
        public static bool FastCompression { get; set; }

        public static bool Validate()
        {
            return RankBlockSize < DomainBlockSize;
        }
    }
}
