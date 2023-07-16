using FoodApp.Models;
using FoodApp.Models.Identity;
using FoodApp.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<FoodAppUser>
    {
        public virtual DbSet<Ingredient> Ingridients { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<FavoriteRecipeUser> FavoriteRecipeUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //OneToMany

            builder.Entity<Recipe>()
            .HasMany(e => e.Ingridients)
            .WithOne(e => e.Recipe)
            .HasForeignKey(e => e.RecipeId);

            builder.Entity<FoodAppUser>()
            .HasMany(e => e.AddedRecipes)
            .WithOne(e => e.OwnerOfRecipe)
            .HasForeignKey(e => e.OwnerOfRecipeId);


            //ManyToMany

            builder.Entity<FavoriteRecipeUser>()
               .HasOne(z => z.Recipe)
               .WithMany(z => z.FavoriteRecipes)
               .HasForeignKey(z => z.RecipeId);

            builder.Entity<FavoriteRecipeUser>()
               .HasOne(z => z.User) // shto ima vo pogorniot entitet
               .WithMany(z => z.FavoriteRecipes) //vo kakva vrska e soodvetnoto svojstvo od negoviot model
               .HasForeignKey(z => z.UserId); //shto e nadvoreshniot kluch vo entitetot


            //OneToOne

            builder.Entity<CookingClasses>()
                .HasOne<Recipe>(z => z.Recipe)
                .WithOne(z => z.CookingClass)
                .HasForeignKey<CookingClasses>(z => z.RecipeId);
        }

        public DbSet<CookingClasses> CookingClasses { get; set; }

    }
}
