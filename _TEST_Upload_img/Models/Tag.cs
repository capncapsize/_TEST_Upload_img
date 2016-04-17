using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _TEST_Upload_img.Models
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }

        public virtual ICollection<ImageTagJoin> Images { get; set; }
    }
}