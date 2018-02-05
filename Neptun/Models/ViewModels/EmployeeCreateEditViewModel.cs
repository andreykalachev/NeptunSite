using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neptun.Models.ViewModels
{
    public class EmployeeCreateEditViewModel
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

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Фото")]
        public string Photo { get; set; }

        public HttpPostedFileBase HttpPostedFilePhoto { get; set; }

        public static implicit operator Employee(EmployeeCreateEditViewModel employeeViewModel)
        {
            return new Employee
            {
                Email = employeeViewModel.Email,
                FirstName = employeeViewModel.FirstName,
                Id = employeeViewModel.Id,
                LastName = employeeViewModel.LastName,
                Patronymic = employeeViewModel.Patronymic,
                PhoneNuber = employeeViewModel.PhoneNuber,
                Photo = employeeViewModel.Photo,
                Position = employeeViewModel.Position
            };
        }

        public static implicit operator EmployeeCreateEditViewModel(Employee employee)
        {
            return new EmployeeCreateEditViewModel
            {
                Email = employee.Email,
                FirstName = employee.FirstName,
                Id = employee.Id,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                PhoneNuber = employee.PhoneNuber,
                Photo = employee.Photo,
                Position = employee.Position,
                HttpPostedFilePhoto = null
            };
        }
    } 
}