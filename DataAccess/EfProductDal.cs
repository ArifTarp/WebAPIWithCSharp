using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnginDemirog.WebApiDemo.Entities;
using EnginDemirog.WebApiDemo.Models;

namespace EnginDemirog.WebApiDemo.DataAccess
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindDbContext>, IProductDal
    {
        public List<ProductModel> GetProductsWithDetails()
        {
            using (NorthwindDbContext context = new NorthwindDbContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductModel{ProductId = p.ProductId,CategoryName = c.CategoryName,ProductName = p.ProductName,UnitPrice = p.UnitPrice};
                return result.ToList();
            }
        }
    }
}
