using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CaseManagmentAPI.Models
{
    public class UserResponseList
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<_User> users { get; set; }
        public int total { get; set; }
    }

    public class _User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public System.Nullable<System.DateTime> LastLogin { get; set; }
        public bool PasswordExpired { get; set; }
    }

    public class UserResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public cUser User { get; set; }


    }

    public class deleteList
    {
        public List<int> ids { get; set; }
    }
    public class cUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public System.Nullable<System.DateTime> Created { get; set; }
        public string LastModifiedBy { get; set; }
        public System.Nullable<System.DateTime> LastLogin { get; set; }
        public System.Nullable<System.DateTime> LastUpdateTime { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public bool PasswordExpired { get; set; }

    }

    public class User

    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string? Token { get; set; }

    }

    public enum Roles

    {

        Admin = 1,
        CMUser = 2

    }

    public class Iresp

    {

        public int Code { get; set; }

        public static string Message { get; set; }

        public Iresp(int code, string message)

        {





            Code = code;

            Message = message;





        }

    }


}
