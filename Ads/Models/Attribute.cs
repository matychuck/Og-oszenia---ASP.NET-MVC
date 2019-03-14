using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ads.Models
{
    public class Attribute
    {
        public int AttributeID { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie typu atrybutu")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie nazwy atrybutu")]
        public string Name { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Cat{ get; set; }

       
    }
}