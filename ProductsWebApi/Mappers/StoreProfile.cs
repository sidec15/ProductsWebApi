using AutoMapper;
using ProductsWebApi.Dal;
using ProductsWebApi.Models;

namespace ProductsWebApi.Mappers
{
  public class StoreProfile : Profile
  {
    public StoreProfile()
    {
      CreateMap<StoreInputDto, Store>();
      CreateMap<Store, StoreOutputDto>();

    }
  }
}
