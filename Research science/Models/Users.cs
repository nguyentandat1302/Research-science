namespace Research_science.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Apply = new HashSet<Apply>();
            Job = new HashSet<Job>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string LogoCompany { get; set; }

        public int? Phone { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Decsription { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CDCD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(300)]
        public string Wallet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apply> Apply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Job { get; set; }
    }
}
