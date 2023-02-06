using Dapper;
using Domain.Entities;
using Infraestructure.Enums;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> createAsync(Product product)
        {
            var result = false;
            try
            {
                using (var con = context.CreateConnection())
                {
                    if (con.State == System.Data.ConnectionState.Closed) con.Open();
                    result = await con.ExecuteScalarAsync<bool>("sp_crud", this.setParameters(product, (int)Operation.create), commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public async Task<bool> deleteAsync(int id)
        {
            try
            {
                using (var con = context.CreateConnection())
                {
                    if (con.State == System.Data.ConnectionState.Closed) con.Open();
                    var p = new Product();
                    p.Id = id;
                    p.FechaRegistro = DateTime.Now;
                    var deletedProduct = await con.ExecuteScalarAsync<bool>("sp_crud", this.setParameters(p, (int)Operation.delete), commandType: System.Data.CommandType.StoredProcedure);
                    if (!deletedProduct)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<Product> findByIdAsync(int id)
        {
            var result = new Product();
            try
            {
                using (var con = context.CreateConnection())
                {
                    if (con.State == System.Data.ConnectionState.Closed) con.Open();
                    var param = new Product();
                    param.Id = id;
                    param.FechaRegistro = DateTime.Now;
                    result = await con.QueryFirstOrDefaultAsync<Product>("sp_crud", this.setParameters(param, (int)Operation.getById), commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public async Task<IEnumerable<Product>> listAsync()
        {
            IEnumerable<Product> result = null;
            try
            {
                using (var con = context.CreateConnection())
                {
                    if (con.State == System.Data.ConnectionState.Closed) con.Open();
                    var p = new Product();
                    p.FechaRegistro = DateTime.Now;
                    result = await con.QueryAsync<Product>("sp_crud", this.setParameters(p, (int)Operation.list), commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        public async Task<bool> updateASync(Product product)
        {
            var result = false;
            try
            {
                using (var con = context.CreateConnection())
                {
                    if (con.State == System.Data.ConnectionState.Closed) con.Open();
                    result = await con.ExecuteScalarAsync<bool>("sp_crud", this.setParameters(product, (int)Operation.update), commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        private DynamicParameters setParameters(Product product, int operation)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", product.Id);
            parameters.Add("@Nombre", product.Nombre);
            parameters.Add("@Precio", product.Precio);
            parameters.Add("@Stock", product.Stock);
            parameters.Add("@FechaRegistro", product.FechaRegistro);
            parameters.Add("@Operation", operation);
            return parameters;
        }
    }
}
