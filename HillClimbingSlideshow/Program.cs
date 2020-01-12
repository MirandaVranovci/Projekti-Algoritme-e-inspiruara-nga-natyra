using System;
using System.Collections.Generic;
using System.Threading;

namespace HillClimbingSlideshow
{
    class Program
    {
        static void Main(string[] args)
        {


            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            List<Slide> slide = new List<Slide>();
            string getpath = IO.ReadInputList("./Fajllat/c_memorable_moments.txt");
            // slide = Slide.PreProcess();
            GA g = new GA();
            slide = g.Run();
            IO.WriteToFile(slide, getpath);
            watch.Stop();
            Console.WriteLine($"Execution end Time: {watch.Elapsed}");

        }

    }

}
