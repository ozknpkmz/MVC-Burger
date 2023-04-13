using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Claims;

namespace HamburgerMVC.Models
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<ExtraIngredient> ExtraIngredients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraIngredientOrder> ExtraIngredientOrders { get; set; }
        public DbSet<MenuOrder> MenuOrders { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ExtraIngredientOrder>().HasKey(x => new { x.OrderId, x.ExtraIngredientId });
            builder.Entity<ExtraIngredientOrder>().HasOne(x => x.ExtraIngredient).WithMany(x => x.Orders).HasForeignKey(x => x.ExtraIngredientId);
            builder.Entity<ExtraIngredientOrder>().HasOne(x => x.Order).WithMany(x => x.ExtraIngredients).HasForeignKey(x => x.OrderId);

            builder.Entity<MenuOrder>().HasKey(x => new { x.OrderId, x.MenuId });
            builder.Entity<MenuOrder>().HasOne(x => x.Menu).WithMany(x => x.Orders).HasForeignKey(x => x.MenuId);
            builder.Entity<MenuOrder>().HasOne(x => x.Order).WithMany(x => x.Menus).HasForeignKey(x => x.OrderId);
            builder.Entity<Order>().Property(x => x.Size).HasConversion<string>();

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Manager", NormalizedName = "MANAGER".ToUpper() });

            // Seed users
            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Email = "manager007@gmail.com",
                NormalizedEmail = "MANAGER007@GMAIL.COM",
                UserName = "manager007",
                NormalizedUserName = "MANAGER007",
                PasswordHash = hasher.HashPassword(null, "Asd123..."),
            });

            // Seed user roles
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210"
            });


            //builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Manager", NormalizedName = "MANAGER".ToUpper() });


            ////a hasher to hash the password before seeding the user to the db
            //var hasher = new PasswordHasher<AppUser>();


            ////Seeding the User to AspNetUsers table
            //builder.Entity<AppUser>().HasData(
            //    new AppUser
            //    {
            //        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
            //        UserName = "manager007",
            //        NormalizedUserName = "MANAGER007",
            //        PasswordHash = hasher.HashPassword(null, "Asd123..."),

            //    }
            //);

            ////Seeding the relation between our user and role to AspNetUserRoles table
            //builder.Entity<IdentityUserRole<string>>().HasData(
            //    new IdentityUserRole<string>
            //    {
            //        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            //        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
            //    }
            //);

        }


    }
}
