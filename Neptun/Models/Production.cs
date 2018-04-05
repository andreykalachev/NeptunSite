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
        public string FullDescriptionPdf { get; set; }

        [Display(Name = "Название кнопки для описания")]
        public string ButtonDescriptionName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Тип продукции")]
        public ProductType ProductType { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }

        [Display(Name = "Title")]
        [MaxLength(60, ErrorMessage = "Допустимая длина 60 превышена")]
        public string PageTitle { get; set; }

        [Display(Name = "Description")]
        [MaxLength(200, ErrorMessage = "Допустимая длина 200 превышена")]
        public string PageDescription { get; set; }

        [Display(Name = "Keywords")]
        [MaxLength(200, ErrorMessage = "Допустимая длина 200 превышена")]
        public string PageKeywords { get; set; }
    }
}