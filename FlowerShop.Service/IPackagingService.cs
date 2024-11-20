using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
    public interface IPackagingService
    {
        Task<IEnumerable<Packaging>> GetAllPackagingAsync();
        Task<Packaging> FindOneById(int id);
		Task<Packaging> UpdateAsync(Packaging packaging);
		Task<PopupViewModel> Delete(Packaging packaging);
        Task<Packaging> AddAsync(Packaging packaging);
    }
}
