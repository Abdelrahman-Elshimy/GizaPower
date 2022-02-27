using GizaPower.Data;
using GizaPower.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<GiazUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;


        public InvoicesController(ApplicationDbContext context, UserManager<GiazUser> userManager, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var invoices = await _unitOfWork.Invoices.GetAll(include: q => q.Include(x => x.User));
            return View(invoices);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(SubInvoice subInvoice)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            var product = _unitOfWork.Products.Get(expression: p => p.Id == subInvoice.ProductId);
            var invoice = new Invoice
            {
                Date = DateTime.Now,
                Status = true,
                TotalValue = 0,
                UserId = currentUser.Id
            };
             _context.Invoices.Add(invoice);
            _context.SaveChanges();
            var subInvoicenew = new SubInvoice
            {
                Date_Created = DateTime.Now,
                InvoiceId = invoice.id,
                ProductCode = subInvoice.ProductCode,
                ProductId = subInvoice.ProductId,
                Quantiy = subInvoice.Quantiy,
                Value = subInvoice.Quantiy * product.Result.Units
            };

            _context.SubInvoices.Add(subInvoicenew);
            _context.SaveChanges();
            var subInvoicesForTheInvoice = await _unitOfWork.SubInvoices.GetAll(expression: s => s.InvoiceId == invoice.id);
            double total = 0;
            foreach(var s in subInvoicesForTheInvoice)
            {
                total += s.Value;
            }
            var newtotal = total + total * 0.14;
            invoice.TotalValue = newtotal;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Print(int? id)
        {
            var invoice = await _unitOfWork.Invoices.Get(expression: x => x.id == id, include: q => q.Include(x => x.SubInvoices).ThenInclude(x => x.Product).Include(x => x.User));

            return View(invoice);
        }
    }
}
