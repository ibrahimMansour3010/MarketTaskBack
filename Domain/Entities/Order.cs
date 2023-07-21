using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order:BaseEntity
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
        public long StockId { get; set; }
        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
        public string? PersonName { get; set; }
    }
}
