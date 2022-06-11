using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RMS.Entities.Scaffold
{
    [Table("order_items")]
    [Index(nameof(MenuId), Name = "fk_menu_id_menu_idx")]
    [Index(nameof(OrderId), Name = "fk_order_id_order_idx")]
    public partial class OrderItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("menu_id")]
        public int MenuId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey(nameof(MenuId))]
        [InverseProperty("OrderItems")]
        public virtual Menu Menu { get; set; }
        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderItems")]
        public virtual Order Order { get; set; }
    }
}
