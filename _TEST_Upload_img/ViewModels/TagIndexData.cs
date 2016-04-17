using System;
using System.Collections.Generic;
using _TEST_Upload_img.Models;

namespace _TEST_Upload_img.ViewModels
{
    public class TagIndexData
    {
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<ImageTagJoin> ImageTagJoins { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}