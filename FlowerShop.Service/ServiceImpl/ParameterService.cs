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
	}
}