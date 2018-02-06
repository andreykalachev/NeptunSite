using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neptun.Models.Enum;

namespace Neptun.Models.ViewModels
{
    public class ProductionCreateEditViewModel
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

        public HttpPostedFileBase HttpPostedFilePdf { get; set; }

        [Display(Name = "Название кнопки для описания")]
        public string ButtonDescriptionName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Тип продукции")]
        public ProductType ProductType { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }

        public HttpPostedFileBase HttpPostedFilePhoto { get; set; }

        public static implicit operator Production(ProductionCreateEditViewModel productionViewModel)
        {
            return new Production
            {
                Id = productionViewModel.Id,
                Title = productionViewModel.Title,
                Description = productionViewModel.Description,
                FullDescriptionPdf = productionViewModel.FullDescriptionPdf,
                Photo = productionViewModel.Photo,
                ButtonDescriptionName = productionViewModel.ButtonDescriptionName,
                ProductType = productionViewModel.ProductType
            };
        }

        public static implicit operator ProductionCreateEditViewModel(Production production)
        {
            return new ProductionCreateEditViewModel
            {
                Id = production.Id,
                Title = production.Title,
                Description = production.Description,
                FullDescriptionPdf = production.FullDescriptionPdf,
                Photo = production.Photo,
                ButtonDescriptionName = production.ButtonDescriptionName,
                ProductType = production.ProductType,
                HttpPostedFilePhoto = null,
                HttpPostedFilePdf = null
            };
        }
    }
}