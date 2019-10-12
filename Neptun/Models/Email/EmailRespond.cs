using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Neptun.Models.Email
{
    public class EmailRespond
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string DestinationEmail { get; set; }

        [Required]
        public string Header { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public EmailRespond()
        {
            
        }

        public EmailRespond(string email, string password, string destinationEmail, string header, string subject, string message)
        {
            Email = email;
            Password = password;
            DestinationEmail = destinationEmail;
            Header = header;
            Subject = subject;
            Message = message;
        }

    }
}