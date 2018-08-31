//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Striping
    {
        public int StripeID { get; set; }
        public int RoadID { get; set; }
        public decimal StripeFrom { get; set; }
        public decimal StripeTo { get; set; }
        public decimal StripeLength { get; set; }
        public string FromToDesc { get; set; }
        public int StripeStatusID { get; set; }
        public int ContractorID { get; set; }
        public Nullable<System.DateTime> StripeDateComp { get; set; }
        public int StripeMaterialID { get; set; }
        public int StripeTypeID { get; set; }
        public string StripeComment { get; set; }
        public string LCUser { get; set; }
        public System.DateTime LCDate { get; set; }
    
        public virtual LUConsolTable LUConsolTable { get; set; }
        public virtual LUConsolTable LUConsolTable1 { get; set; }
        public virtual LUConsolTable LUConsolTable2 { get; set; }
        public virtual LUContractor LUContractor { get; set; }
        public virtual MasterRoad MasterRoad { get; set; }
    }
}
