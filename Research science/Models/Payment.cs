namespace Research_science.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDPayment { get; set; }

        public decimal? Amount { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public DateTime? TransactionDate { get; set; }

        public int? IDJob { get; set; }

        public int? IDCustomer { get; set; }

        public int? IDEmployer { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employer Employer { get; set; }

        public virtual Job Job { get; set; }
    }
}
