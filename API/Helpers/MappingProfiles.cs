using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
  public class MappingProfiles : Profile
  {
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
        .ForMember(d=>d.ProductBrand, s=>s.MapFrom(a=>a.ProductBrand.Name))
        .ForMember(d=>d.ProductType, s => s.MapFrom(a =>a.ProductType.Name))
        .ForMember(d=>d.PictureUrl, s => s.MapFrom<ProductUrlResolver>());
    }
  }
}