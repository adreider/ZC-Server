using System.Collections.Generic;
using System.Threading.Tasks;
using TI_Domain.identity;

namespace TI_Repository.Interface
{
    public interface IRepository<T> where T : class
    {
         //Repositorio Generico     
        //CRUD

        //C - Create
        Task Add(T entity);

        //R - READ
        Task<IEnumerable<T>> GetAllAsync();

        //R - READ By ID
        Task<T> GetByIdAsync(string id);
        Task<T> GetByIdAsyncInt(int id);

        //U -UPDATE
        void Udpate(T entity);

        // D -DELETE
        void Delete(T entity);

        Task<bool> SaveChangesAsync();

        Task<IEnumerable<T>> FindByEmailAsync(string email);

         Task<UserRole> GetRole(string id);

    }
}