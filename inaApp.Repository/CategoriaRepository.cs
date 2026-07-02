using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Data;
using inaApp.Entites;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Repository
{
    public class CategoriaRepository : IGenericRepository<Categoria>
    {
        //Inyección de dependecias
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<Categoria> ActualizarAsync(int id, Categoria entity)
        {
            try
            {
                //Extraemos la entidad
                var categoria = await _context.Categoria.Where(c => c.Id == id && c.Estado == true).FirstOrDefaultAsync();

                //Validamos la entidad
                if (categoria is null)
                {
                    throw new NotFoundException($"No se encontro un cliente activo con la el id {id}");
                }


                //Actualizamos la entidad
                await _context.Categoria
                    .Where(c => c.Id == id && c.Estado == true)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c=>c.Nombre,entity.Nombre)
                    .SetProperty(c=>c.Descripcion,entity.Descripcion)
                    );

                //Cambiamos campos
                entity.Estado = true;
                entity.Id = id;

                //Retornamos la entidad
                return await _context.Categoria.FirstOrDefaultAsync(c => c.Id == id && c.Estado == true);
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is SqlException sqlEx &&
                (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                // Error por nombre de categoría duplicado
                if (sqlEx.Message.Contains("IX_tbCategoria_Nombre"))
                {
                    throw new DuplicateNameException(
                        "Ya existe una categoría con ese nombre."
                    );
                }

                throw new ConflictException("Ya existe una categoría con datos duplicados.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Categoria> CrearAsync(Categoria entity)
        {
            try
            {
                //Cambiamos el estado a true
                entity.Estado = true;
                //Agregamos la entidad a la tabla
                await _context.Categoria.AddAsync(entity);
                //Guardamos los datos
                await _context.SaveChangesAsync();

                //Retornamos la entidad
                return entity;
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is SqlException sqlEx &&
                (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                // Error por nombre de categoría duplicado
                if (sqlEx.Message.Contains("IX_tbCategoria_Nombre"))
                {
                    throw new DuplicateNameException(
                        "Ya existe una categoría con ese nombre."
                    );
                }

                throw new ConflictException("Ya existe una categoría con datos duplicados.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                //Extraemos la entidad
                var result=await _context.Categoria
                    .Where(c=>c.Id==id && c.Estado==true)
                    .ExecuteUpdateAsync(setters=>setters
                    .SetProperty(c=>c.Estado,false)
                    )>0;

                //Validamos si existe
                if (!result)
                {
                    throw new NotFoundException($"No se encontro ninguna categora activa con el id {id}");
                }

                //Retornamos los resultados
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            try
            {
                //Retornamos la entidad
                var categoria= await _context.Categoria
                    .Where(c => c.Id == id && c.Estado == true)
                    .SingleOrDefaultAsync();

                //Validamos la categoria
                if (categoria is null) throw new NotFoundException($"No se encontro ninguna categoria activa con el id {id}");

                //Retornamos la categoria
                return categoria;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Categoria>> obtenerTodosAsync()
        {
            try
            {
                //Retornamos un lista con todas las categorias activas
                return await _context.Categoria
                    .Where(c=>c.Estado==true)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> validarNombreRepetido(string nombre)
        {
            try
            {
                //Retornamos un booleano si existe
                return await _context.Categoria.AnyAsync(c => c.Nombre.ToLower()==nombre.ToLower());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
