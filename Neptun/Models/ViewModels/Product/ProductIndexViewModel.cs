using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Neptun.Models.DTO;
using Neptun.Models.Enum;

namespace Neptun.Models.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public ProductType? ProductType { get; set; }
        public List<ProductIndexDto> Products { get; set; }
        
    }
}