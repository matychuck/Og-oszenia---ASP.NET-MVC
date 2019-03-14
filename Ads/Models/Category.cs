using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ads.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Wymagane jest wpisanie nazwy kategorii")]
        public string Name { get; set; }

        public int? ParentcategoryID { get; set; }

        public virtual Category CategoryParent{ get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<Ad> Ad { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }

    }
}