using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TI_Domain.identity;
using TI_Repository.Interface;

namespace TI_Repository.Repositories {
    public class Repository<T> : IRepository<T> where T : class {

        public readonly DataContext context;
        public Repository (DataContext context) {
            this.context = context;
        }
        public async Task Add (T entity) {
            await this.context.Set<T> ().AddAsync (entity);
        }

        public void Delete (T entity) {
            this.context.Set<T> ().Remove (entity);
        }

        public Task<IEnumerable<T>> FindByEmailAsync (string email) {
            throw new System.NotImplementedException ();
        }

        public async Task<IEnumerable<T>> GetAllAsync () {
            return await this.context.Set<T> ().ToListAsync ();
        }

        public async Task<T> GetByIdAsync (string id) {
            return await this.context.Set<T> ().FindAsync (id);
        }

        public async Task<T> GetByIdAsyncInt (int id) {
            return await this.context.Set<T> ().FindAsync (id);
        }

        public async Task<UserRole> GetRole (string id) {
            return await this.context.Set<UserRole> ().FindAsync (id);
        }

        public async Task<bool> SaveChangesAsync () {
            return (await this.context.SaveChangesAsync ()) > 0;
        }

        public void Udpate (T entity) {
            this.context.Set<T> ().Update (entity);
        }
    }
}