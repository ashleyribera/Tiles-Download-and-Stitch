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
    
    public partial class CommercialSitesAudit
    {
        public int CommSiteAuditID { get; set; }
        public int CommSiteID { get; set; }
        public int MasterSubID { get; set; }
        public string SiteName { get; set; }
        public Nullable<int> EntranceRoadID { get; set; }
        public int ConstInspID { get; set; }
        public int SWInspID { get; set; }
        public int BOCDistrictID { get; set; }
        public Nullable<System.DateTime> StorageDate { get; set; }
        public string StorageLoc { get; set; }
        public bool SWFacInspMaintCov { get; set; }
        public decimal Acreage { get; set; }
        public int DeveloperID { get; set; }
        public string LCUser { get; set; }
        public System.DateTime LCDate { get; set; }
    
        public virtual LUBOCDistrict LUBOCDistrict { get; set; }
        public virtual LUConstInspector LUConstInspector { get; set; }
        public virtual LUDeveloper LUDeveloper { get; set; }
        public virtual MasterSubdiv MasterSubdiv { get; set; }
        public virtual LUSWInspector LUSWInspector { get; set; }
    }
}