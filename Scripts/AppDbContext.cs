using Microsoft.EntityFrameworkCore;
using Monte_Karlo.Models;

namespace Monte_Karlo.DataBase
{
    public class AppDbContext : DbContext
    {
        // Таблица параметров круга
        public DbSet<CircleParams> CircleParams { get; set; }

        // Таблица результатов моделирования
        public DbSet<SimulationResult> SimulationResults { get; set; }

        // Настройка подключения к базе данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = "DataBase.db"; // Путь к файлу базы данных
            optionsBuilder.UseSqlite($"Data Source={databasePath}"); // Использование SQLite как источника данных
            //optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message)); // Логгирование (по желанию)
        }

        // Конфигурация модели данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CircleParams>()
                .HasIndex(cp => new
                {
                    cp.TotalPoints // Уникальный индекс по количеству точек
                })
                .IsUnique();
        }
    }
}
