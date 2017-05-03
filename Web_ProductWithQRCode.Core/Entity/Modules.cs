namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Modules
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modules()
        {
            Functionals = new HashSet<Functionals>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Functionals> Functionals { get; set; }
    }
}
