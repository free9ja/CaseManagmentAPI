namespace CaseManagmentAPI.Models
{
    public class CMUser
    {
        public int ID { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public System.Nullable<System.DateTime> Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public System.Nullable<System.DateTime> LastLogin { get; set; }
        public System.Nullable<System.DateTime> LastUpdateTime { get; set; }
        public bool IsActive { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public bool PasswordExpired { get; set; }
        public string? PhoneNumberOne { get; set; }
        public string? PhoneNumberTwo { get; set; }
        public bool IsDeleted { get; set; }
        // public int numberOfRows { get; set; }
    }

    public class Role
    {
        public int ID { get; set; }
        public string? UserName { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public System.Nullable<System.DateTime> Created { get; set; }
        public System.Nullable<System.DateTime> LastUpdateTime { get; set; }
    }

}
