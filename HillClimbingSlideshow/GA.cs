using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillClimbingSlideshow 
{
    class GA  : IComparer<List<Slide>>
    { 
        readonly int PopSize;
        readonly double MutationRate;
        readonly int IterationCount;
        readonly List<List<Slide>> Individuals;
        readonly int ts;
        public GA()
        {
            this.PopSize = Helpers.PopSize;
            this.MutationRate = Helpers.MutationRate;
            this.IterationCount = Helpers.IterationCount;
            this.ts = Helpers.ts;
            this.Individuals = new List<List<Slide>>(Helpers.PopSize);
        }
        void Initialize()
        {
            for (int i = 0; i < PopSize; i++)
            { 
                List<Slide> s = Slide.PreProcess();
                 
                this.Individuals.Add(s);
            }

        }
        public List<Slide> Run()
        {
            var rnd = new Random();
            this.Initialize();

            for (int i = 0; i < this.IterationCount; i++)
            {
                var X = this.TurnamentSelect(this.ts);
                List<Slide> M = Slide.Copy(X);
                if (this.MutationRate < rnd.NextDouble())
                {
                  Slide.Mutate(M);
                } 
                Individuals.Sort(new GA());
                var min = Individuals[0];
                if (Slide.CalculateScore(M) >= Slide.CalculateScore(min))
                {
                    Individuals[0] = M;
                }
            }
            Individuals.Sort(new GA());
            var Max = Individuals[this.PopSize - 1];
            Console.WriteLine( Slide.CalculateScore(Max));
            return Max;
        }
        public List<Slide> TurnamentSelect(int t)
        { 
            var rnd = new Random();
            var randomIndividuals = new List<List<Slide>>(t);
            List<Slide> best = null;

            for (int i = 0; i < t; i++)
            {
                var index = rnd.Next(0, this.PopSize);
                var s = Individuals[index];

                if (best == null || Slide.CalculateScore(s)> Slide.CalculateScore(best))
                {
                    best = s;
                }
            } 
            return best;
        }
        public int Compare( List<Slide> x, List<Slide> y)
        {
            return Slide.CalculateScore(x).CompareTo(Slide.CalculateScore(y));
        }
    }

}
