using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Core.IRepositoryDesignPattern
{
<<<<<<< HEAD
    public interface ISharedMethod <T> where T : class
    {
        public IEnumerable<T> GetAllAsync(T entity);
        public Task<T> GetByIdAsync(Guid id);
        public Task<T> AddAsync(T entity);
        public Task<T> UpdateAsync(T entity);
        public Task<T> SoftDeleteAsync(T entity);
        public Task<T> HardDeleteAsync(T entity);


=======
    internal interface ISharedMethod
    {
>>>>>>> 9c8cd6270ab129e0a49971a768fdf8ba8a048ef9
    }
}
