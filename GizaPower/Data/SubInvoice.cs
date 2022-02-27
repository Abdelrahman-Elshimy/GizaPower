using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Data
{
    public class SubInvoice
    {
        [Key]
        public int id { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public DateTime Date_Created { get; set; }

        public string ProductCode { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }



        public double Quantiy { get; set; }
        public double Value { get; set; }


    }
}
