using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Models;
using Products.Api.Models.Dtos;

namespace Products.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public ProductController(ProductDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto GetProduct()
        {
            try
            {
                IEnumerable<Product> products = _dbContext.Products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;

        }

        [HttpGet("{id:int}")]
        public ResponseDto GetProductById(int id)
        {

            try
            {
                Product product = _dbContext.Products.First(x => x.ProductId == id);
                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }

        [HttpGet("GetByName/{code}")]
        public ResponseDto GetProductByName(string code)
        {

            try
            {
                Product product = _dbContext.Products.First(x => x.Name.ToLower() == code.ToLower());
                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }


        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto CreateProduct([FromBody] ProductDto productDto)
        {

            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _dbContext.Add(product);
                _dbContext.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto UpdateProduct([FromBody] ProductDto productDto)
        {

            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _dbContext.Update(product);
                _dbContext.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto DeleteProduct(int id)
        {

            try
            {
                Product product = _dbContext.Products.First(x => x.ProductId == id);
                _dbContext.Remove(product);
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }
    }
}
