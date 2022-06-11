using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("contacts")]
    [Index(nameof(AddressId), Name = "fk_address_id_address_idx")]
    public partial class Contact
    {
        public Contact()
        {
            Inventories = new HashSet<Inventory>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("mobile")]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Column("address_id")]
        public int? AddressId { get; set; }
        [Column("gst")]
        [StringLength(45)]
        public string Gst { get; set; }
        [Column("incharge_name")]
        [StringLength(45)]
        public string InchargeName { get; set; }
        [Column("incharge_mobile")]
        [StringLength(15)]
        public string InchargeMobile { get; set; }
        [Column("type", TypeName = "tinyint")]
        public byte Type { get; set; }
        [Column("is_active", TypeName = "tinyint")]
        public byte IsActive { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Contacts")]
        public virtual Address Address { get; set; }
        [InverseProperty(nameof(Inventory.Vendor))]
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
