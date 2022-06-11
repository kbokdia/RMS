using RMS.Entities.Scaffold;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RMS.Models
{
   public class CreateMenuModel
   {

      [StringLength(75)]
      public string Name { get; set; }

      [StringLength(75)]
      public string CategoryType { get; set; }

      public double? Price { get; set; }

      [StringLength(500)]
      public string Description { get; set; }

      [StringLength(500)]
      public string ImageUrl { get; set; }

      [StringLength(500)]
      public string Tags { get; set; }
      public string[] TagsList { get; set; }

      public bool IsVeg { get; set; }
      public Status Status { get; set; }

      public static T ToModel<T>(Menu entity) where T : CreateMenuModel, new()
      {
         var model = new T();
         model.Name = entity.Name;
         model.CategoryType = entity.CategoryType;
         model.Price = entity.Price;
         model.Description = entity.Description;
         model.ImageUrl = entity.ImageUrl;
         model.IsVeg = entity.IsVeg <= 1;
         model.Status = (Status)entity.Status;
         model.Tags = entity.Tags;
         model.TagsList = entity.Tags?.Split(',')?
            .Select(tag => tag.Trim())?.ToArray();

         return model;
      }

      public static Menu ToEntity<T>(T model) where T : CreateMenuModel, new()
      {
         var entity = new Menu();
         entity.Name = model.Name;
         entity.CategoryType = model.CategoryType;
         entity.Price = model.Price;
         entity.Description = model.Description;
         entity.ImageUrl = model.ImageUrl;
         entity.IsVeg = (byte)(model.IsVeg ? 1 : 2);
         entity.Status = (byte)model.Status;
         entity.Tags = string.Join(",", model.TagsList);
         return entity;
      }

   }
   public class MenuModel : CreateMenuModel
   {
      public int Id { get; set; }
      public new static T ToModel<T>(Menu entity) where T : MenuModel, new()
      {
         var model = CreateMenuModel.ToModel<T>(entity);
         model.Id = entity.Id;
         return model;
      }

      public new static Menu ToEntity<T>(T model) where T : MenuModel, new()
      {
         var entity = CreateMenuModel.ToEntity(model);
         entity.Id = entity.Id;
         return entity;
      }
   }
}
