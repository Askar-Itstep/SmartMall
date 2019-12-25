namespace SmartMall
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Debitors
    {
        public int id { get; set; }

        public int? custom_id { get; set; }

        public int? order_id { get; set; }

        [Column(TypeName = "money")]
        public decimal? sum_begin_debit { get; set; }

        [Column(TypeName = "money")]
        public decimal? new_pay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_new_pay { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? current_sum_debit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_plan_repay { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
