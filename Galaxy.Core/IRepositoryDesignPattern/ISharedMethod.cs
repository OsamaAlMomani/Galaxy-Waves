using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.IRepositoryDesignPattern
{
    public interface ISharedMethod <T> where T : class
    {
        public IEnumerable<T> GetAllAsync(T entity);
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<T> SoftDeleteAsync(T entity);
        public Task<T> HardDeleteAsync(T entity);


    }
}
