using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Neptun.Models.Enum;

namespace Neptun.Models.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Тип продукции")]
        public ProductType ProductType { get; set; }

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
    }
}