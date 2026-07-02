using AutoMapper;
using inaApp.DTOs.Categoria;
using inaApp.DTOs.cliente;
using inaApp.DTOs.Producto;
using inaApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Services.Mapping
{
    public class MappingProfile: Profile//Profile para mappeo
    {
        public MappingProfile()
        {
            //Dto create a entity
            CreateMap<ProductoCreateDTO, Producto>();
            CreateMap<ClienteCreateDTO, Cliente>();
            CreateMap<CategoriaCreateDTO, Categoria>();


            //Dto update a entity
            CreateMap<ProductoUpdateDTO, Producto>();
            CreateMap<ClienteUpdateDTO, Cliente>();
            CreateMap<CategoriaUpdateDTO, Categoria>();

            //Entity a Dto response
            CreateMap<Producto,ProductoResponseDTO>();
            CreateMap<Cliente,ClienteResponseDTO>();
            CreateMap<Categoria, CategoriaResponseDTO>();



       }
    }
}
