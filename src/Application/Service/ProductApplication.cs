using Application.Common;
using Application.Dto;
using Application.Interface;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ProductApplication : IProductApplication
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly CreateProductValidator createValidation;
        private readonly UpdateProductValidator updateValidation;
        private readonly IdValidator idValidation;

        public ProductApplication(IMapper mapper, IProductRepository productRepository, CreateProductValidator createValidation, UpdateProductValidator updateValidation,
            IdValidator idValidation)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.createValidation = createValidation;
            this.updateValidation = updateValidation;
            this.idValidation = idValidation;
        }
        public async Task<Response<bool>> create(ProductCreateDto productCreate)
        {
            var res = new Response<bool>();
            try
            {
                var validation = createValidation.Validate(productCreate);
                if (!validation.IsValid)
                {
                    res.Errors = validation.Errors;
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = "Error en validaciones";
                    return res;
                }
                var product = mapper.Map<Product>(productCreate);
                product.FechaRegistro = DateTime.Now;

                var resultRespository = await productRepository.createAsync(product);
                if (!resultRespository)
                {
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = "No se pudo crear producto";
                    return res;
                }

                res.Data = resultRespository;
                res.Code = 201;
                res.Message = "Creado";
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = e.Message;
                res.Code = 500;
            }
            return res;
        }
        public async Task<Response<bool>> delete(int id)
        {
            var res = new Response<bool>();
            try
            {

                var validation = idValidation.Validate(id);
                if (!validation.IsValid)
                {
                    res.Errors = validation.Errors;
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = "Error en validaciones";
                    return res;
                }

                var existProduct = await productRepository.findByIdAsync(id);
                if (existProduct is null)
                {
                    res.IsSuccess = false;
                    res.Code = 404;
                    res.Message = $"No existe producto con id {id}";
                    return res;
                }

                var deleted = await productRepository.deleteAsync(id);
                if (!deleted)
                {
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = "No se pudo eliminar";
                    return res;
                }

                res.Data = deleted;
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = e.Message;
                res.Code = 500;
            }
            return res;
        }
        public async Task<Response<IEnumerable<ProductDto>>> getAll()
        {
            var res = new Response<IEnumerable<ProductDto>>();
            try
            {
                var result = await productRepository.listAsync();
                if (result is null)
                {
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = $"Sin resultado";
                    return res;
                }

                res.Data = mapper.Map<IEnumerable<ProductDto>>(result);
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = e.Message;
                res.Code = 500;
            }
            return res;
        }
        public async Task<Response<ProductDto>> getById(int id)
        {
            var res = new Response<ProductDto>();
            try
            {
                var validation = idValidation.Validate(id);
                if (!validation.IsValid)
                {
                    res.Errors = validation.Errors;
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = "Error en validaciones";
                    return res;
                }

                var result = await productRepository.findByIdAsync(id);
                if (result is null)
                {
                    res.IsSuccess = false;
                    res.Code = 404;
                    res.Message = $"No existe producto con id {id}";
                    return res;
                }

                res.Data = mapper.Map<ProductDto>(result);
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = e.Message;
                res.Code = 500;
            }
            return res;
        }
        public async Task<Response<bool>> update(ProductDto productDto)
        {
            var res = new Response<bool>();
            try
            {
                var validation = updateValidation.Validate(productDto);
                if (!validation.IsValid)
                {
                    res.Errors = validation.Errors;
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = "Error en validaciones";
                    return res;
                }

                var product = mapper.Map<Product>(productDto);
                product.FechaRegistro = DateTime.Now;

                var result = await productRepository.findByIdAsync(product.Id);
                if (result is null)
                {
                    res.IsSuccess = false;
                    res.Code = 404;
                    res.Message = $"No existe producto con id {product.Id}";
                    return res;
                }
                var updated = await productRepository.updateASync(product);
                if (!updated)
                {
                    res.IsSuccess = false;
                    res.Code = 400;
                    res.Message = $"No se pudo actualizar";
                    return res;
                }

                res.Data = updated;
            }
            catch (Exception e)
            {
                res.IsSuccess = false;
                res.Message = e.Message;
                res.Code = 500;
            }
            return res;
        }
    }
}
