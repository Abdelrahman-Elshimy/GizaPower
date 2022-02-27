using GizaPower.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Invoice> Invoices { get; }
        IGenericRepository<SubInvoice> SubInvoices { get; }
        Task Save();
        
    }
}
