namespace Web_ProductWithQRCode.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Intro { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [StringLength(1000)]
        public string Meta { get; set; }

        [StringLength(100)]
        public string CateID { get; set; }

        public int? Views { get; set; }

        public bool? Status { get; set; }

        public int? Lang { get; set; }

        [Column(TypeName = "ntext")]
        public string Tag { get; set; }

        [Column(TypeName = "ntext")]
        public string Digital { get; set; }

        public double? Price { get; set; }

        public double? Discount { get; set; }

        [Column(TypeName = "ntext")]
        public string ListImage { get; set; }

        [Column(TypeName = "ntext")]
        public string QRCode { get; set; }

        public int? CreateBy { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public virtual Users Users { get; set; }
    }
}
