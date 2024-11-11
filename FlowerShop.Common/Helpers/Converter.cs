using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Common.Helpers
{
    public static class Converter
    {
        public static string ConvertToVietnameseDong(decimal amount)
        {
            return string.Format("{0:N0} đ", amount);
        }

    }
}
