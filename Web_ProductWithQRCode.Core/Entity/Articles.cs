namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articles
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Intro { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDateTime { get; set; }

        [StringLength(1000)]
        public string Meta { get; set; }

        public int? CateID { get; set; }

        public int? Views { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual Users Users { get; set; }
    }
}
