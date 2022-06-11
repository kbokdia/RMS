using RMS.Entities.Scaffold;
using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
   public enum UserType
   {
      Undefined = 0,
      Customer = 1,
      Staff = 2,
      Admin = 3,
   }
   public class CreateUserModel
   {
      [StringLength(45)]
      public string Name { get; set; }
      [StringLength(65)]
      public string Email { get; set; }
      [Required]
      [StringLength(15)]
      public string Mobile { get; set; }
      [StringLength(255)]
      public string Password { get; set; }
      public UserType Type { get; set; }
      public Status Status { get; set; }

      public static T ToModel<T>(User entity) where T : CreateUserModel, new()
      {
         var model = new T();
         model.Name = entity.Name;
         model.Email = entity.Email;
         model.Mobile = entity.Mobile;
         model.Type = (UserType)entity.Type;
         model.Status = (Status)entity.Status;
         return model;
      }

      public static User ToEntity<T>(T model) where T : CreateUserModel, new()
      {
         var entity = new User();
         entity.Name = model.Name;
         entity.Email = model.Email;
         entity.Mobile = model.Mobile;
         entity.Password = model.Password;
         entity.Type = (byte)model.Type;
         entity.Status = (byte)model.Status;
         return entity;
      }
   }
   public class UserModel : CreateUserModel
   {
      public int Id { get; set; }

      public new static T ToModel<T>(User entity) where T : UserModel, new()
      {
         var model = CreateUserModel.ToModel<T>(entity);
         model.Id = entity.Id;
         return model;
      }

      public new static User ToEntity<T>(T model) where T : UserModel, new()
      {
         var entity = CreateUserModel.ToEntity(model);
         entity.Id = entity.Id;
         return entity;
      }

   }
}
