using System.Collections.Generic;
using Neptun.Models.Enum;

namespace Neptun.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public ProductType? ProductType { get; set; }
        public List<Production> Products { get; set; }

        public ProductIndexViewModel()
        {
            
        }
        public ProductIndexViewModel(int pageCount, int currentPage, ProductType? productType)
        {
            PageCount = pageCount;
            CurrentPage = currentPage;
            ProductType = productType;
        }
    }
}