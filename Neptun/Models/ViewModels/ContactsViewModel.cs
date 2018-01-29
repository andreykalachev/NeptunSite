using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neptun.Models.ViewModels
{
    public class ContactsViewModel
    {
        public Company Company { get; set; }
        public List<Employee> Employees { get; set; }
    }
}