using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BudgetModels
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext GetDbContext(string connectionString)
        {
            var budgetbuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            budgetbuilder.UseNpgsql(connectionString);
            var dbContext = new ApplicationDbContext(budgetbuilder.Options);
            return dbContext;
        }

        public static ApplicationDbContext GetDbContext(IConfiguration config, string connectionName = "DefaultConnection")
        {
            var budgetbuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            budgetbuilder.UseNpgsql(config.GetConnectionString(connectionName));
            var dbContext = new ApplicationDbContext(budgetbuilder.Options);
            return dbContext;
        }
    }
}
