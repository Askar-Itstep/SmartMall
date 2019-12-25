namespace SmartMall
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            Orders = new HashSet<Orders>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string fullname_emp { get; set; }

        public int age_emp { get; set; }

        [Required]
        [StringLength(50)]
        public string login_emp { get; set; }

        [Required]
        [StringLength(50)]
        public string password_emp { get; set; }

        public int? role_id { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
