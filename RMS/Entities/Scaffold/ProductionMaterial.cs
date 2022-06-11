using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("production_materials")]
    [Index(nameof(InventoryId), Name = "fk_production_materials_inventory_id_idx")]
    [Index(nameof(ProductionId), Name = "fk_production_materials_production_id_idx")]
    public partial class ProductionMaterial
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("production_id")]
        public int ProductionId { get; set; }
        [Column("inventory_id")]
        public int InventoryId { get; set; }
        [Column("quantity")]
        public double? Quantity { get; set; }
        [Column("weight")]
        public double? Weight { get; set; }

        [ForeignKey(nameof(InventoryId))]
        [InverseProperty("ProductionMaterials")]
        public virtual Inventory Inventory { get; set; }
        [ForeignKey(nameof(ProductionId))]
        [InverseProperty("ProductionMaterials")]
        public virtual Production Production { get; set; }
    }
}
