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
    
    public partial class BondComment
    {
        public int BondCommentID { get; set; }
        public int BondID { get; set; }
        public string BondComment1 { get; set; }
        public string LCUser { get; set; }
        public System.DateTime LCDate { get; set; }
    
        public virtual MasterBond MasterBond { get; set; }
    }
}
