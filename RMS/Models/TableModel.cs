using RMS.Entities.Scaffold;
using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
   public enum Status
   {
      Undefined = 0,
      Active = 1,
      Inactive = 2,
   }

   public enum TableStatus
   {
      Undefined = Status.Undefined,
      Active = Status.Active,
      Inactive = Status.Inactive,
      Available = 3,
      Occupied = 4
   }
   public class CreateTableModel
   {
      [StringLength(45)]
      public string Name { get; set; }
      public int? Capacity { get; set; }
      public TableStatus Status { get; set; }

      public static T ToModel<T>(RmsTable entity) where T : CreateTableModel, new()
      {
         var model = new T();
         model.Name = entity.Name;
         model.Capacity = entity.Capacity;
         model.Status = (TableStatus)entity.Status;
         return model;
      }

      public static RmsTable ToEntity<T>(T model) where T : CreateTableModel, new()
      {
         var entity = new RmsTable();
         entity.Name = model.Name;
         entity.Capacity = model.Capacity;
         entity.Status = (byte)model.Status;
         return entity;
      }
   }

   public class TableModel: CreateTableModel
   {
      public int Id { get; set; }

      public new static T ToModel<T>(RmsTable entity) where T : TableModel, new()
      {
         var model = CreateTableModel.ToModel<T>(entity);
         model.Id = entity.Id;
         return model;
      }

      public new static RmsTable ToEntity<T>(T model) where T : TableModel, new()
      {
         var entity = CreateTableModel.ToEntity(model);
         entity.Id = entity.Id;
         return entity;
      }
   }
}
