using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("inventories")]
    [Index(nameof(VendorId), Name = "fk_inventories_contacts")]
    [Index(nameof(ProductId), Name = "fk_inventories_products")]
    public partial class Inventory
    {
        public Inventory()
        {
            ProductionMaterials = new HashSet<ProductionMaterial>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("vendor_id")]
        public int? VendorId { get; set; }
        [Column("quantity")]
        public double? Quantity { get; set; }
        [Column("weight")]
        public double? Weight { get; set; }
        [Column("purchase_datetime")]
        public DateTime? PurchaseDatetime { get; set; }
        [Column("qr_code")]
        [StringLength(100)]
        public string QrCode { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("last_modified")]
        public DateTime? LastModified { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Inventories")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(VendorId))]
        [InverseProperty(nameof(Contact.Inventories))]
        public virtual Contact Vendor { get; set; }
        [InverseProperty(nameof(ProductionMaterial.Inventory))]
        public virtual ICollection<ProductionMaterial> ProductionMaterials { get; set; }
    }
}
