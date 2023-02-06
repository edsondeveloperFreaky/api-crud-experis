using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Interface
{
    public interface IProductRepository
    {
        Task<bool> createAsync(Product product);
        Task<bool> updateASync(Product product);
        Task<IEnumerable<Product>> listAsync();
        Task<Product> findByIdAsync(int id);
        Task<bool> deleteAsync(int id);
    }
}
