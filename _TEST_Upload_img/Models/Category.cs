using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _TEST_Upload_img.Models
{
    public class Category
    {
        [Key]
        public string Name { get; set; }

        public int Level { get; set; }

        public string ImageSource { get; set; }

        public Category Parent { get; set;  }


        public virtual ICollection<Category> Children { get; set; }
    }
}