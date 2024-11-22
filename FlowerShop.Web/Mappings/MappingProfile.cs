using AutoMapper;
using FlowerShop.DataAccess.Models;
using FlowerShop.Web.ViewModels;

namespace FlowerShop.Web.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>()
             .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src =>
                 src.ParentCategoryId == 0 ? null : src.ParentCategoryId));

            CreateMap<ProductItem, ProductItemViewModel>();
            CreateMap<ProductItemViewModel, ProductItem>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();

            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppUserViewModel, AppUser>();

			CreateMap<Packaging, PackagingViewModel>();
			CreateMap<PackagingViewModel, Packaging>();
		}
    }
}
