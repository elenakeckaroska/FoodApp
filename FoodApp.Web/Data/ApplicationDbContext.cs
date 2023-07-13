using FoodApp.Web.Data.Identity;
using FoodApp.Web.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;

namespace FoodApp.Web.Data
{
    public class ApplicationDbContext :  IdentityDbContext<FoodAppUser>
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

            //builder.Entity<ShoppingCart>()
            //    .HasOne<EBiletsUser>(z => z.Owner)
            //    .WithOne(z => z.UserCart)
            //    .HasForeignKey<ShoppingCart>(z => z.OwnerId);
        }

    }
}
