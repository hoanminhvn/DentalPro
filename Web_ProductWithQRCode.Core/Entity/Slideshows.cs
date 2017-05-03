namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slideshows
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? CateID { get; set; }

        [StringLength(1000)]
        public string Image { get; set; }

        public bool? Status { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
