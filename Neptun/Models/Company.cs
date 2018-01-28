using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neptun.Models
{
    public class Company
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Phone(ErrorMessage = "Введите корректный телефонный номер")]
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Введите корректный факс")]
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Факс")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Введите корректный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}