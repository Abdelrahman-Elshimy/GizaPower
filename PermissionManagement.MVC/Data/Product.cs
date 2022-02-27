using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GizaPower.Data
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Price per unit field is required")]
        public int Units { get; set; }
        public double Value { get; set; }
        public bool Active { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public GiazUser User { get; set; }
        [Range(0, int.MaxValue)]
        [Required(ErrorMessage = "Quantity field is required")]
        public double Quantiy { get; set; }
        [Required(ErrorMessage = "product Code field is required")]
        public string Product_Code { get; set; }
    }
}
