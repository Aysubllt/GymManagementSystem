using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Models;

namespace GymManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- 1. IDENTITY TOHUM VERİLERİ ---
            const string adminRoleId = "543c3933-289b-4401-831e-9279185a5382";
            const string userRoleId = "14686036-7975-4089-9a74-972161741544";
            const string adminUserId = "6e522967-832d-4581-9b62-302306915f01";

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
            );

            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = adminUserId,
                UserName = "b231210055@sakarya.edu.tr",
                NormalizedUserName = "B231210055@SAKARYA.EDU.TR",
                Email = "b231210055@sakarya.edu.tr",
                NormalizedEmail = "B231210055@SAKARYA.EDU.TR",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "sau")
            });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminUserId }
            );

            // --- 2. SALONLAR (5 ADET) ---
            builder.Entity<Gym>().HasData(
                new Gym { Id = 1, Name = "SAU Merkez Kampüs", Address = "Esentepe Kampüsü", OpeningTime = new TimeSpan(08, 00, 00), ClosingTime = new TimeSpan(22, 00, 00) },
                new Gym { Id = 2, Name = "Serdivan Premium", Address = "Mavi Durak Mevkii", OpeningTime = new TimeSpan(07, 00, 00), ClosingTime = new TimeSpan(23, 00, 00) },
                new Gym { Id = 3, Name = "Çarşı Şube", Address = "Adapazarı Merkez", OpeningTime = new TimeSpan(09, 00, 00), ClosingTime = new TimeSpan(21, 00, 00) },
                new Gym { Id = 4, Name = "Erenler Kompleksi", Address = "Erenler Merkez", OpeningTime = new TimeSpan(08, 30, 00), ClosingTime = new TimeSpan(22, 30, 00) },
                new Gym { Id = 5, Name = "Sapanca Life", Address = "Sapanca Sahil", OpeningTime = new TimeSpan(07, 30, 00), ClosingTime = new TimeSpan(22, 00, 00) }
            );

            // --- 3. HİZMETLER (10 ADET - Salonlara Dağıtılmış) ---
            builder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Fitness Standart", DurationMinutes = 60, Price = 500, GymId = 1 },
                new Service { Id = 2, Name = "Birebir Pilates", DurationMinutes = 45, Price = 1200, GymId = 2 },
                new Service { Id = 3, Name = "Crossfit Seansı", DurationMinutes = 50, Price = 800, GymId = 1 },
                new Service { Id = 4, Name = "Kick Boks Eğitimi", DurationMinutes = 90, Price = 1000, GymId = 3 },
                new Service { Id = 5, Name = "Yoga ve Meditasyon", DurationMinutes = 75, Price = 700, GymId = 2 },
                new Service { Id = 6, Name = "Yüzme Kursu", DurationMinutes = 60, Price = 900, GymId = 4 },
                new Service { Id = 7, Name = "Zumba Grup Dersi", DurationMinutes = 45, Price = 400, GymId = 3 },
                new Service { Id = 8, Name = "HIIT Antrenman", DurationMinutes = 30, Price = 600, GymId = 5 },
                new Service { Id = 9, Name = "Diyetisyen Danışmanlığı", DurationMinutes = 30, Price = 1100, GymId = 1 },
                new Service { Id = 10, Name = "VIP Spa & Fitness", DurationMinutes = 120, Price = 2500, GymId = 5 }
            );

            // --- 4. EĞİTMENLER (10 ADET - Salonlara Dağıtılmış) ---
            builder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, Name = "Ahmet Yılmaz", Specialty = "Vücut Geliştirme", GymId = 1, ShiftStartTime = new TimeSpan(08, 00, 00), ShiftEndTime = new TimeSpan(17, 00, 00) },
                new Trainer { Id = 2, Name = "Ayşe Demir", Specialty = "Pilates", GymId = 2, ShiftStartTime = new TimeSpan(09, 00, 00), ShiftEndTime = new TimeSpan(18, 00, 00) },
                new Trainer { Id = 3, Name = "Mehmet Kaya", Specialty = "Crossfit", GymId = 1, ShiftStartTime = new TimeSpan(13, 00, 00), ShiftEndTime = new TimeSpan(22, 00, 00) },
                new Trainer { Id = 4, Name = "Selin Yurt", Specialty = "Yoga", GymId = 2, ShiftStartTime = new TimeSpan(10, 00, 00), ShiftEndTime = new TimeSpan(19, 00, 00) },
                new Trainer { Id = 5, Name = "Caner Öz", Specialty = "Yüzme", GymId = 4, ShiftStartTime = new TimeSpan(08, 00, 00), ShiftEndTime = new TimeSpan(16, 00, 00) },
                new Trainer { Id = 6, Name = "Elif Ak", Specialty = "Kick Boks", GymId = 3, ShiftStartTime = new TimeSpan(12, 00, 00), ShiftEndTime = new TimeSpan(21, 00, 00) },
                new Trainer { Id = 7, Name = "Burak Arslan", Specialty = "Fitness", GymId = 5, ShiftStartTime = new TimeSpan(14, 00, 00), ShiftEndTime = new TimeSpan(22, 00, 00) },
                new Trainer { Id = 8, Name = "Deniz Can", Specialty = "Kardiyo", GymId = 4, ShiftStartTime = new TimeSpan(09, 00, 00), ShiftEndTime = new TimeSpan(18, 00, 00) },
                new Trainer { Id = 9, Name = "Murat Yıldız", Specialty = "Diyetisyen", GymId = 1, ShiftStartTime = new TimeSpan(09, 00, 00), ShiftEndTime = new TimeSpan(17, 00, 00) },
                new Trainer { Id = 10, Name = "Fatma Şahin", Specialty = "Zumba", GymId = 3, ShiftStartTime = new TimeSpan(15, 00, 00), ShiftEndTime = new TimeSpan(21, 00, 00) }
            );

            // --- 5. İLİŞKİ VE CASCADE SİLMEYİ ENGELLEME ---
            builder.Entity<Appointment>()
                .HasOne(a => a.Trainer)
                .WithMany(t => t.Appointments)
                .HasForeignKey(a => a.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            }
        }
    }
}