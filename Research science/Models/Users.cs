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
        public int UserID { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

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

        public string Image { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(300)]
        public string Wallet { get; set; }

        [StringLength(100)]
        public string Business { get; set; }

        public int? CanCuoc { get; set; }


        [NotMapped]
        //[Compare("Password")]
        public string ConfirmPass { get; set; }
        public bool Accpet { get; set; }

        public int? MaLoaiUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apply> Apply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Job> Job { get; set; }

        public virtual LoaiUser LoaiUser { get; set; }
    }
}
