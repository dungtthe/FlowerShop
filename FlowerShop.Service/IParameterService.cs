using FlowerShop.Common.ViewModels;
using FlowerShop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Service
{
	public interface IParameterService
	{
		Task<ICollection<ParameterConfiguration>> GetData();

		Task<PopupViewModel> Update(ParameterConfiguration request);
	}
}