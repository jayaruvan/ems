//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.Core.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        public Supplier()
        {
            this.Meters = new HashSet<Meter>();
            this.Reports = new HashSet<Report>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public string IP { get; set; }
    
        public virtual ICollection<Meter> Meters { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
