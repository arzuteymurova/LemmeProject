using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmeProject.Domain.Entities
{
    public class ProductSearchHistory : BaseEntity
    {
        public DateTime SearchedDate { get; set; }

        //Relations
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
