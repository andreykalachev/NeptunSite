using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Neptun.Models.ViewModels.Product
{
    public class ProductInfoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Название")]
        [MaxLength(300, ErrorMessage = "Допустимая длина превышена")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [MaxLength(50000, ErrorMessage = "Допустимая длина превышена")]
        [Display(Name = "Описание")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Название кнопки для описания")]
        public string ButtonDescriptionName { get; set; }

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
    }
}