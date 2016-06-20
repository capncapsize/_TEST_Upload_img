using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _TEST_Upload_img.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        //[ForeignKey("Category")]
        public Category Category { get; set; }

        public virtual ICollection<ImageTagJoin> Tags { get; set; }
    }
}