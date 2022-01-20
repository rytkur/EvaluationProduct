using System.Threading.Tasks;
using EvaluationProduct.Context.Repositories;
using EvaluationProduct.Domain.Interfaces;

namespace EvaluationProduct.Context.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IProductRepository _productRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IProductRepository Products
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
