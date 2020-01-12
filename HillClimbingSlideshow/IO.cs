using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HillClimbingSlideshow
{

    class IO
    {
        public static string ReadInputList(string path)
        {
            List<Photo> photos = new List<Photo>();
            StreamReader reader = new StreamReader(path); 
            string[] firstLine = reader.ReadLine().Split(' ');
            int index = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                char type = line[0];
                int numritags = line[1];
                List<string> tags = line.Split(' ').ToList();
                tags.RemoveRange(0, 2);
                Photo photo = new Photo();
                if (type == 'H')
                {
                    photo.pozita = "H";
                    photo.numri_tagjeve = numritags;
                    photo.tags = tags;
                    photo.index = index++; 
                    Input.list_horizontal_photo.Add(photo);


                }
                else
                {
                    photo.pozita = "V";
                    photo.numri_tagjeve = numritags;
                    photo.tags = tags;
                    photo.index = index++;
                    Input.list_vertical_photo.Add(photo);

                }
            }
            return path.Split('/').Last(); ;
        }

        public static void WriteToFile(List<Slide> slidephoto, string outputPath)
        {
            string dirName = AppDomain.CurrentDomain.BaseDirectory; // Starting Dir
            FileInfo fileInfo = new FileInfo(dirName);
            DirectoryInfo parentDir = fileInfo.Directory.Parent.Parent;
            string parentDirName = parentDir.FullName + "\\Out\\" + outputPath;
            if (!File.Exists(parentDirName))
            { 
                File.Create(parentDirName).Dispose();

            } 
            using (TextWriter tw = new StreamWriter(parentDirName))
            {
                tw.WriteLine(slidephoto.Count);
                foreach (Photo photo in slidephoto)
                {
                    string s = "";
                    if (photo.pozita == "H")
                    {
                        s += photo.index;
                    }
                    else if (photo.pozita == "V")
                    {
                        s += $"{photo.indexLeft} {photo.indexRight}";
                    }

                    tw.WriteLine(s);
                }
            }
            Console.WriteLine("Finished");
        }
    }
}
