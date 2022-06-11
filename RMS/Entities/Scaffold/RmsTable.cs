using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("rms_table")]
    public partial class RmsTable
    {
        public RmsTable()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }
        [Column("capacity")]
        public int? Capacity { get; set; }
        [Column("status", TypeName = "tinyint")]
        public byte Status { get; set; }

        [InverseProperty(nameof(Order.Table))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
