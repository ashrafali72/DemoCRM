using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCRM.Model
{ 
    public class User
    {
        public int Id { get; set; }
        public Nullable<int> UserTypeId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string? Name { get; set; }
        public string? Comments { get; set; }
        public string? OrgName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Department { get; set; }
        public byte[]? Photograph { get; set; }
        public byte[]? Logo { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string? PANNumber { get; set; }
        public string? CSTNumber { get; set; }
        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public System.DateTime Modified { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Email { get; set; }
        public string? UserIP { get; set; }
        public string? AssignedMenuId { get; set; }


        public System.DateTime? LastLoginDate { get; set; }
        //public string RoleName { get; set; }
   
    }
}
