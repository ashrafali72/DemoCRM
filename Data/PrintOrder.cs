using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PrintOrderClass
    {
        public int Id { get; set; }
        public string? OrderCode { get; set; }
        public string? OrderType { get; set; }
        public string? BrandName { get; set; }
        public string? LabelClaim { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string? Rate { get; set; }
        public string? MRP { get; set; }
        public string? PackType { get; set; }
        public string? PackSize { get; set; }
        public string? TabCapType { get; set; }
        public string? TabCapSize { get; set; }
        public Nullable<decimal> CylinderCharge { get; set; }
        public Nullable<decimal> ApprovalCharge { get; set; }
        public Nullable<decimal> ItemSecurity { get; set; }
        public string? Comments { get; set; }
        public string? Remarks { get; set; }
        public string? AssignTo { get; set; }
        public string? PvcColor { get; set; }
        public string? PaymentType { get; set; }
        public int? ExpiryMonth { get; set; }
        public Nullable<System.DateTime> AcceptedDate { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<int> PreQuantity { get; set; }
        public string? PreRate { get; set; }
    }
}
