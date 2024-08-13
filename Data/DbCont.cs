using Microsoft.EntityFrameworkCore;
using StripeWebApp.Models;
namespace StripeWebApp.Data
{
    public class DbCont:DbContext
    {
        public  DbCont (DbContextOptions<DbCont> options):base(options){
        }
       public DbSet<Item> Items { get; set; }
    }
}
