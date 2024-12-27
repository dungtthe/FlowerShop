using FlowerShop.Common.MyConst;
using FlowerShop.Common.Template;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
    public class SaleInvoiceService : ISaleInvoiceService
    {
        private readonly ISaleInvoiceRepository _saleInvoiceRepository;
        private readonly ISaleInvoiceDetailRepository _saleInvoiceDetailRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaleInvoiceService(ISaleInvoiceRepository saleInvoiceRepository, IUnitOfWork unitOfWork, ISaleInvoiceDetailRepository saleInvoiceDetailRepository, IPaymentMethodRepository paymentMethodRepository, IProductRepository productRepository, ICartDetailRepository cartDetailRepository, IProductPriceRepository productPriceRepository)
        {
            _saleInvoiceRepository = saleInvoiceRepository;
            _unitOfWork = unitOfWork;
            _saleInvoiceDetailRepository = saleInvoiceDetailRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _productRepository = productRepository;
            _cartDetailRepository = cartDetailRepository;
            _productPriceRepository = productPriceRepository;
        }

		public async Task<SaleInvoice> GetSaleInvoiceByIdAsync(int saleInvoiceId)
		{
			try
			{
                // Truy vấn SaleInvoice theo Id
                var saleInvoice = (await _saleInvoiceRepository.FindAsync(s=>s.Id==saleInvoiceId && !s.IsDelete)).FirstOrDefault();
					

				return saleInvoice;
			}
			catch (Exception ex)
			{
				// Log lỗi nếu cần thiết
				throw new Exception("An error occurred while retrieving the sale invoice.", ex);
			}
		}


		public async Task<SaleInvoice> GetSingleById(int id)
        {
            if (id == -1)
            {
                return null;
            }
            var order = await _saleInvoiceRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
            return order;
        }

        public async Task<ICollection<SaleInvoice>> GetSaleInvoiceWithIcludeAsync()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.DANG_CHO)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<ICollection<SaleInvoice>> LayCacDonHangDaXacNhan()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.DA_XAC_NHAN)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<ICollection<SaleInvoice>> LayCacDonHangDaHuy()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.DA_HUY)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<ICollection<SaleInvoice>> LayCacDonHangDangChuanBi()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.DANG_CHUAN_BI)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<ICollection<SaleInvoice>> LayCacDonHangDangGiao()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.DANG_GIAO_HANG)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<ICollection<SaleInvoice>> LayCacDonHangGiaoThanhCong()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.GIAO_HANG_THANH_CONG)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<ICollection<SaleInvoice>> LayCacDonHangGiaoThatBai()
        {
            var tempresult = (await _saleInvoiceRepository.GetAllWithIncludeAsync(c => c.Customer, p => p.PaymentMethod));
            var result = new List<SaleInvoice>();
            foreach (var item in tempresult)
            {
                if (item.Status == ConstStatusSaleInvoice.GIAO_HANG_THAT_BAI)
                    result.Add(item);
            }
            return result.ToList();
        }

        public async Task<PopupViewModel> ChoXacNhan(int id)
        {
            var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
            if (order == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
            }
            order.Status = ConstStatusSaleInvoice.DA_XAC_NHAN;
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã chuyển sang đã xác nhận");
        }

        public async Task<PopupViewModel> Huy(int id, string reason)
        {
            var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
            if (order == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
            }
            order.Status = ConstStatusSaleInvoice.DA_HUY;
            order.Note = reason;
            await XuLyDonHangSauKhiHuy(id);
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã hủy");
        }

        public async Task<PopupViewModel> DaXacNhan(int id)
        {
            var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
            if (order == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
            }
            order.Status = ConstStatusSaleInvoice.DANG_CHUAN_BI;
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã chuyển sang đang chuẩn bị");
        }

        public async Task<PopupViewModel> DangChuanBi(int id)
        {
            var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
            if (order == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
            }
            order.Status = ConstStatusSaleInvoice.DANG_GIAO_HANG;
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã sẵn sàng và chuyển sang trạng thái giao hàng.");
        }

        public async Task<PopupViewModel> GiaoThanhCong(int id)
        {
            var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
            if (order == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
            }
            order.Status = ConstStatusSaleInvoice.GIAO_HANG_THANH_CONG;
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đơn hàng đã giao thành công!");
        }

        public async Task<PopupViewModel> GiaoThatBai(int id, string reason)
        {
            var order = await _saleInvoiceRepository.GetSingleByIdAsync(id);
            if (order == null)
            {
                return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
            }
            order.Status = ConstStatusSaleInvoice.GIAO_HANG_THAT_BAI;
            order.Note = reason;
            await XuLyDonHangSauKhiHuy(id);
            await _unitOfWork.Commit();
            return new PopupViewModel(PopupViewModel.SUCCESS, "Thất bại", "Đơn hàng giao không thành công!");
        }

        public async Task<ICollection<SaleInvoiceDetail>> ChiTietDonHang(int id)
        {
            var danhSachchiTietDonHang = (await _saleInvoiceDetailRepository.GetAllWithIncludeAsync(c => c.SaleInvoice, p => p.Product));
            var danhSachchiTietMotDonHang = danhSachchiTietDonHang.Where(d => d.SaleInvoiceId == id);
            if (danhSachchiTietMotDonHang == null)
            {
                return null;
            }
            return danhSachchiTietMotDonHang.ToList();
        }

        public async Task XuLyDonHangSauKhiHuy(int id)
        {
            var chiTietDonHangList = await ChiTietDonHang(id);
            foreach (var item in chiTietDonHangList)
            {
                item.Product.Quantity += item.Quantity;
                item.IsDelete = true;
            }
        }

        public async Task<double> TongTienCuaMotDonHang(int id)
        {
            double tongtien = 0;
            var chitietdonhang = await ChiTietDonHang(id);
            foreach (var item in chitietdonhang)
            {
                tongtien += item.Quantity * item.UnitPrice;
            }

            return tongtien;
        }

        public async Task<double> TongDoanhThuThangNay()
        {
            double tongtien = 0;
            var donHangGiaoThanhCong = await LayCacDonHangGiaoThanhCong();
            // Lọc các đơn hàng thuộc tháng hiện tại
            var donHangTrongThangNay = donHangGiaoThanhCong
                .Where(d => d.CreateDay.Month == DateTime.Now.Month && d.CreateDay.Year == DateTime.Now.Year);

            // Tính tổng tiền của các đơn hàng trong tháng này
            foreach (var item in donHangTrongThangNay)
            {
                var tien = await TongTienCuaMotDonHang(item.Id);
                tongtien += tien;
            }
            return tongtien;
        }

        public async Task<double> TongDoanhThuHomNay()
        {
            double tongtien = 0;
            var donHangGiaoThanhCong = await LayCacDonHangGiaoThanhCong();
            // Lọc các đơn hàng giao thành công hôm nay
            var donHangHomNay = donHangGiaoThanhCong
                .Where(d => d.CreateDay.Month == DateTime.Now.Month
                && d.CreateDay.Year == DateTime.Now.Year && d.CreateDay.Day == DateTime.Now.Day);

            // Tính tổng tiền của các đơn hàng trong hôm nay
            foreach (var item in donHangHomNay)
            {
                var tien = await TongTienCuaMotDonHang(item.Id);
                tongtien += tien;
            }
            return tongtien;
        }

        public async Task<int> SoDonHangCho()
        {
            var donHangCho = await GetSaleInvoiceWithIcludeAsync();
            var soLuong = donHangCho.Count();
            return soLuong;
        }

        public async Task<Dictionary<string, object>> GetSalesDataByDateRangeAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var invoices = await _saleInvoiceRepository.GetAllAsync();

                var filteredInvoices = invoices.Where(i => i.Status == ConstStatusSaleInvoice.GIAO_HANG_THANH_CONG);

                if (startDate.HasValue)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.CreateDay >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.CreateDay <= endDate.Value);
                }

                // Nhóm doanh thu theo ngày
                var dailySales = filteredInvoices
                    .GroupBy(i => i.CreateDay.Date)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Danh sách ngày cần hiển thị: luôn bao gồm startDate và endDate
                var labels = new HashSet<string>
                {
                    startDate.Value.ToString("dd/MM/yyyy"),
                    endDate.Value.ToString("dd/MM/yyyy")
                };

                var values = new List<double>();

                // Thêm tối đa 3 ngày có doanh thu khác 0, ưu tiên ngày gần nhất
                foreach (var day in dailySales.Keys.OrderBy(d => d))
                {
                    if (labels.Count >= 7) break; // Bao gồm startDate, endDate và tối đa 5 ngày khác
                    labels.Add(day.ToString("dd/MM/yyyy"));
                }

                // Đảm bảo giá trị được sắp xếp theo thời gian
                var sortedLabels = labels.OrderBy(label => DateTime.ParseExact(label, "dd/MM/yyyy", null)).ToList();

                foreach (var label in sortedLabels)
                {
                    var date = DateTime.ParseExact(label, "dd/MM/yyyy", null);

                    if (dailySales.ContainsKey(date))
                    {
                        double total = 0;
                        foreach (var invoice in dailySales[date])
                        {
                            total += await TongTienCuaMotDonHang(invoice.Id);
                        }
                        values.Add(total);
                    }
                    else
                    {
                        // Gán 0 nếu không có hóa đơn cho ngày đó
                        values.Add(0);
                    }
                }

                return new Dictionary<string, object>
        {
            { "labels", sortedLabels },
            { "values", values }
        };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong Service GetSalesDataByDateRangeAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, object>> GetTopSellingProductsAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Lấy tất cả chi tiết hóa đơn bao gồm thông tin hóa đơn cha
                var invoiceDetails = await _saleInvoiceDetailRepository.GetAllWithIncludeAsync(i => i.SaleInvoice, i => i.Product);

                // Lọc chi tiết hóa đơn dựa trên trạng thái hóa đơn cha là GIAO_HANG_THANH_CONG
                var filteredInvoiceDetails = invoiceDetails.Where(i => i.SaleInvoice?.Status == ConstStatusSaleInvoice.GIAO_HANG_THANH_CONG);

                // Lọc theo ngày nếu có tham số
                if (startDate.HasValue)
                {
                    filteredInvoiceDetails = filteredInvoiceDetails.Where(i => i.SaleInvoice?.CreateDay >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    filteredInvoiceDetails = filteredInvoiceDetails.Where(i => i.SaleInvoice?.CreateDay <= endDate.Value);
                }

                // Nhóm và tính tổng số lượng bán theo sản phẩm
                var productSales = filteredInvoiceDetails
                    .GroupBy(d => d.Product.Title) // Nhóm theo tên sản phẩm
                    .Select(g => new
                    {
                        ProductName = g.Key,
                        TotalQuantity = g.Sum(d => d.Quantity) // Tổng số lượng bán của sản phẩm
                    })
                    .OrderByDescending(p => p.TotalQuantity) // Sắp xếp giảm dần theo số lượng
                    .Take(10) // Lấy top 10 sản phẩm
                    .ToList();

                // Chuẩn bị dữ liệu trả về
                var labels = productSales.Select(p => p.ProductName).ToList();
                var values = productSales.Select(p => p.TotalQuantity).ToList();

                return new Dictionary<string, object>
                {
                    { "labels", labels },
                    { "values", values }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong Service GetTopSellingProductsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, object>> GetAllSale(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var invoices = await _saleInvoiceRepository.GetAllAsync();

                // Lọc các hóa đơn có trạng thái giao hàng thành công
                var filteredInvoices = invoices.Where(i => i.Status == ConstStatusSaleInvoice.GIAO_HANG_THANH_CONG);

                // Áp dụng bộ lọc ngày nếu có
                if (startDate.HasValue)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.CreateDay.Date >= startDate.Value.Date);
                }
                if (endDate.HasValue)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.CreateDay.Date <= endDate.Value.Date);
                }

                // Nhóm doanh thu theo ngày
                var dailySales = filteredInvoices
                    .GroupBy(i => i.CreateDay.Date)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Danh sách tất cả các ngày trong khoảng từ startDate đến endDate
                var allDates = Enumerable.Range(0, (endDate.Value.Date - startDate.Value.Date).Days + 1)
                    .Select(offset => startDate.Value.Date.AddDays(offset))
                    .ToList();

                var labels = new List<string>();
                var values = new List<double>();

                foreach (var date in allDates)
                {
                    labels.Add(date.ToString("dd/MM/yyyy")); // Thêm nhãn ngày

                    if (dailySales.ContainsKey(date))
                    {
                        // Tính tổng doanh thu của các hóa đơn trong ngày
                        double total = 0;
                        foreach (var invoice in dailySales[date])
                        {
                            total += await TongTienCuaMotDonHang(invoice.Id);
                        }
                        values.Add(total);
                    }
                    else
                    {
                        // Gán giá trị doanh thu là 0 nếu không có hóa đơn trong ngày
                        values.Add(0);
                    }
                }

                // Trả về dữ liệu dưới dạng dictionary
                return new Dictionary<string, object>
        {
            { "labels", labels },
            { "values", values }
        };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi trong Service GetSalesDataByDateRangeAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ResponeMessage> ConfirmCheckoutAsync(AppUser user, string fullName, string phoneNumber, string address, string note, int shippingFee, int selectedPaymentMethodId, string sProductIds)
        {
            if (user == null || shippingFee < 0 || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(sProductIds))
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Có lỗi xảy ra");
            }

            List<int> productsId = new List<int>();
            string[] s = sProductIds.Split(' ');
            foreach (var item in s)
            {
                int productId;
                if (int.TryParse(item, out productId))
                {
                    productsId.Add(productId);
                }
            }
            if (productsId.Count == 0)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Có lỗi xảy ra");
            }

            var paymentMethod = (await _paymentMethodRepository.FindAsync(p => p.Id == selectedPaymentMethodId && !p.IsDelete)).FirstOrDefault();
            if (paymentMethod == null)
            {
                return new ResponeMessage(ResponeMessage.ERROR, "Không tìm thấy phương thức thanh toán đã chọn");
            }

            var cartDetails = (await _cartDetailRepository.GetAllAsync()).Where(c => !c.IsDeleted);

            foreach (var item in productsId)
            {
                var findProduct = (await _productRepository.FindAsync(p => p.Id == item && !p.IsDelete)).FirstOrDefault();
                if (findProduct == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, "Không tìm sản phẩm đã mua");
                }
                var findCartDetail = cartDetails.Where(c => c.ProductId == item).FirstOrDefault();
                if (findProduct == null)
                {
                    return new ResponeMessage(ResponeMessage.ERROR, "Có lỗi xảy ra");
                }

            }

          

            SaleInvoice saleInvoice = new SaleInvoice()
            {
                CreateDay = DateTime.Now,
                CustomerId = user.Id,
                PaymentMethodId = selectedPaymentMethodId,
                Status = ConstStatusSaleInvoice.DANG_CHO,
                IsDelete = false,
                Note = note,
                IsPaid = false,
                ShippingCost = shippingFee,
                NameRecipient = fullName,
                PhoneNumberRecipient = phoneNumber,
                DeliveryAddress = address
            };
            await _saleInvoiceRepository.AddAsync(saleInvoice);
            foreach (var item in productsId)
            {

                var productPrices = await _productPriceRepository.FindAsync(P => P.ProductId == item);
                var priceSell = productPrices.OrderBy(p => p.Priority).ToList();
                var cartDetail = cartDetails.ToList().Where(p => p.ProductId == item).FirstOrDefault();
                cartDetail.IsDeleted = true;
                var saleInvoiceDetail = new SaleInvoiceDetail()
                {
                    SaleInvoice = saleInvoice,
                    ProductId = item,
                    Quantity = cartDetail.Quantity,
                    UnitPrice = (int)priceSell[0].Price
                };

                await _saleInvoiceDetailRepository.AddAsync(saleInvoiceDetail);
            }

            await _unitOfWork.Commit();
            if (paymentMethod.Id != 2)
            {
                return new ResponeMessage(ResponeMessage.SUCCESS, "/");
            }
            else//thanh toán online
            {

            }

            return null;

        }

		public async Task<SaleInvoice> GetSaleInvoiceByProductIdAsync(int productId)
		{
			try
			{
				return await _saleInvoiceDetailRepository.GetSaleInvoiceByProductIdAsync(productId);
			}
			catch (Exception ex)
			{
				// Xử lý log lỗi hoặc tùy chỉnh ngoại lệ
				throw new Exception("An error occurred while retrieving the sale invoice by product ID.", ex);
			}
		}
	}
}