using MyStore.DTO.Category;
using MyStore.Models;
using AutoMapper;
using MyStore.DTO.Membership;
using MyStore.DTO.Product;

namespace MyStore.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Membership, MembershipResponseDto>();
            CreateMap<MembershipCreateDto, Membership>();
            CreateMap<MembershipUpdateDto, Membership>();

            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductCreateDto, Product>();

        }
    }
}
