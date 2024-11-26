using AutoMapper;
using Ecommerce.FrontEnd.ViewModels;
using EcommerceShop.Common.Dto;

namespace Ecommerce.FrontEnd.AutoMapper
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<LoginDto, LoginViewModel>()
        .ForMember(dest => dest.Email, opt 
          => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Password, opt 
          => opt.MapFrom(src => src.HashedPassword)).ReverseMap();

      CreateMap<ProductLineDto, ProductLineViewModel>()
        .ForMember(dest => dest.Product, opt 
          => opt.MapFrom(src => src.Product))
        .ForMember(dest => dest.Quantity, opt 
          => opt.MapFrom(src => src.Quantity)).ReverseMap();

      CreateMap<UserDto, CustomerViewModel>()
        .ForMember(dest => dest.Name, opt
          => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Email, opt 
          => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Address, opt 
          => opt.MapFrom(src => src.Address)).ReverseMap();

      CreateMap<OrderDto, OrderViewModel>()
        .ForMember(dest => dest.Customer, opt
          => opt.MapFrom(src => src.Customer))
        .ForMember(dest => dest.ProductLines, opt 
          => opt.MapFrom(src => src.ProductLines))
        .ForMember(dest => dest.TotalPrice, opt 
          => opt.MapFrom(src => src.TotalPrice)).ReverseMap();

      CreateMap<ProductDto, ProductViewModel>()
        .ForMember(dest => dest.Name, opt 
          => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Price, opt 
          => opt.MapFrom(src => src.Price))
        .ForMember(dest => dest.Amount, opt
          => opt.MapFrom(src => src.Amount)).ReverseMap();
    }
  }
}
