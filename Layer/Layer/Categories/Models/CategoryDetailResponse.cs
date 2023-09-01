using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categories
{
    internal class CategoryDetailResponse
    {
        public Category Category { get; set; }
        public List<View> Views { get; set; }
    }
}
