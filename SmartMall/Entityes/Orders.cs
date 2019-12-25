namespace SmartMall
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Debitors = new HashSet<Debitors>();
        }

        public int id { get; set; }

        public int? custom_id { get; set; }

        public int? prod_id { get; set; }

        public int number_item { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_ship { get; set; }

        [Column(TypeName = "money")]
        public decimal sum_pay { get; set; }

        [Column(TypeName = "money")]
        public decimal sum_order { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? sum_debit { get; set; }

        public int? seller_id { get; set; }

        public virtual Customers Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Debitors> Debitors { get; set; }

        public virtual Employees Employees { get; set; }

        public virtual Products Products { get; set; }
    }
}
