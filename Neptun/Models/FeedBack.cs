using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neptun.Models
{
    public class FeedBack
    {
        [HiddenInput(DisplayValue = false)] public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Phone(ErrorMessage = "Введите корректный телефонный номер")]
        [Display(Name = "Телефон")]
        public string PhoneNuber { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Введите корректный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [MaxLength(1000, ErrorMessage = "Сообщение слишком длинное")]
        [Display(Name = "Сообщение")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

    }
}