using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("transports")]
    [Index(nameof(AddressId), Name = "fk_transport_address_idx")]
    public partial class Transport
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }
        [Column("incharge_name")]
        [StringLength(45)]
        public string InchargeName { get; set; }
        [Column("incharge_mobile")]
        [StringLength(15)]
        public string InchargeMobile { get; set; }
        [Column("address_id")]
        public int? AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Transports")]
        public virtual Address Address { get; set; }
    }
}
