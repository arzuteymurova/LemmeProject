using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmeProject.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public string HowToUse { get; set; }
        public string Ingredients { get; set; }

        //Relations
       // public List<Image> Images { get; set; }
    }
}
