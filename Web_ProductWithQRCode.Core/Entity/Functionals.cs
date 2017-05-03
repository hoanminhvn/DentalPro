namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Functionals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Functionals()
        {
            Permissions = new HashSet<Permissions>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? ModuleID { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permissions> Permissions { get; set; }

        public virtual Modules Modules { get; set; }
    }
}
