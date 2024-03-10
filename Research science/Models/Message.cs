namespace Research_science.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDMessage { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public DateTime? SendDate { get; set; }

        public int? IDCustomer { get; set; }

        public int? IDEmployer { get; set; }

        public int? IDJob { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employer Employer { get; set; }

        public virtual Job Job { get; set; }
    }
}
