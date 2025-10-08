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
    }
}
