using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace HillClimbingSlideshow
{
    class Slide : Photo
    {

        public Slide(Photo photo)
        {
            pozita = "H";
            numri_tagjeve = photo.numri_tagjeve;
            index = photo.index;
            tags = photo.tags;
        }
        public Slide(Photo photo1, Photo photo2)
        {
            pozita = "V";
            int common = 0;
            photo1.tags.ToList().ForEach(v =>
            {
                if (photo2.tags.ToList().Contains(v)) common++;
            });
            numri_Tagjeve_l = photo1.numri_tagjeve;
            numri_Tagjeve_r = photo2.numri_tagjeve;
            tags = photo1.tags.Union(photo2.tags).ToList();
            tagsL = photo1.tags;
            tagsR = photo2.tags;
            indexLeft = photo1.index;
            indexRight = photo2.index;
        }
        public static List<Slide> PreProcess()
        {
            List<Slide> slides = new List<Slide>();
            List<Slide> slides_v = new List<Slide>();
            List<Photo> pozita_h = (from i in Input.list_horizontal_photo orderby i.numri_tagjeve descending select i).ToList();
            for (int i = 0; i < pozita_h.Count(); i++)
            {
                slides.Add(new Slide(Input.list_horizontal_photo[i]));
            }
            List<Photo> pozita_v = (from i in Input.list_vertical_photo orderby i.numri_tagjeve descending select i).ToList();
            int count = Helpers.CountVerticals;
            if(pozita_v.Count()>3)
            {
                SwapPhoto(Input.list_vertical_photo.ToList());
            }
          
            for (int i = 1; i < pozita_v.Count(); i += 2)
            {
                slides_v.Add(new Slide(Input.list_vertical_photo[i], Input.list_vertical_photo[i - 1]));

            }
            slides.AddRange(slides_v); 
            return slides;
        }


        public static List<Slide> Mutate(List<Slide> sliders)
        {
            Helpers.Iterations = sliders.Count();
            for (int i = 0; i < Helpers.Iterations - 1; i++)
            {
                Slide slide1 = sliders[i];
                int m = i + 1;
                int maxscore = 0;
                Random rnd = new Random();
                for (int j = 1; j < rnd.Next(Helpers.RandomFrom, Helpers.RandomTo) && (i + j) < Helpers.Iterations; j++)
                {
                  //  Console.WriteLine(rnd.Next(Helpers.RandomFrom, Helpers.RandomTo));
                    Slide slide2 = sliders[j + i];

                    int score = CompareTags(slide1, slide2);

                    if (score > maxscore)
                    {
                        maxscore = score;
                        m = i + j;
                    }
                }

               Swap(sliders, i + 1, m);
            }
            Random rnd1 = new Random();
            for (int i = 0; i < 20; i++)
            {
                if (rnd1.Next(10) > 5)
                {
                    RemoveSlide(rnd1.Next(1, sliders.Count() - 1), sliders);
                }
            }

            return sliders;
        }


        public static List<Photo> SwapPhoto(List<Photo> photo_v)
        {
            Helpers.Iterations = 300;

            for (int i = 0; i < Helpers.Iterations; i++)
            {
                Photo p1 = photo_v[i];
                int m = i + 1;
                int maxscore = 0;
                for (int j = 1; j < Helpers.Iterations && (i + j) < Helpers.Iterations; j++)
                {
                    Photo p2 = photo_v[j + i];

                    int score = GetScore(p1, p2);

                    if (score > maxscore)
                    {
                        maxscore = score;
                        m = i + j;
                    }
                   
                }

                 Swap(photo_v, i + 1, m);
            } 

            return photo_v;
        }

        public static int CalculateScore(List<Slide> slide)
        {
            int i = 1;
            int compare = 0; int sum = 0;
            Slide slide1, slide2;

            while (i < slide.Count())
            {
                slide1 = slide[i - 1];
                slide2 = slide[i];
                compare = CompareTags(slide1, slide2);
                sum += compare;
                i++;
            }
            return sum;
        }
        public static List<Slide> Copy(List<Slide> slides)
        {
            var slide = new List<Slide>(slides);
            return slide;
        }

    }
}
