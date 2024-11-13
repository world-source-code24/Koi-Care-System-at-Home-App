using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business_Object;

public partial class KoiCareSystemAppContext : DbContext
{
    public KoiCareSystemAppContext()
    {
    }

    public KoiCareSystemAppContext(DbContextOptions<KoiCareSystemAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountTbl> AccountTbls { get; set; }

    public virtual DbSet<CartTbl> CartTbls { get; set; }

    public virtual DbSet<KoisTbl> KoisTbls { get; set; }

    public virtual DbSet<OrderDetailsTbl> OrderDetailsTbls { get; set; }

    public virtual DbSet<OrdersTbl> OrdersTbls { get; set; }

    public virtual DbSet<PondsTbl> PondsTbls { get; set; }

    public virtual DbSet<ProductsTbl> ProductsTbls { get; set; }

    public virtual DbSet<ShopsTbl> ShopsTbls { get; set; }

    public virtual DbSet<WaterParametersTbl> WaterParametersTbls { get; set; }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountTbl>(entity =>
        {
            entity.HasKey(e => e.AccId).HasName("PK__account___A471AFDA80CB25AE");

            entity.ToTable("account_tbl");

            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasDefaultValue("guest")
                .HasColumnName("role");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
        });

        modelBuilder.Entity<CartTbl>(entity =>
        {
            entity.HasKey(e => new { e.AccId, e.ProductId }).HasName("PK__cart_tbl__16A0A2CC8669FD51");

            entity.ToTable("cart_tbl");

            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Acc).WithMany(p => p.CartTbls)
                .HasForeignKey(d => d.AccId)
                .HasConstraintName("FK__cart_tbl__accId__534D60F1");

            entity.HasOne(d => d.Product).WithMany(p => p.CartTbls)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__cart_tbl__produc__5441852A");
        });

        modelBuilder.Entity<KoisTbl>(entity =>
        {
            entity.HasKey(e => e.KoiId).HasName("PK__kois_tbl__915924CF7B487A8A");

            entity.ToTable("kois_tbl");

            entity.Property(e => e.KoiId).HasColumnName("koiId");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Breed)
                .HasMaxLength(50)
                .HasColumnName("breed");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Length)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("length");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Physique)
                .HasMaxLength(50)
                .HasColumnName("physique");
            entity.Property(e => e.PondId).HasColumnName("pondId");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.Pond).WithMany(p => p.KoisTbls)
                .HasForeignKey(d => d.PondId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__kois_tbl__pondId__4222D4EF");
        });

        modelBuilder.Entity<OrderDetailsTbl>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__order_de__BAD83E4BAD89E37D");

            entity.ToTable("order_details_tbl");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalPrice");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetailsTbls)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_det__order__4E88ABD4");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetailsTbls)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_det__produ__4F7CD00D");
        });

        modelBuilder.Entity<OrdersTbl>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders_t__0809335D3A505048");

            entity.ToTable("orders_tbl");

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.StatusOrder)
                .HasMaxLength(100)
                .HasColumnName("statusOrder");
            entity.Property(e => e.StatusPayment)
                .HasMaxLength(100)
                .HasColumnName("statusPayment");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalAmount");

            entity.HasOne(d => d.Acc).WithMany(p => p.OrdersTbls)
                .HasForeignKey(d => d.AccId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__orders_tb__accId__46E78A0C");
        });

        modelBuilder.Entity<PondsTbl>(entity =>
        {
            entity.HasKey(e => e.PondId).HasName("PK__ponds_tb__74327499EEA19E34");

            entity.ToTable("ponds_tbl");

            entity.Property(e => e.PondId).HasColumnName("pondId");
            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.Depth)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("depth");
            entity.Property(e => e.DrainCount).HasColumnName("drain_count");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PumpCapacity).HasColumnName("pump_capacity");
            entity.Property(e => e.Volume).HasColumnName("volume");

            entity.HasOne(d => d.Acc).WithMany(p => p.PondsTbls)
                .HasForeignKey(d => d.AccId)
                .HasConstraintName("FK__ponds_tbl__accId__3C69FB99");
        });

        modelBuilder.Entity<ProductsTbl>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__2D10D16AC2AA778C");

            entity.ToTable("products_tbl");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductInfo)
                .HasColumnType("text")
                .HasColumnName("productInfo");
            entity.Property(e => e.ShopId).HasColumnName("shopId");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.Shop).WithMany(p => p.ProductsTbls)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__products___shopI__4BAC3F29");
        });

        modelBuilder.Entity<ShopsTbl>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__shops_tb__E5C424DC2B748FB5");

            entity.ToTable("shops_tbl");

            entity.Property(e => e.ShopId).HasColumnName("shopId");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<WaterParametersTbl>(entity =>
        {
            entity.HasKey(e => e.ParameterId).HasName("PK__water_pa__F762666B902DEF76");

            entity.ToTable("water_parameters_tbl");

            entity.Property(e => e.ParameterId).HasColumnName("parameterId");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.No2Level)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("no2Level");
            entity.Property(e => e.No3Level)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("no3Level");
            entity.Property(e => e.Note)
                .HasColumnType("text")
                .HasColumnName("note");
            entity.Property(e => e.O2Level)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("o2Level");
            entity.Property(e => e.PhLevel)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("phLevel");
            entity.Property(e => e.Po4Level)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("po4Level");
            entity.Property(e => e.PondId).HasColumnName("pondId");
            entity.Property(e => e.Salt)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("salt");
            entity.Property(e => e.Temperature)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("temperature");
            entity.Property(e => e.TotalChlorines)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("totalChlorines");

            entity.HasOne(d => d.Pond).WithMany(p => p.WaterParametersTbls)
                .HasForeignKey(d => d.PondId)
                .HasConstraintName("FK__water_par__pondI__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
