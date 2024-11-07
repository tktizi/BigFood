using AutoMapper;
using Catalog_BLL.DTO.Requests;
using Catalog_BLL.DTO.Responses;
using Catalog_DAL.Entities;

namespace Catalog_BLL.Configurations
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CategoryRequest, Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>()
                .ForMember(
                    response => response.Products,
                    options => options.MapFrom(category => category.Products));
            CreateMap<ProductRequest, Product>().ReverseMap();
            CreateMap<Product, ProductResponse>()
                .ForMember(
                    response => response.Category,
                    options => options.MapFrom(product => product.Category));
            CreateMap<Category, ShortCategoryResponse>().ReverseMap();
            CreateMap<Product, ShortProductResponse>().ReverseMap();

        }

      
    }
}


