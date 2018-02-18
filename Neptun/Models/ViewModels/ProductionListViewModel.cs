using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Neptun.Models.Enum;

namespace Neptun.Models.ViewModels
{
    public class ProductionListViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Название")]
        [MaxLength(300, ErrorMessage = "Допустимая длина превышена")]
        public string Title { get; set; }

        [Display(Name = "Полное описание в PDF")]
        public string FullDescriptionPdf { get; set; }

        [Display(Name = "Название кнопки для описания")]
        public string ButtonDescriptionName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Тип продукции")]
        public ProductType ProductType { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }
    }
}