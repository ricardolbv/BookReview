using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Contracts.Persistence
{
    public interface IAsyncRepository <T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<int> DeleteAsync(T enttty);
    }
}
