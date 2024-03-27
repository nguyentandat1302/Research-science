using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Research_science.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Apply> Apply { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<LoaiUser> LoaiUser { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apply>()
                .Property(e => e.CV)
                .IsUnicode(false);

            modelBuilder.Entity<Apply>()
                .Property(e => e.Coverletter)
                .IsUnicode(false);

            modelBuilder.Entity<Apply>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.JobName)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .HasMany(e => e.Language)
                .WithMany(e => e.Job)
                .Map(m => m.ToTable("JobLanguage").MapLeftKey("JobID").MapRightKey("LanguageID"));

            modelBuilder.Entity<Job>()
                .HasMany(e => e.Skill)
                .WithMany(e => e.Job)
                .Map(m => m.ToTable("JobSkill").MapLeftKey("JobID").MapRightKey("SkillID"));

            modelBuilder.Entity<Language>()
                .Property(e => e.NameLanguage)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiUser>()
                .Property(e => e.TenLoai)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Skill>()
                .Property(e => e.SkillName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.LogoCompany)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Wallet)
                .IsUnicode(false);
        }
    }
}
