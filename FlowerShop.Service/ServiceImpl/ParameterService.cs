using FlowerShop.Common.MyConst;
using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using FlowerShop.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service.ServiceImpl
{
	public class ParameterService : IParameterService
	{
		private readonly IParameterConfigurationRepository _parameterConfigurationRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ParameterService(IParameterConfigurationRepository parameterConfigurationRepository, IUnitOfWork unitOfWork)
		{
			_parameterConfigurationRepository = parameterConfigurationRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<ICollection<ParameterConfiguration>> GetData()
		{
			var data = await _parameterConfigurationRepository.GetAllAsync();
			if (data == null)
			{
				return new List<ParameterConfiguration>();
			}
			return data.ToList();
		}

		public async Task<PopupViewModel> Update(ParameterConfiguration req)
		{
			if (req == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", ConstValues.CoLoiXayRa);
			}
			var parameter = await _parameterConfigurationRepository.GetSingleByIdAsync(req.Id + 1);
			if (parameter == null)
			{
				return new PopupViewModel(PopupViewModel.ERROR, "Lỗi", "Không tìm thấy bảng tham số");
			}
			parameter.AllowedFeedbackDay = req.AllowedFeedbackDay;
			parameter.ShippingCostPerKilometer = req.ShippingCostPerKilometer;
			await _unitOfWork.Commit();
			return new PopupViewModel(PopupViewModel.SUCCESS, "Thành công", "Đã cập nhật thành công");
		}
	}
}