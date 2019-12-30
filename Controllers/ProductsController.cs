using System;
using EnginDemirog.WebApiDemo.DataAccess;
using EnginDemirog.WebApiDemo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EnginDemirog.WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        //normal şartlarda bussiness katmanından bağlanmamız gerekiyor
        private IProductDal _productDal;

        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var products = _productDal.GetList();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId == productId);
                if (product == null)
                {
                    return NotFound($"There is no product such id's {productId}");
                }

                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromForm]/*[FromBody] json için*/Product product)
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromForm]Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {
                _productDal.Delete(new Product{ProductId = productId});
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetProductsWithDetail")]
        public IActionResult GetProductsWithDetail()
        {
            try
            {
                var products = _productDal.GetProductsWithDetails();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}