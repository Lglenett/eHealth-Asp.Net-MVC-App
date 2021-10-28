using eHealth.Data;
using eHealth.Data.Services;
using eHealth.Data.Static;
using eHealth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eHealth.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync();
            return View(allProducts);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync();

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }

        //Get: Products/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImageURL, Name, Description, ProductCategory, Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.AddAsync(product);
            return RedirectToAction(nameof(Index));
        }


        //Get: ProductsDetails/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);

            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }

        //Get: Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ImageURL, Name, ProductCategory, Description, Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            await _service.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));

        }

        //Get: Products/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));

        }
    }
}
