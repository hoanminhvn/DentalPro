namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Articles = new HashSet<Articles>();
            Products = new HashSet<Products>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? Gender { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        public int? GroupID { get; set; }

        public bool? Status { get; set; }

        [Required]
        [MaxLength(200)]
        public byte[] Password { get; set; }

        public DateTime? Birthday { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Articles> Articles { get; set; }

        public virtual GroupsUser GroupsUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
