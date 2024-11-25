using FlowerShop.Common.MyConst;
using FlowerShop.Common.Template;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using FlowerShop.DataAccess.Repositories.RepositoriesImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlowerShop.Service.ServiceImpl
{
	public class SuppliersService : ISuppliersService
	{
		private readonly ISupplierRepository _supplierRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SuppliersService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
		{
			_supplierRepository = supplierRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<ResponeMessage> AddNewSupplier(string companyName, string taxCode, string email, string phone, int type, string images, string description, string industry, string address, bool isDelete)
		{
			// Chuyển danh sách ảnh sang chuỗi JSON
			string imagesJson = JsonConvert.SerializeObject(images);

			Supplier supplier = new Supplier()
			{
				CompanyName = companyName,
				TaxCode = taxCode,
				Email = email,
				Phone = phone,
				Type = type = 1,
				Images = imagesJson,
				Description = description,
				Industry = industry = "Công nghiệp hoa",
				Address = address,
				IsDelete = isDelete = false
			};
			var result = await _supplierRepository.AddAsync(supplier);
			if (result == null)
			{
				return new ResponeMessage(ResponeMessage.ERROR, $"Có lỗi xảy ra");
			}
			await _unitOfWork.Commit();
			return new ResponeMessage(ResponeMessage.SUCCESS, $"Thêm nhà cung cấp thành công");
		}

		public async Task<ICollection<Supplier>> ChiTietNhaCungCap(int id)
		{
			var nhaCungCap = await _supplierRepository.GetSingleByIdAsync(id);
			if (nhaCungCap == null)
			{
				return new List<Supplier>(); // Trả về danh sách rỗng
			}
			return new List<Supplier> { nhaCungCap }; // Trả về danh sách chứa một nhà cung cấp
		}

		public async Task<PopupViewModel> Delete(int id)
		{
			var supplier = await _supplierRepository.GetSingleByIdAsync(id);
			if (supplier == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			supplier.IsDelete = true;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đã xóa thành công");
		}

		public async Task<Supplier> GetSingleById(int id)
		{
			if (id == -1)
			{
				return null;
			}
			var supplier = await _supplierRepository.SingleOrDefaultWithIncludeAsync(p => p.Id == id);
			return supplier;
		}

		public async Task<ICollection<Supplier>> GetSuppliersAsync()
		{
			var result = await _supplierRepository.GetAllAsync();
			return result.ToList();
		}

		public async Task<Supplier> UpdateAsync(Supplier supplier)
		{
			var result = _supplierRepository.Update(supplier);
			if (result != null)
			{
				await _unitOfWork.Commit();
			}
			return result;
		}
	}
}