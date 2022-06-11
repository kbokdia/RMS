using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("orders")]
    [Index(nameof(TableId), Name = "fk_table_id_rms_table_idx")]
    [Index(nameof(UserId), Name = "fk_user_id_user_idx")]
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("table_id")]
        public int TableId { get; set; }
        [Column("instructions")]
        [StringLength(200)]
        public string Instructions { get; set; }
        [Column("order_datetime")]
        public DateTime OrderDatetime { get; set; }
        [Column("status", TypeName = "tinyint")]
        public byte Status { get; set; }

        [ForeignKey(nameof(TableId))]
        [InverseProperty(nameof(RmsTable.Orders))]
        public virtual RmsTable Table { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Orders")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(OrderItem.Order))]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
