using Application.Common;
using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController:Controller
    {
        private readonly IProductApplication productApplication;

        public ProductController(IProductApplication productApplication)
        {
            this.productApplication = productApplication;
        }
        [HttpPost("create")]
        public async Task<ActionResult> create([FromBody] ProductCreateDto createDto)
        {
            var response = await productApplication.create(createDto);
            switch (response.Code)
            {
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, response);
                case 400:
                    return BadRequest(response);
                case 201:
                    return StatusCode(StatusCodes.Status201Created, response);
                default:
                    return Ok(response);
            }
        }
        [HttpPut("update")]
        public async Task<ActionResult> update([FromBody] ProductDto productDto)
        {
            var response = await productApplication.update(productDto);
            switch (response.Code)
            {
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, response);
                case 400:
                    return BadRequest(response);
                case 201:
                    return StatusCode(StatusCodes.Status201Created, response);
                default:
                    return Ok(response);
            }
        }
        [HttpGet("list-all")]
        public async Task<ActionResult> getAll()
        {
            var response = await productApplication.getAll();
            switch (response.Code)
            {
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, response);
                case 400:
                    return BadRequest(response);
                case 201:
                    return StatusCode(StatusCodes.Status201Created, response);
                default:
                    return Ok(response);
            }
        }
        [HttpGet("detail/{nId}")]
        public async Task<ActionResult> getById([FromRoute] int nId)
        {
            var response = await productApplication.getById(nId);
            switch (response.Code)
            {
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, response);
                case 400:
                    return BadRequest(response);
                case 201:
                    return StatusCode(StatusCodes.Status201Created, response);
                default:
                    return Ok(response);
            }
        }
        [HttpDelete("delete/{nId}")]
        public async Task<ActionResult> delete([FromRoute] int nId)
        {
            var response = await productApplication.delete(nId);
            switch (response.Code)
            {
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, response);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, response);
                case 400:
                    return BadRequest(response);
                case 201:
                    return StatusCode(StatusCodes.Status201Created, response);
                default:
                    return Ok(response);
            }
        }
    }
}
