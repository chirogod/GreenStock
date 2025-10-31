using GreenStock.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<SaleModel> Sales { get; set; }
        public DbSet<CashRegisterModel> CashRegisters { get; set; }
        public DbSet<SupplierModel> Suppliers { get; set; }
        public DbSet<StoreModel> Stores { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<BrandModel> Brands { get; set; }

        public DbSet<SaleItemModel> SaleItems { get; set; }
        public DbSet<RoleModel> Roles { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=AUGUSTO\SQLEXPRESS; Database=GreenStock; Trusted_Connection=true; Encrypt=False; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Relaciones de Product ---
            modelBuilder.Entity<ProductModel>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductModel>()
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductModel>()
                .HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Relaciones de Users y Roles ---

            modelBuilder.Entity<UserRoleModel>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });    //Definir la clave primaria compuesta (la combinación de UserId y RoleId)

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.User)                  // UserRoleModel tiene UN User
                .WithMany(u => u.UserRoles)             // UserModel tiene MUCHOS UserRoles
                .HasForeignKey(ur => ur.UserId)         // La clave foránea en UserRoleModel es UserId
                .OnDelete(DeleteBehavior.Cascade);      // Opcional: Si eliminas un Usuario, se eliminan sus UserRoles

            // 3. Configurar la relación UserRoleModel (Muchos) a RoleModel (Uno)
            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.Role)                  // UserRoleModel tiene UN Role
                .WithMany(r => r.UserRoles)             // RoleModel tiene MUCHOS UserRoles
                .HasForeignKey(ur => ur.RoleId)         // La clave foranea en UserRoleModel es RoleId
                .OnDelete(DeleteBehavior.Cascade);      // Opcional: Si se elimina un Rol, se eliminan los UserRoles asociados
        }
    }
}
