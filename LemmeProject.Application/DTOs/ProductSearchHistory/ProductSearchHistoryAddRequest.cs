using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmeProject.Application.DTOs.ProductSearchHistory
{
    public class ProductSearchHistoryAddRequest
    {
        public DateTime SearchedDate { get; set; }

        //Relations
        public int ProductId { get; set; }
    }
}
