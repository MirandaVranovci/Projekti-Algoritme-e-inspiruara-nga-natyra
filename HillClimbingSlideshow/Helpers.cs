using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbingSlideshow
{
     class Helpers
    {
        public static int RandomFrom { get; set; } = 400;
        public static int RandomTo { get; set; } = 8000;
        public static int Iterations { get; set; }
        public static int CountVerticals { get; set; } = 10;
        public static int PopSize { get;  } = 5;
        public static double MutationRate { get; set; } = 0.2;
        public static int IterationCount { get; set; } = 5;
        public static int ts { get; set; } = 5;
    }
}
