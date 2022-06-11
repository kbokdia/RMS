using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("products")]
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
            Productions = new HashSet<Production>();
            RelatedProductProducts = new HashSet<RelatedProduct>();
            RelatedProductRelatedProductNavigations = new HashSet<RelatedProduct>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("type", TypeName = "tinyint")]
        public byte Type { get; set; }
        [Column("product_qr")]
        [StringLength(100)]
        public string ProductQr { get; set; }
        [Column("category_type", TypeName = "tinyint")]
        public byte? CategoryType { get; set; }
        [Column("metric_type", TypeName = "tinyint")]
        public byte MetricType { get; set; }
        [Column("is_active", TypeName = "tinyint")]
        public byte IsActive { get; set; }

        [InverseProperty(nameof(Inventory.Product))]
        public virtual ICollection<Inventory> Inventories { get; set; }
        [InverseProperty(nameof(Production.Product))]
        public virtual ICollection<Production> Productions { get; set; }
        [InverseProperty(nameof(RelatedProduct.Product))]
        public virtual ICollection<RelatedProduct> RelatedProductProducts { get; set; }
        [InverseProperty(nameof(RelatedProduct.RelatedProductNavigation))]
        public virtual ICollection<RelatedProduct> RelatedProductRelatedProductNavigations { get; set; }
    }
}
