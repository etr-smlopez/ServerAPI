using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Models;

namespace ServerAPI.SQLAccess
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public DbSet<SalesOrderModel> SalesOrder { get; set; }
        public DbSet<ReturnsModel> Returns { get; set; }
        public DbSet<InvoiceModel> Invoice { get; set; }
        public DbSet<StockTransferModel> StockTransfer { get; set; }
        public DbSet<StockWithdrawalModel> StockWithdrawal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderModel>().HasNoKey().ToView("DashboardSalesOrder");
            modelBuilder.Entity<ReturnsModel>().HasNoKey().ToView("DashboardReturns");
            modelBuilder.Entity<InvoiceModel>().HasNoKey().ToView("DashboardInvoice");
            modelBuilder.Entity<StockTransferModel>().HasNoKey().ToView("DashboardStockTransfer");
            modelBuilder.Entity<StockWithdrawalModel>().HasNoKey().ToView("DashboardStockwithdrawal");
        }
 
    }
}
