using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neptun.Models.ViewModels
{
    public class NewsCreateEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [MaxLength(50000, ErrorMessage = "Допустимая длина превышена")]
        [Display(Name = "Описание")]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }

        public HttpPostedFileBase HttpPostedFilePhoto { get; set; }


        public static implicit operator News(NewsCreateEditViewModel newsViewModel)
        {
            return new News
            {
                Id = newsViewModel.Id,
                Title = newsViewModel.Title,
                Description = newsViewModel.Description,
                Date = newsViewModel.Date,
                Photo = newsViewModel.Photo,

            };
        }

        public static implicit operator NewsCreateEditViewModel(News news)
        {
            return new NewsCreateEditViewModel
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Date = news.Date,
                Photo = news.Photo,
                HttpPostedFilePhoto = null,
            };
        }
    }
}