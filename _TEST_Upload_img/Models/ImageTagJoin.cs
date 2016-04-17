using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _TEST_Upload_img.Models
{
    public class ImageTagJoin
    {
        public int ID { get; set; }

        [Index("IX_ImageAndTag", 1, IsUnique = true)]
        [ForeignKey("Image")]
        public int ImageID { get; set; }

        [Index("IX_ImageAndTag", 2, IsUnique = true)]
        [ForeignKey("Tag")]
        public string TagName { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Image Image { get; set; }
    }
}