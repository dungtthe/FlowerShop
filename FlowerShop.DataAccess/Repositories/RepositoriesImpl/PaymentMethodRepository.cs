using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class PaymentMethodRepository : RepositoryBase<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
