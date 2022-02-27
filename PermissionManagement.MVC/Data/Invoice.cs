using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Data
{
    public class Invoice
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public double TotalValue { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public GiazUser User { get; set; }

        public bool Status { get; set; }

        public virtual List<SubInvoice> SubInvoices { get; set; }


    }
}
