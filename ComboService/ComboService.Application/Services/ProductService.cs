using AutoMapper;
using ComboService.Application.Interfaces;
using ComboService.Application.ViewModels.Response;
using ComboService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWord)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWord;
        }

        public async Task<IEnumerable<ProductResponseModel>> GetProducts()
        {
            var products = _unitOfWork.Repository<Product>().GetAll().ToList();
            if (products.Count > 0)
            {
                products = products.OrderByDescending(x => x.CreationDate).ToList();
                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponseModel>>(products);
            }
            else throw new Exception("Not have any combo");
        }
    }
}
