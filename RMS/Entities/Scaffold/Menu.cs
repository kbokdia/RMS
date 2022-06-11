using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("menu")]
    public partial class Menu
    {
        public Menu()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(75)]
        public string Name { get; set; }
        [Column("category_type")]
        [StringLength(75)]
        public string CategoryType { get; set; }
        [Column("price")]
        public double? Price { get; set; }
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
        [Column("image_url")]
        [StringLength(200)]
        public string ImageUrl { get; set; }
        [Column("tags")]
        [StringLength(500)]
        public string Tags { get; set; }
        [Column("isVeg", TypeName = "tinyint")]
        public byte? IsVeg { get; set; }
        [Column("status", TypeName = "tinyint")]
        public byte Status { get; set; }

        [InverseProperty(nameof(OrderItem.Menu))]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
