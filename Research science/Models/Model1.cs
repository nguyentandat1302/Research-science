using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Research_science.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employer> Employer { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Proposal> Proposal { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Fullname)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Employer>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Job>()
                .Property(e => e.Budget)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Job>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Proposal>()
                .Property(e => e.ProposalText)
                .IsUnicode(false);

            modelBuilder.Entity<Skill>()
                .Property(e => e.SkillName)
                .IsUnicode(false);
        }
    }
}
