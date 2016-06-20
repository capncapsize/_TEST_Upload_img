using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _TEST_Upload_img.Models;

namespace _TEST_Upload_img.ViewModels
{
    public class CategoryData
    {
        public IEnumerable<Category>[] subCategories { get; set; }
    }
}
