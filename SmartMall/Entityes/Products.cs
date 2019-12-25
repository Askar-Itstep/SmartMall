namespace SmartMall
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Orders = new HashSet<Orders>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string name_prod { get; set; }

        [StringLength(50)]
        public string manufactur { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [StringLength(200)]
        public string imagePath { get; set; }

        public int? quantity_on_storage { get; set; }

        [Column(TypeName = "money")]
        public decimal purch_price { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
