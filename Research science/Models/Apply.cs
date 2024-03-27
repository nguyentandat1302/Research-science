namespace Research_science.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Apply")]
    public partial class Apply
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplyID { get; set; }

        public int? UserID { get; set; }

        public int? JobID { get; set; }

        [StringLength(255)]
        public string CV { get; set; }

        [StringLength(255)]
        public string Coverletter { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TimeAppointment { get; set; }

        [StringLength(500)]
        public string Status { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public virtual Job Job { get; set; }

        public virtual Users Users { get; set; }
    }
}
