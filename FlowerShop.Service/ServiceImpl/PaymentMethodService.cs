﻿using FlowerShop.Common;
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
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepositor, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paymentMethodRepository = paymentMethodRepositor;
        }

        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return false;
            }

            var paymentMethod = await _paymentMethodRepository.GetSingleByIdAsync(id??-1);
            if (paymentMethod == null)
            {
                return false;
            }

            paymentMethod.IsDelete = true;

            var result = _paymentMethodRepository.Update(paymentMethod);
            if (result != null)
            {
                _unitOfWork.Commit();
                return true;
            }
            return false;

        }

        public async Task<ICollection<PaymentMethod>> GetPaymentMethodsAsync()
        {
            var result = await _paymentMethodRepository.GetAllAsync();
            return result.ToList();
        }

        public PaymentMethod Update(PaymentMethod paymentMethod)
        {
            var result = _paymentMethodRepository.Update(paymentMethod);
            if (result != null)
            {
                _unitOfWork.Commit();
            }
            return result;

        }
    }
}
