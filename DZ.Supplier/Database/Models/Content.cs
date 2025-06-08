using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.SupplierProcessor.Database.Models
{
    public class Content : Base
    {
        public string PoNumber { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }
    }
}
