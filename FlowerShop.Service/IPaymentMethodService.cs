using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IPaymentMethodService
    {
        Task<ICollection<PaymentMethod>> GetPaymentMethodsAsync();
        Task<PaymentMethod> UpdateAsync(PaymentMethod paymentMethod);
        Task<bool> DeleteAsync(int? id);

    }
}
