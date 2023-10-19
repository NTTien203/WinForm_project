using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Test.Model1
{
    public partial class DBQuanLySV : DbContext
    {
        public DBQuanLySV()
            : base("name=DBQuanLySV")
        {
        }

        public virtual DbSet<HeDT> HeDTs { get; set; }
        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }
        public virtual DbSet<Lop> Lops { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        public virtual DbSet<Diem> Diems { get; set; }
        public virtual DbSet<DangNhap> DangNhaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeDT>()
                .Property(e => e.MaHeDT)
                .IsFixedLength();

            modelBuilder.Entity<HeDT>()
                .HasMany(e => e.Lops)
                .WithRequired(e => e.HeDT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Khoa>()
                .Property(e => e.MaKhoa)
                .IsFixedLength();

            modelBuilder.Entity<Khoa>()
                .Property(e => e.DienThoai)
                .IsFixedLength();

            modelBuilder.Entity<Khoa>()
                .HasMany(e => e.Lops)
                .WithRequired(e => e.Khoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.MaKhoaHoc)
                .IsFixedLength();

            modelBuilder.Entity<KhoaHoc>()
                .HasMany(e => e.Lops)
                .WithRequired(e => e.KhoaHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaLop)
                .IsFixedLength();

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaKhoa)
                .IsFixedLength();

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaHeDT)
                .IsFixedLength();

            modelBuilder.Entity<Lop>()
                .Property(e => e.MaKhoaHoc)
                .IsFixedLength();

            modelBuilder.Entity<Lop>()
                .HasMany(e => e.SinhViens)
                .WithRequired(e => e.Lop)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.MaMH)
                .IsFixedLength();

            modelBuilder.Entity<MonHoc>()
                .HasMany(e => e.Diems)
                .WithRequired(e => e.MonHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaSV)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.MaLop)
                .IsFixedLength();

            modelBuilder.Entity<SinhVien>()
                .HasMany(e => e.Diems)
                .WithRequired(e => e.SinhVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaSV)
                .IsFixedLength();

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaMH)
                .IsFixedLength();
        }
    }
}
