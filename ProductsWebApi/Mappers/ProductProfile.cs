using AutoMapper;
using ProductsWebApi.Dal;
using ProductsWebApi.Models;

namespace ProductsWebApi.Mappers
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<ProductInputDto, Product>()
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (decimal)src.Price / 100));
      CreateMap<Product, ProductOutputDto>()
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => (decimal)src.Price * 100));
      ;

    }
  }
}
