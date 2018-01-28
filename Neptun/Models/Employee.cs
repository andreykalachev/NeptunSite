using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neptun.Models
{
    public class Employee
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Phone(ErrorMessage = "Введите корректный телефонный номер")]
        [Required(ErrorMessage = "Обязательно для заполнения")]
        [Display(Name = "Телефон")]
        public string PhoneNuber { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Введите корректный Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}