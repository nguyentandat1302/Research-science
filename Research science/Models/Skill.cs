namespace Research_science.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Skill")]
    public partial class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDSkill { get; set; }

        [StringLength(255)]
        public string SkillName { get; set; }

        public int? Experience { get; set; }

        public int? IDCustomer { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
