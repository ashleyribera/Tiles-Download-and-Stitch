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
    
    public partial class PlatAlias
    {
        public int PlatAliasID { get; set; }
        public int MasterPlatID { get; set; }
        public string PlatAliasName { get; set; }
        public string PlatAliasComment { get; set; }
        public string LCUser { get; set; }
        public System.DateTime LCDate { get; set; }
    
        public virtual SubdivisionPlat SubdivisionPlat { get; set; }
    }
}
