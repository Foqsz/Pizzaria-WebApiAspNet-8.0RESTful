using AutoMapper;
using Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Core.Models;

namespace Pizzaria_WebApiAspNet_8._0RESTful.Pizza.Application.DTOs.Mappings;

public class PizzaMappingProfile : Profile
{
    public PizzaMappingProfile()
    {
        CreateMap<PizzariaModel, PizzariaDTO>().ReverseMap();
        CreateMap<PizzaCategoriaModel, PizzaCategoriaDTO>().ReverseMap();
        CreateMap<PizzariaDTO, PizzaDTOUpdateRequest>().ReverseMap();
        CreateMap<PizzariaDTO, PizzaDTOUpdateResponse>().ReverseMap();
    }
}
