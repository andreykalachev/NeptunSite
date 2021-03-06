﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Neptun.Models.Enum;

namespace Neptun.Models.ViewModels.Product
{
    public class ProductAdminViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Тип продукции")]
        public ProductType ProductType { get; set; }

        [Display(Name = "Название кнопки для описания")]
        public string ButtonDescriptionName { get; set; }

        [Display(Name = "Фото")]
        public byte[] Photo { get; set; }
    }
}