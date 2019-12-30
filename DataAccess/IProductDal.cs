using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.Entities;
using EnginDemirog.WebApiDemo.Models;

namespace EnginDemirog.WebApiDemo.DataAccess
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductModel> GetProductsWithDetails();
    }
}
