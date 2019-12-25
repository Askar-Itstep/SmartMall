namespace SmartMall
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StatisticSeller")]
    public partial class StatisticSeller
    {
        [Key]
        [StringLength(50)]
        public string fullname_emp { get; set; }

        public int? num_sell { get; set; }

        [Column(TypeName = "money")]
        public decimal? sum_cash { get; set; }
    }
}
