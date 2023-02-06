using Application.Common;
using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IProductApplication
    {
        Task<Response<bool>> create(ProductCreateDto productCreate);
        Task<Response<ProductDto>> getById(int id);
        Task<Response<bool>> update(ProductDto productDto);
        Task<Response<IEnumerable<ProductDto>>> getAll();
        Task<Response<bool>> delete(int id);
    }
}
