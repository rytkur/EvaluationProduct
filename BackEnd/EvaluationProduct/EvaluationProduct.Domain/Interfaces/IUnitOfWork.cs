using System;
using System.Threading.Tasks;

namespace EvaluationProduct.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        Task<int> SaveAsync();
    }
}
