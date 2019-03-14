using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ads.Models
{
    public class User
    {
        //[Key]
        public int UserID { get; set; }

        [Required(ErrorMessage ="Wymagane jest wpisanie emaila")]
        [DataType(DataType.EmailAddress)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie imienia")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie nazwiska")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie telefonu")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie hasla")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Hasło musi mieć minimum 4znaki")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Hasla musza byc takie same")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public int MyPagination { get; set; }

        public virtual ICollection<Ad> Ad { get; set; }

        public string ActivationCode { get; set; }
    }
}