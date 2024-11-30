using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Common.Template
{
    public class ResponeMessage
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public ResponeMessage() { }
        public ResponeMessage(int id, string? msg)
        {
            this.Id = id;
            this.Message = msg;
        }

        public static readonly int ERROR = 0;
        public static readonly int SUCCESS = 1;
        public static readonly int NOT_FOUND = 2;
        public static readonly string CO_LOI_XAY_RA = "Có lỗi xảy ra";
    }
}
