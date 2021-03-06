using RMS.Entities.Scaffold;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RMS.Models
{
   public enum OrderStatus
   {
      Undefined = 0,
      Active = 1,
      Inactive = 2,
      Pending = 3,
      Completed = 4
   }
   public class CreateOrderModel
   {
      public string Mobile { get; set; }
      public int UserId { get; set; }
      [Required]
      public int TableId { get; set; }
      [StringLength(200)]
      public string Instructions { get; set; }
      public OrderStatus Status { get; set; }
      public CreateOrderItemModel[] Items { get; set; }

      public static T ToModel<T>(Order entity) where T : CreateOrderModel, new()
      {
         var model = new T();
         model.TableId = entity.TableId;
         model.Instructions = entity.Instructions;
         model.UserId = entity.UserId;
         model.Status = (OrderStatus)entity.Status;
         model.Items = entity.OrderItems.Select(i => CreateOrderItemModel.ToModel<CreateOrderItemModel>(i)).ToArray();
         return model;
      }

      public static Order ToEntity<T>(T model) where T : CreateOrderModel, new()
      {
         var entity = new Order();
         entity.TableId = model.TableId;
         entity.Instructions = model.Instructions;
         entity.UserId = model.UserId;
         entity.Status = (byte)model.Status;
         entity.OrderItems = model.Items
            .Select(i => CreateOrderItemModel.ToEntity(i))
            .ToList();
         return entity;
      }
   }

   public class OrderModel: CreateOrderModel
   {
      public int Id { get; set; }
      public new static T ToModel<T>(Order entity) where T : OrderModel, new()
      {
         var model = CreateOrderModel.ToModel<T>(entity);
         model.Id = entity.Id;
         return model;
      }

      public new static Order ToEntity<T>(T model) where T : OrderModel, new()
      {
         var entity = CreateOrderModel.ToEntity(model);
         entity.Id = entity.Id;
         return entity;
      }
   }

   public class GetOrderModel: OrderModel
   {
      public DateTime OrderDatetime { get; set; }
      public UserModel User { get; set; }
      public TableModel Table { get; set; }
      public new GetOrderItemModel[] Items { get; set; }

      public static GetOrderModel ToModel(Order entity)
      {
         var model = OrderModel.ToModel<GetOrderModel>(entity);

         if (entity.User != null)
            model.User = UserModel.ToModel<UserModel>(entity.User);

         if(entity.Table != null)
            model.Table = TableModel.ToModel<TableModel>(entity.Table);

         model.OrderDatetime = entity.OrderDatetime;
         model.Items = entity.OrderItems.Select(i => GetOrderItemModel.ToModel<GetOrderItemModel>(i)).ToArray();

         return model;
      }
   }

   public class CreateOrderItemModel
   {
      public int MenuId { get; set; }
      public int Quantity { get; set; }

      public static T ToModel<T>(OrderItem entity) where T : CreateOrderItemModel, new()
      {
         var item = new T();
         item.MenuId = entity.MenuId;
         item.Quantity = entity.Quantity ?? 1;
         return item;
      }

      public static OrderItem ToEntity<T>(T model) where T : CreateOrderItemModel, new()
      {
         var entity = new OrderItem();
         entity.MenuId = model.MenuId;
         entity.Quantity = model.Quantity;
         return entity;
      }
   }

   public class OrderItemModel: CreateOrderItemModel
   {
      public int Id { get; set; }
      public new static T ToModel<T>(OrderItem entity) where T : OrderItemModel, new()
      {
         var model = CreateOrderItemModel.ToModel<T>(entity);
         model.Id = entity.Id;
         return model;
      }
   }

   public class GetOrderItemModel: OrderItemModel
   {
      public MenuModel Menu { get; set; }

      public new static T ToModel<T>(OrderItem entity) where T : GetOrderItemModel, new()
      {
         var model = OrderItemModel.ToModel<T>(entity);
         if(entity.Menu != null)
            model.Menu = MenuModel.ToModel<MenuModel>(entity.Menu);
         return model;
      }
   }
}
