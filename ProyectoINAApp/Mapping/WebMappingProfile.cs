using AutoMapper;
using inaApp.DTOs.Categoria;
using inaApp.DTOs.Producto;
using ProyectoINAApp.Models.Categoria;
using ProyectoINAApp.Models.Producto;

namespace ProyectoINAApp.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            //DTO A VIEWMODEL - Producto
            CreateMap<ProductoResponseDTO, ProductoIndexViewModel>();
            CreateMap<ProductoCreateDTO, ProductoCreateViewModel>();

            //DTO A VIEWMODEL - Categoria
            CreateMap<CategoriaResponseDTO, CategoriaIndexViewModel>();
            CreateMap<CategoriaResponseDTO, CategoriaEditViewModel>();

            //VIEWMODEL A DTO - Producto
            CreateMap<ProductoIndexViewModel, ProductoResponseDTO>();
            CreateMap<ProductoCreateViewModel, ProductoCreateDTO>();
            CreateMap<ProductoEditViewModel, ProductoUpdateDTO>();

            //VIEWMODEL A DTO - Categoria
            CreateMap<CategoriaCreateViewModel, CategoriaCreateDTO>();
            CreateMap<CategoriaEditViewModel, CategoriaUpdateDTO>();
        }
    }
}
