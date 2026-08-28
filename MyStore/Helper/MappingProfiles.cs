using MyStore.DTO.Category;
using MyStore.Models;
using AutoMapper;
using MyStore.DTO.Membership;
using MyStore.DTO.Product;
using MyStore.DTO.Client;
using MyStore.DTO.Sale;

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
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Client, ClientResponseDto>();
            CreateMap<ClientCreateDto, Client>();
            CreateMap<ClientUpdateDto, Client>();

            CreateMap<SaleCreateDto, Sale>();
            CreateMap<Sale, SaleResponseDto>();
        }
    }
}
