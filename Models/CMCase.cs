
namespace CaseManagmentAPI.Models
{
    public class CMCase
    {
        public int ID { get; set; }
        public string description { get; set; }
        public System.Nullable<System.DateTime> createdAt { get; set; }
        public System.Nullable<System.DateTime> resolvedAt { get; set; }
        public string status { get; set; }
        public int caseTypeId { get; set; }
        public CMCaseType cMCaseType { get; set; }
        public int customerId { get; set; }
        public CMCustomer cMCustomer { get; set; }
        public int agentId { get; set; }
        public CMAgent cMAgent { get; set; }
    }

    public class CMCaseType
    {
        public int ID { get; set; }
        public string name { get; set; }
    }

    public class CMCustomer
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string email { get; set; }
        public System.Nullable<System.DateTime> createdAt { get; set; }
        public System.Nullable<System.DateTime> updatedAt { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class CMAgent
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string email { get; set; }
        public System.Nullable<System.DateTime> createdAt { get; set; }
        public System.Nullable<System.DateTime> updatedAt { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
