using Microsoft.EntityFrameworkCore;
using W8_Backend.Data.Entities;

namespace W8_Backend.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;


        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;

        }

        //Defining the tables of W8 Database
        public DbSet<Users> Users { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<LogDetails> LogDetails { get; set; }
        public DbSet<SystemVariables> SystemVariables { get; set; }

        public DbSet<EmployeeMonthlyWorkDifferences> EmployeeMonthlyWorkDifferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Defining Relationships between The tables of the W8 Database
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.HasIndex(c => c.UserName) // Create an index
                .IsUnique(); // that is unique
                entity.HasIndex(c => c.MicroUserID) // Create an index
                .IsUnique(); // that is unique


            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.CompanyID);

            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(x => x.LogID);

            });

            modelBuilder.Entity<EmployeeMonthlyWorkDifferences>(entity =>
            {
                entity.HasKey(x => new { x.Month, x.Year, x.Emp_number, x.CodeNumber , x.CodeDesc, x.CompanyName }); ;
            });

            modelBuilder.Entity<LogDetails>(entity =>
            {
                entity.HasKey(x => x.DetailID);
                entity.HasOne(e => e.Log).WithMany(e => e.LogDetails).HasForeignKey(x => x.LogID).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SystemVariables>(entity =>
            {
                entity.HasKey(x => x.VariableID);

            });




        }
    }
}
