using System.Data.Entity;
using MasterWebService.Models.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MasterWebService.Models
{
    public class AddressContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public AddressContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AddressContext>());
        }

        /*    public static AddressContext Create()
            {
                return new AddressContext();
            }*/

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AddressContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<CustomerAddress> Address { get; set; }
    }
} 