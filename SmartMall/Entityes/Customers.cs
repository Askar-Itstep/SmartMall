namespace SmartMall
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customers()
        {
            Debitors = new HashSet<Debitors>();
            Orders = new HashSet<Orders>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string fullname_customer { get; set; }

        public int age_custom { get; set; }

        [Required]
        [StringLength(50)]
        public string login_custom { get; set; }

        [Required]
        [StringLength(50)]
        public string password_custom { get; set; }

        public int? role_id { get; set; }

        [Column(TypeName = "text")]
        public string address { get; set; }

        [StringLength(20)]
        public string phoneNum { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Debitors> Debitors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
