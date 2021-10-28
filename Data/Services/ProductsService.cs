using eHealth.Data.Base;
using eHealth.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eHealth.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>,  IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .FirstOrDefaultAsync(n => n.Id == id);
            return  productDetails;
                
        }
    }
}
