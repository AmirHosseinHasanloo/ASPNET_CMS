//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(PagesMetaData))]
    public partial class Pages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pages()
        {
            this.Comments = new HashSet<Comments>();
            this.Tags = new HashSet<Tags>();
        }
    
        public int PageID { get; set; }
        public int GroupID { get; set; }
        public string PageTitle { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public string ImageName { get; set; }
        public int Visit { get; set; }
        public bool ShowInSlider { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Page_Groups Page_Groups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tags> Tags { get; set; }
    }
}
