using inaApp.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.Interfaces
{
    public interface IGenericService<TResponse,TCreate,TUpdate>
    {
        //Firmas de los metodos
        Task<ApiResponse<List<TResponse>>> obtenerTodosAsync();//Task xq son metodos asincronicos

        Task<ApiResponse<TResponse>> ObtenerPorIdAsync(int id);

        Task<ApiResponse<TResponse>> CrearAsync(TCreate entity);

        Task<ApiResponse<TResponse>> ActualizarAsync(int id,TUpdate entity);

        Task<ApiResponse<bool>> EliminarAsync(int id);
    }
}
