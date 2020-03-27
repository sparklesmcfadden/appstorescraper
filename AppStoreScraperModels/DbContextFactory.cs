using BudgetModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json.Linq;

namespace Budget

{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
        {
            var o1 = JObject.Parse(System.IO.File.ReadAllText("appsettings.json"));
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(o1["ConnectionStrings"]["DefaultConnection"].Value<string>());
            return new ApplicationDbContext(builder.Options);
        }
    }
}
