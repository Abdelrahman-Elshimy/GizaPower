using GizaPower.Data;
using GizaPower.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Product> _products;
        private IGenericRepository<Invoice> _invoices;
        private IGenericRepository<SubInvoice> _subinvoices;
       

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Invoice> Invoices => _invoices ??= new GenericRepository<Invoice>(_context);
        public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context);
        public IGenericRepository<SubInvoice> SubInvoices => _subinvoices ??= new GenericRepository<SubInvoice>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}
