using System;
using System.Collections.Generic;
using System.Linq; 

namespace HillClimbingSlideshow
{
     
    class Photo  
    {
        
        public string pozita { get; set; }
        public int numri_tagjeve { get; set; }
        public List<String> tags { get; set; }
        public int index { get; set; }
        public int indexLeft { get; set; }
        public int indexRight { get; set; }
        public int numri_Tagjeve_l { get; set; }
        public int numri_Tagjeve_r { get; set; }
        public List<String>  tagsL { get; set; }
        public List<String> tagsR { get; set; }
        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        public static void RemoveSlide(int i, List<Slide> slider)
        {
            slider.RemoveAt(i);
        } 
        public static int CompareTags(Slide slide1, Slide slide2)
        {
            int common_tags = slide1.tags.Intersect(slide2.tags).Count();
            int only_in_photo1 = slide1.tags.Count() - common_tags;
            int only_in_photo2 = slide2.tags.Count() - common_tags;
            return Math.Min(common_tags, Math.Min(only_in_photo1, only_in_photo2));

        }

        public static int GetScore(Photo one, Photo second)
        {

            var Unic = one.tags.Intersect(second.tags).Count();
            var DiffOne = one.tags.Except(second.tags).Count();
            var DiffSecond = second.tags.Except(one.tags).Count();

            var sum = Math.Min(Unic, Math.Min(DiffOne, DiffSecond));

            return sum;
        }
    }

}
