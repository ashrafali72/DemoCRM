using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCRM.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? PageUrl { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string? CssClass { get; set; }
        public Nullable<int> ParentOrder { get; set; }
        public Nullable<int> ChildOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public string? CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string? ModifiedBy { get; set; }

        //public string ParentName { get; set; }
    }
}
