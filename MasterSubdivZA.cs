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
    
    public partial class MasterSubdivZA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterSubdivZA()
        {
            this.SubDivZAs = new HashSet<SubDivZA>();
        }
    
        public int MasterSubdivZAID { get; set; }
        public int MasterSubID { get; set; }
        public string ZoningAppNo { get; set; }
        public System.DateTime ZoningAppDate { get; set; }
        public string ZoningAppComment { get; set; }
        public string LCUser { get; set; }
        public System.DateTime LCDate { get; set; }
    
        public virtual MasterSubdiv MasterSubdiv { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubDivZA> SubDivZAs { get; set; }
    }
}
