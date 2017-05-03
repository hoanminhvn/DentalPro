namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Permissions
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FunctionalID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupID { get; set; }

        public int? ValueRights { get; set; }

        public virtual Functionals Functionals { get; set; }

        public virtual GroupsUser GroupsUser { get; set; }
    }
}
