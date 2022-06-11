using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("addresses")]
    public partial class Address
    {
        public Address()
        {
            Contacts = new HashSet<Contact>();
            Transports = new HashSet<Transport>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }
        [Column("address1")]
        [StringLength(60)]
        public string Address1 { get; set; }
        [Column("address2")]
        [StringLength(60)]
        public string Address2 { get; set; }
        [Column("area")]
        [StringLength(45)]
        public string Area { get; set; }
        [Column("city")]
        [StringLength(45)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(45)]
        public string State { get; set; }
        [Column("pincode")]
        [StringLength(10)]
        public string Pincode { get; set; }

        [InverseProperty(nameof(Contact.Address))]
        public virtual ICollection<Contact> Contacts { get; set; }
        [InverseProperty(nameof(Transport.Address))]
        public virtual ICollection<Transport> Transports { get; set; }
    }
}
