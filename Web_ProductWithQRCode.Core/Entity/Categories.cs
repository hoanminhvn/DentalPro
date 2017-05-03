namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categories()
        {
            Articles = new HashSet<Articles>();
            Slideshows = new HashSet<Slideshows>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Meta { get; set; }

        public int? Parent { get; set; }

        public int? Type { get; set; }

        public bool? Status { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public int? lang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articles> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slideshows> Slideshows { get; set; }
    }
}
