//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomerMessage
    {
        public long ID { get; set; }
        public Nullable<System.DateTime> colEffectiveDate { get; set; }
        public Nullable<System.DateTime> colEndDate { get; set; }
        public long colFKDMVId { get; set; }
        public Nullable<byte> colIsForAllCMF { get; set; }
        public string colMessage { get; set; }
        public string PropertyName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<long> FKUserMaintId_LastModifiedBy { get; set; }
        public Nullable<long> FKUserMaintId_CreatedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public byte[] timestamp { get; set; }
    }
}