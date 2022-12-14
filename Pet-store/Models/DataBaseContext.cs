using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pet_store.Models
{
    public partial class DataBaseContext : DbContext
    {
        public static DataBaseContext Instance { get; private set; }

        static DataBaseContext()
        {
            Instance = new DataBaseContext();
            InitBasket();
            InitUsersList();
        }

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<Basket> Baskets { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Characteristic> Characteristics { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductCharacteristic> ProductCharacteristics { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public List<ProductIsInBasket> InBasket { get; set; } = null!;
        public List<UsersList> UsersLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=petshop;user=root;password=root1234!", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"))
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("animal");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Race)
                    .HasMaxLength(45)
                    .HasColumnName("race");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.ToTable("basket");

                entity.HasIndex(e => e.IdOrder, "idOrder");

                entity.HasIndex(e => e.IdProduct, "idProduct");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("basket_ibfk_2");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("basket_ibfk_1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Characteristic>(entity =>
            {
                entity.ToTable("characteristic");

                entity.HasIndex(e => e.IdAnimal, "idAnimal");

                entity.HasIndex(e => e.IdFood, "idFood");

                entity.HasIndex(e => e.IdGame, "idGame");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdAnimal).HasColumnName("idAnimal");

                entity.Property(e => e.IdFood).HasColumnName("idFood");

                entity.Property(e => e.IdGame).HasColumnName("idGame");

                entity.HasOne(d => d.IdAnimalNavigation)
                    .WithMany(p => p.Characteristics)
                    .HasForeignKey(d => d.IdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("characteristic_ibfk_3");

                entity.HasOne(d => d.IdFoodNavigation)
                    .WithMany(p => p.Characteristics)
                    .HasForeignKey(d => d.IdFood)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("characteristic_ibfk_1");

                entity.HasOne(d => d.IdGameNavigation)
                    .WithMany(p => p.Characteristics)
                    .HasForeignKey(d => d.IdGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("characteristic_ibfk_2");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("food");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Compound)
                    .HasMaxLength(250)
                    .HasColumnName("compound");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Compound)
                    .HasMaxLength(250)
                    .HasColumnName("compound");

                entity.Property(e => e.Material)
                    .HasMaxLength(45)
                    .HasColumnName("material");

                entity.Property(e => e.Weight).HasColumnName("weight");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.IdUser, "idUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfOrder)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfOrder");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(10)
                    .HasColumnName("price");

                entity.Property(e => e.Rating).HasColumnName("rating");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("product_category");

                entity.HasIndex(e => e.IdProduct, "product_category_ibfk_1_idx");

                entity.HasIndex(e => e.IdCategory, "product_category_ibfk_2_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_category_ibfk_2");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_category_ibfk_1");
            });

            modelBuilder.Entity<ProductCharacteristic>(entity =>
            {
                entity.ToTable("product_characteristic");

                entity.HasIndex(e => e.IdProduct, "product_characteristic_ibfk_1_idx");

                entity.HasIndex(e => e.IdCharacteristic, "product_characteristic_ibfk_2_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCharacteristic).HasColumnName("idCharacteristic");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.HasOne(d => d.IdCharacteristicNavigation)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(d => d.IdCharacteristic)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_characteristic_ibfk_2");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_characteristic_ibfk_1");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.IdRole, "idRole");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirthday).HasColumnName("dateOfBirthday");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .HasColumnName("email");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(150)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasColumnType("mediumtext")
                    .HasColumnName("phone");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private static void InitBasket()
        {
            var productList = Instance.Products.ToList();
            Instance.InBasket = new(productList.Count);

            productList.ForEach(p => Instance.InBasket.Add(new()
            {
                Product = p,
                IsInBasket = false
            }));
        }

        private static void InitUsersList()
        {
            var users = Instance.Users.ToList();
            Instance.UsersLists = new(users.Count);

            users.ForEach(u => Instance.UsersLists.Add(new()
            {
                User = u,
                UserRole = Instance.Roles.First(r => r.Id == u.IdRole)
            }));
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
