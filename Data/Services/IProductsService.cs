using eHealth.Data.Base;
using eHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eHealth.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);

    }
}
