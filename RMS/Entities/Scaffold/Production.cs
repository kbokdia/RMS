using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("productions")]
    [Index(nameof(ProductId), Name = "fk_productions_products")]
    public partial class Production
    {
        public Production()
        {
            ProductionMaterials = new HashSet<ProductionMaterial>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("start_date")]
        public DateTime? StartDate { get; set; }
        [Column("end_date")]
        public DateTime? EndDate { get; set; }
        [Column("quantity")]
        public double? Quantity { get; set; }
        [Column("weight")]
        public double? Weight { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("wastage")]
        public double? Wastage { get; set; }
        [Column("production_qr")]
        [StringLength(100)]
        public string ProductionQr { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Productions")]
        public virtual Product Product { get; set; }
        [InverseProperty(nameof(ProductionMaterial.Production))]
        public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; }
    }
}
