using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

    }
}
