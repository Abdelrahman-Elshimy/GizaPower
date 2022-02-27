using GizaPower.Data;
using GizaPower.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<GiazUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;


        public ProductController(ApplicationDbContext context, UserManager<GiazUser> userManager, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAll(include: p => p.Include(x => x.User));
            return View(products);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.Date = DateTime.Now;
            product.Value = product.Units * product.Quantiy;
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            product.UserId = currentUser.Id;
            await _unitOfWork.Products.Insert(product);
            await _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.Products.Get(expression: q => q.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ExistProduct = _context.Products.Find(product.Id);
                   if (ExistProduct != null)
                    {
                        ExistProduct.Value = product.Units * product.Quantiy;
                        ExistProduct.Product_Code = product.Product_Code;
                        ExistProduct.Name = product.Name;
                        ExistProduct.Active = product.Active;
                        ExistProduct.Units = product.Units;
                        ExistProduct.Quantiy = product.Quantiy;
                        await _unitOfWork.Save();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Product Not Found");
                        return View(product);
                    }

                }catch
                {
                    return RedirectToAction(nameof(Index));
                }
 
                
            }
            return View(product);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _unitOfWork.Products.Get(expression: q => q.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _unitOfWork.Products.Get(expression: q => q.Id == id);
            await _unitOfWork.Products.Delete(id);
            await _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }


    }
}