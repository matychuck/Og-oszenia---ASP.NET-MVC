using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ads.Models
{
    public class AdminMessage
    {
        public int AdminMessageID { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie tytułu")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie tresci")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

   
    }
}