using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{

    public class SaleInvoiceDetailsService : ISaleInvoiceDetailService
    {
        private readonly ISaleInvoiceRepository _saleInvoiceRepository;
        private readonly ISaleInvoiceDetailRepository _saleInvoiceDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FlowerShopContext _context;

        public SaleInvoiceDetailsService(FlowerShopContext context, ISaleInvoiceRepository saleInvoiceRepository, IUnitOfWork unitOfWork, ISaleInvoiceDetailRepository saleInvoiceDetailRepository)
        {
            _saleInvoiceRepository = saleInvoiceRepository;
            _unitOfWork = unitOfWork;
            _saleInvoiceDetailRepository = saleInvoiceDetailRepository;
            _context = context;
        }

        public async Task<SaleInvoiceDetail> GetSaleInvoiceDetailByIdAsync(int saleInvoiceDetailId)
        {
            try
            {
                // Truy vấn SaleInvoiceDetail theo SaleInvoiceDetailId
                var saleInvoiceDetail = await _context.SaleInvoiceDetails
                    .Where(s => s.Id == saleInvoiceDetailId && !s.IsDelete) // Kiểm tra IsDelete nếu cần
                    .FirstOrDefaultAsync();

                return saleInvoiceDetail;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                throw new Exception("An error occurred while retrieving the sale invoice detail.", ex);
            }
        }


        public async Task<List<SaleInvoiceDetail>> GetSaleInvoiceDetailsByUserIdAsync(string userId)
        {
            try
            {
                var saleInvoiceDetails = await _context.SaleInvoiceDetails
                    .Include(s => s.SaleInvoice) // Bao gồm thông tin hóa đơn nếu cần
                    .Where(s => s.SaleInvoice.CustomerId == userId && !s.IsDelete) // Kiểm tra người dùng và trạng thái xóa
                    .ToListAsync();

                return saleInvoiceDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving sale invoice details for the user.", ex);
            }
        }
    }

    
}
