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
    
    public partial class RightOfWay
    {
        public int RightOfWayID { get; set; }
        public string RefType { get; set; }
        public int RefID { get; set; }
        public int ROWTypeID { get; set; }
        public Nullable<int> ROWWidth { get; set; }
        public decimal ROWFrom { get; set; }
        public decimal ROWTo { get; set; }
        public decimal ROWLength { get; set; }
        public string FromToDesc { get; set; }
        public string ROWComment { get; set; }
        public Nullable<System.DateTime> DeedRecordDate { get; set; }
        public string DeedBook { get; set; }
        public string DeedPage { get; set; }
        public string LCUser { get; set; }
        public System.DateTime LCDate { get; set; }
    
        public virtual LUConsolTable LUConsolTable { get; set; }
    }
}
