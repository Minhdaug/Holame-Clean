using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_NewsCategory")]
    public class NewsCategory : CommonAbstract
    {
        public NewsCategory()
        {
            this.News = new HashSet<News>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Alias { get; set; }

        public string Description { get; set; }

        [StringLength(250)]
        public string Icon { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        [StringLength(500)]
        public string SeoDescription { get; set; }

        [StringLength(250)]
        public string SeoKeywords { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual NewsCategory Parent { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}