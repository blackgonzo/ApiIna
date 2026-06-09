using inaApp.Entities;
using inaApp.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static inaApp.Repository.ProductoRepository;
using inaApp.Common.Interfaces;
using inaApp.Data;
using Microsoft.EntityFrameworkCore;

namespace inaApp.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<Producto> ActualizarAsync(Producto entity)
        {
            try
            {
                _context.Producto.Update(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Producto> CrearAsync(Producto entity)
        {
            try
            {
                _context.Producto.Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var producto = await ObtenerPorIdAsync(id);
               if (producto == null)
                {
                    return false;
                }
                producto.Estado = false;
                _context.Producto.Update(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _context.Producto.Where(x => x.Id == id && x.Estado == true).SingleOrDefaultAsync();
                ;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            try
            {
                return await _context.Producto.Where(x => x.Estado == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Producto?> ObtenerPorNombreAsync(string nombre)
        {
            try
            {
                return await _context.Producto
                    .Where(x => x.Nombre == nombre && x.Estado == true)
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}