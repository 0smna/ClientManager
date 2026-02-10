using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientManagerBLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<ClientManagerDAL.Entities.Cliente, ClientManagerBLL.Dtos.ClienteDto>().ReverseMap();
        }
    }
}