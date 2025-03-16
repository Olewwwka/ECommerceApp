using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductEntity>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)); 

            CreateMap<ProductEntity, Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId));

            CreateMap<ShoppingCartProductEntity, Product>()
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
               .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
