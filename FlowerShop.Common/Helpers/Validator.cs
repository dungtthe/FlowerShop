using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlowerShop.Common.Helpers
{
    public static class Validator
    {
        // Hàm kiểm tra email
        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy cho email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy cho số điện thoại Việt Nam
            string pattern = @"^0\d{9,10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
