﻿using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class SaleInvoiceDetailRepository : RepositoryBase<SaleInvoiceDetail>, ISaleInvoiceDetailRepository
    {
        public SaleInvoiceDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
