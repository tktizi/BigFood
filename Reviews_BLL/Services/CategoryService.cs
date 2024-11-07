using AutoMapper;
using Catalog_BLL.DTO.Requests;
using Catalog_BLL.DTO.Responses;
using Catalog_BLL.Services.Contracts;
using Catalog_DAL.Entities;
using Catalog_DAL.UOF;

namespace Catalog_BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShortCategoryResponse>> GetAllCategoriesAsync()
        {
            var result = await _unitOfWork.CategoryRepository.GetAllAsync();
            await _unitOfWork.CompleteAsync();
            var shortCategoryResponse = _mapper.Map<IEnumerable<ShortCategoryResponse>>(result);
            return shortCategoryResponse;
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(int categoryId)
        {
            var result = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
            await _unitOfWork.CompleteAsync();
            var categoryResponse = _mapper.Map<CategoryResponse>(result);
            return categoryResponse;
        }

        public async Task<CategoryResponse> AddCategoryAsync(CategoryRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var result = await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            var categoryResponse = _mapper.Map<CategoryResponse>(result);
            return categoryResponse;
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(CategoryRequest categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var result = await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
            var categoryResponse = _mapper.Map<CategoryResponse>(result);
            return categoryResponse;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _unitOfWork.CategoryRepository.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }

}
