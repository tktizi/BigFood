using AutoMapper;
using Catalog_BLL.DTO.Requests;
using Catalog_BLL.DTO.Responses;
using Catalog_BLL.Services.Contracts;
using Catalog_DAL.Entities;
using Catalog_DAL.UOF;

namespace Catalog_BLL.Services
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var result = await _unitOfWork.ProductRepository.GetAllAsync();
            _unitOfWork.CompleteAsync();
            var productResponse = _mapper.Map<IEnumerable<ProductResponse>>(result);
            return productResponse;
        }

        public async Task<ProductResponse> GetProductByIdAsync(int productId)
        {
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            _unitOfWork.CompleteAsync();
            var productResponse = _mapper.Map<ProductResponse>(result);
            return productResponse;
        }

        public async Task<ProductResponse> AddProductAsync(ProductRequest productRequest)
        {
            var product = _mapper.Map<Product>( productRequest );
            var result = await _unitOfWork.ProductRepository.AddAsync(product);
            _unitOfWork.CompleteAsync();
            var productResponse = _mapper .Map<ProductResponse>(result);
            return productResponse;
        }

        public async Task<ProductResponse> UpdateProductAsync(ProductRequest productRequest)
        {
            var product = _mapper.Map<Product>(productRequest);
            var result = await _unitOfWork.ProductRepository.UpdateAsync(product);
            _unitOfWork.CompleteAsync();
            var productResponse = _mapper.Map<ProductResponse>(result);
            return productResponse;
        }

        public async Task DeleteProductAsync(int id)
        {
            await _unitOfWork.ProductRepository.DeleteByIdAsync(id);
            _unitOfWork.CompleteAsync();
        }
    }
}
