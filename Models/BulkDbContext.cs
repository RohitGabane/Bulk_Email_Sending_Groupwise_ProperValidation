using Microsoft.EntityFrameworkCore;

namespace Bulk_Email_Sending_Groupwise.Models
{
    public class BulkDbContext:DbContext
    {
        public BulkDbContext()
        {

        }
        public BulkDbContext(DbContextOptions<BulkDbContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<EmpDeptMapping>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.EmpDeptMapping)
                .HasForeignKey(d => d.Emp_ID);

            modelBuilder.Entity<EmpDeptMapping>()
                .HasOne(d => d.Department)
                .WithMany(dept => dept.EmpDeptMapping)
                .HasForeignKey(d => d.Dept_Id);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EmpDeptMapping> EmpDeptMapping { get; set; }

       
    }
}
