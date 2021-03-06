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
    
    public partial class Report
    {
        public Report()
        {
            this.MeterReadings = new HashSet<MeterReading>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AlteredDate { get; set; }
        public string IP { get; set; }

        public System.DateTime ReportDate { get; set; }
        public int SupplierId { get; set; }
    
        public virtual ICollection<MeterReading> MeterReadings { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
