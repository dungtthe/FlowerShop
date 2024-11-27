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

		public async Task<ResponeMessage> AddNewSupplier(Supplier _supplier)
		{
			// Kiểm tra xem ảnh có phải là chuỗi hay không. Nếu là chuỗi thì chuyển thành mảng.
			List<string> imageNames = new List<string>();

			// Nếu _supplier.Images là một chuỗi, chuyển thành mảng một phần tử
			if (!string.IsNullOrEmpty(_supplier.Images))
			{
				imageNames.Add(_supplier.Images); // Thêm tên ảnh vào danh sách
			}

			// Chuyển danh sách ảnh thành chuỗi JSON
			string imagesJson = JsonConvert.SerializeObject(imageNames); // Chuyển thành chuỗi JSON

			Supplier supplier = new Supplier()
			{
				CompanyName = _supplier.CompanyName,
				TaxCode = _supplier.TaxCode,
				Email = _supplier.Email,
				Phone = _supplier.Phone,
				Type = _supplier.Type = 1,
				Images = imagesJson,
				Description = _supplier.Description,
				Industry = _supplier.Industry = "Công nghiệp hoa",
				Address = _supplier.Address,
				IsDelete = _supplier.IsDelete = false
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