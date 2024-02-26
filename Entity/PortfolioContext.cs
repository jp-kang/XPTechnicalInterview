using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using XPTechnicalInterview.Domain;

namespace XPTechnicalInterview.Entity
{
    public class PortfolioContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<FinancialProduct> FinancialProducts { get; set; }
        public DbSet<Investment> Investments { get; set; }

        public string DbPath { get; }

        public PortfolioContext()
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join("Data/porfolio.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

    }
}
