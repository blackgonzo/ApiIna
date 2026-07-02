using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Data;
using inaApp.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Repository
{
    public class ProductoRespository : IProductoRepository<Producto>
    {

        private readonly ApplicationDbContext _context;

        public ProductoRespository(ApplicationDbContext context)
        {
            _context = context;   
        }
        public async Task<Producto> ActualizarAsync(int id,Producto entity)
        {
            try
            {
                var producto = await _context.Producto.Where(x => x.Id == id && x.Estado == true).FirstOrDefaultAsync();

                if (producto is null)
                {
                    return null;
                }



                await _context.Producto
                    .Where(x => x.Id == id && x.Estado == true)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Nombre, entity.Nombre)
                    .SetProperty(p => p.Precio, entity.Precio)
                    .SetProperty(p => p.Stock, entity.Stock)
                    .SetProperty(p => p.Descripcion, entity.Descripcion)
                    .SetProperty(p => p.CategoriaId, entity.CategoriaId)
                    );

                //Seteamos entidad
                entity.Id = id;
                entity.Estado = true;

                //Retornamos la entidad
                return await _context.Producto
                  .Include(p => p.Categoria)
                  .FirstOrDefaultAsync(p => p.Id == id && p.Estado == true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Producto> CrearAsync(Producto entity)
        {
            try
            {
                entity.Estado = true;

                await _context.Producto.AddAsync(entity);
                await _context.SaveChangesAsync();

                return await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == entity.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var producto = await ObtenerPorIdAsync(id);
                if (producto==null)
                {
                    return false;
                }
                producto.Estado = false;
                _context.Producto.Update(producto);
                await _context.SaveChangesAsync();
                return true;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _context.Producto.Where(x => x.Id == id && x.Estado == true).Include(p => p.Categoria).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> obtenerTodosAsync()
        {
            try
            {
                //Obtener lista de Productos, donde los estados este activos|
                return await _context.Producto
                    .Where(x=> x.Estado==true)
                    .Include(p=>p.Categoria)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> validarNombreRepetido(string nombre) { 
            //Validar que el nombre no este repetido
            return await _context.Producto.AnyAsync(x => x.Nombre.ToLower() == nombre.ToLower());
        }


        public async Task<bool> ValidarCategoriaProducto(int id)
        {
            //Retornamos si la categoria existe o se encuentra activa
            return await _context.Categoria
                .AnyAsync(c => c.Id == id && c.Estado == true);
        }

    }
}
