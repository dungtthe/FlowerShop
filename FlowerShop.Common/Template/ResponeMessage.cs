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
    }
}
