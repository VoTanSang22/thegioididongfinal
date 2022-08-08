using Microsoft.EntityFrameworkCore;
namespace thegioididong.Models
{
    public class MyDBConext : DbContext
    {
        public MyDBConext(DbContextOptions options) : base(options) { }
        #region DbSet
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
       // public DbSet<Order_Detail> Order_Detail { get; set; }
        #endregion      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            //configuration.GetConnectionString("")
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //tạo dữ liệu ban đầu cho db nếu muốn
            
        }
    }
}
