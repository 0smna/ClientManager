using AutoMapper;
using ClientManagerDAL.Entities;
using ClientManagerBLL.Dtos;

namespace ClientManagerBLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            // 🔵 CLIENTES
            CreateMap<Cliente, ClienteDto>().ReverseMap();

            // 🔵 VEHICULOS
            CreateMap<Vehiculo, VehiculoDto>().ReverseMap();

            // 🔵 CITAS
            CreateMap<CitaLavado, CitaLavadoDto>()
                .ForMember(dest => dest.Estado,
                           opt => opt.MapFrom(src => (int)src.Estado))
                .ReverseMap()
                .ForMember(dest => dest.Estado,
                           opt => opt.MapFrom(src => (EstadoCita)src.Estado));
        }
    }
}