using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("users")]
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }
        [Column("email")]
        [StringLength(65)]
        public string Email { get; set; }
        [Required]
        [Column("mobile")]
        [StringLength(15)]
        public string Mobile { get; set; }
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }
        [Column("type", TypeName = "tinyint")]
        public byte Type { get; set; }
        [Column("status", TypeName = "tinyint")]
        public byte Status { get; set; }

        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
