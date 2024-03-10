namespace Research_science.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Proposal")]
    public partial class Proposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDProposal { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "text")]
        public string ProposalText { get; set; }

        public DateTime? Submission { get; set; }

        public int? IDJob { get; set; }

        public int? IDCustomer { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Job Job { get; set; }
    }
}
