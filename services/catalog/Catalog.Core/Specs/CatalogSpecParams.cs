using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Specs
{
    public  class CatalogSpecParams
    {
        private const int MaxPageSize = 80;
        private int _PageSize = 10;


        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;

        }


        public string? BrandId { get; set; }

        public string? TypeId { get; set; }

        public string? Sort { get; set; }

        public string? Search { get; set; }
    }
}
