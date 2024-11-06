using FlowerShop.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess
{
    public class FlowerShopContext : IdentityDbContext<AppUser>
    {

    }
}
