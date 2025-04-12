using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebBanHangOnline.Models.EF
{
    [Table("tb_ProductSize")]
    public class ProductSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Size { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}