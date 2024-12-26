using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Common.Helpers
{
    public static class Converter
    {
        public static string ConvertToVietnameseDong(decimal amount)
        {
            var cultureInfo = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            return string.Format(cultureInfo, "{0:N0} đ", amount);
        }


        public static byte[] ConvertStringToByteArray(string input)
        {
            try
            {


                // Xóa ký tự ',' cuối cùng nếu có
                if (input.EndsWith(","))
                {
                    input = input.Substring(0, input.Length - 1);
                }

                return input
                    .Split(',', StringSplitOptions.RemoveEmptyEntries) 
                    .Select(s => (byte.Parse(s.Trim())))         
                    .ToArray();                                        
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static Image ByteToImage(byte[] byteArray)
        {
            try
            {
                return Image.Load<Rgba32>(byteArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
