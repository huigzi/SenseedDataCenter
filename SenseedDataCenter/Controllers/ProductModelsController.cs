using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SenseedDataCenter.Models;
using SenseedDataCenter.ViewModel;

namespace SenseedDataCenter.Controllers
{
    public class ProductModelsController : Controller
    {
        private readonly SenseedDataCenterContext _context;

        public ProductModelsController(SenseedDataCenterContext context)
        {
            _context = context;
        }

        // GET: ProductModels
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(category => category.Category).ToListAsync());
        }

        // GET: ProductModels/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories.Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });

            return View();
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(new Product
                {
                    SerialId = productModel.SerialId,
                    SoftWareRev = productModel.SoftWareRev,
                    HardWareRev = productModel.HardWareRev,
                    Locate = productModel.Locate,
                    CategoryId = productModel.Category,
                    UserName = productModel.UserName
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productModel);
        }

        // GET: ProductModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = await _context.Products.FindAsync(id);
            if (temp == null)
            {
                return NotFound();
            }

            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories.Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });

            var productModel = new ProductModel
            {
                SerialId = temp.SerialId,
                SoftWareRev = temp.SoftWareRev,
                HardWareRev = temp.HardWareRev,
                Locate = temp.Locate,
                Category = temp.CategoryId,
                UserName = temp.UserName
            };
            return View(productModel);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var temp = new Product
                    {
                        SerialId = productModel.SerialId,
                        SoftWareRev = productModel.SoftWareRev,
                        HardWareRev = productModel.HardWareRev,
                        Locate = productModel.Locate,
                        Id = productModel.Id,
                        CategoryId = productModel.Category,
                        UserName = productModel.UserName
                    };

                    _context.Products.Update(temp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
        }

        // GET: ProductModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var temp = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);

            if (temp == null)
            {
                return NotFound();
            }

            var productModel = new ProductModel
            {
                SerialId = temp.SerialId,
                SoftWareRev = temp.SoftWareRev,
                HardWareRev = temp.HardWareRev,
                Locate = temp.Locate,
                Category = temp.CategoryId,
                UserName = temp.UserName
            };

            return View(productModel);
        }

        // POST: ProductModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
