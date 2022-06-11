using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("related_products")]
    [Index(nameof(ProductId), Name = "fk_product_id_idx")]
    [Index(nameof(RelatedProductId), Name = "fk_related_product_id_idx")]
    public partial class RelatedProduct
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Key]
        [Column("related_product_id")]
        public int RelatedProductId { get; set; }
        [Column("quantity")]
        public double? Quantity { get; set; }
        [Column("weight")]
        public double? Weight { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("RelatedProductProducts")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(RelatedProductId))]
        [InverseProperty("RelatedProductRelatedProductNavigations")]
        public virtual Product RelatedProductNavigation { get; set; }
    }
}
