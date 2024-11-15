using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class PackagingService : IPackagingService
    {
        private readonly IPackagingRepository _packagingRepository;
        public PackagingService(IPackagingRepository packagingRepository)
        {
            _packagingRepository = packagingRepository;
        }

        public async Task<Packaging> FindOneById(int id)
        {
            var rs = (await _packagingRepository.GetSingleByIdAsync(id));
            if(rs==null || rs.IsDelete)
            {
                rs = null;
            }
            return rs;
        }

        public async Task<IEnumerable<Packaging>> GetAllPackagingAsync()
        {
            var ls = (await _packagingRepository.GetAllAsync()).Where(p => p.IsDelete == false);
            return ls;
        }
    }
}
