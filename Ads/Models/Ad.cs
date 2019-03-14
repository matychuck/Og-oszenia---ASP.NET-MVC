using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ads.Models
{
    public class Ad
    {
        public int AdID { get; set; }

       
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        
        public DateTime DateOfInsert { get; set; }

        public int CategoryID { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public virtual Category Category { get; set;}
      

        public string MediaPath { get; set; }

        public bool IsModerated { get; set; }

        public int VisitCounter { get; set; }

      
    }
}