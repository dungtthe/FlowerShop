using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Common.Helpers
{
    public static class Utils
    {
        public static string AddPhotoForProduct(string fileName, string imgPre)
        {

            if (string.IsNullOrEmpty(imgPre))
            {
                imgPre = "[\"no_img.png\"]";
            }

            string[] s = JsonConvert.DeserializeObject<string[]>(imgPre);
            List<string> images = new List<string>();

            if (!string.IsNullOrEmpty(fileName))
            {
                images.Add(fileName);
            }

            foreach (var item in s)
            {
                if (!item.Equals("no_img.png"))
                {
                    images.Add(item);
                }
            }

            images.Add("no_img.png");

            return JsonConvert.SerializeObject(images);
        }


        public static string RemovePhotoForProduct(string fileName, string imgPre)
        {
            if (string.IsNullOrEmpty(imgPre))
            {
                imgPre = "[\"no_img.png\"]";
            }

            string[] s = JsonConvert.DeserializeObject<string[]>(imgPre);
            List<string> images = new List<string>(s);

            images.Remove(fileName);

            if (images.Count == 0)
            {
                images.Add("no_img.png");
            }

            return JsonConvert.SerializeObject(images);
        }

        public static string SetDefaultProductImage(string fileName, string imgPre)
        {
            if (string.IsNullOrEmpty(imgPre))
            {
                 return "[\"no_img.png\"]";
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return imgPre;
            }

            string[] s = JsonConvert.DeserializeObject<string[]>(imgPre);
            List<string> images = new List<string>();
            images.Add(fileName);
            foreach(var im in s)
            {
                if (!(fileName == im))
                {
                    images.Add(im);
                }
            }
            return JsonConvert.SerializeObject(images);
        }



        public static string GenerateRandomString(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyz123456789";
            Random random = new Random();
            char[] randomString = new char[length];

            for (int i = 0; i < length; i++)
            {
                randomString[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(randomString);
        }

        public static string GenerateRandomToken(List<string> tokensOld,int lenght)
        {
            bool flag = true;
            var tokenTemp = "";
            while (flag)
            {
                flag=false;
                 tokenTemp = GenerateRandomString(lenght);
                foreach (string token in tokensOld)
                {
                    if(token == tokenTemp)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return tokenTemp;
        }

      
    }
}
