using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neptun.Models.Enum;

namespace Neptun.Models
{
    public class Production
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

        [Display(Name = "Полное описание в PDF")]
        public byte[] FullDescriptionPdf { get; set; }

        [Display(Name = "Название кнопки для описания")]
        public string ButtonDescriptionName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Тип продукции")]
        public ProductType ProductType { get; set; }

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
    }
}