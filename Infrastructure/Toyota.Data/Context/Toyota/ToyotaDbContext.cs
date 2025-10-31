using System.Data;
using System.Reflection;
using Toyota.Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Toyota.Application.Api;
using Toyota.Shared.Entities.Common;

namespace Toyota.Data.Context.Toyota
{
    public class ToyotaDbContext : DbContext, IToyotaDbContext
    {
        public ToyotaDbContext(DbContextOptions<ToyotaDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? connectionString = ApiConfiguration.Configuration?.GetConnectionString("DefaultConnection") ?? "";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) && typeof(BaseContextEntity).IsAssignableFrom(i.GenericTypeArguments[0])));

            base.OnModelCreating(modelBuilder);
        }

        public async Task EnsureCreated()
        {
            await Database.EnsureCreatedAsync();
            if (!City.Any())
            {
                var filePath = Path.Combine("/app/Data", "cities.txt");
                if (File.Exists(filePath))
                {
                    var cities = File.ReadAllLines(filePath, System.Text.Encoding.UTF8)
                     .Where(x => !string.IsNullOrWhiteSpace(x))
                     .Select(x => new City { CityName = x.Trim() })
                     .ToList();

                    City.AddRange(cities);
                    User.Add(new User()
                    {
                        Username = ApiConfiguration.Configuration?.GetSection("SampleUser").GetSection("username").Value ?? "" ?? "admin",
                        Password = ApiConfiguration.Configuration?.GetSection("SampleUser").GetSection("password").Value ?? "" ?? "---",
                        FullName = ApiConfiguration.Configuration?.GetSection("SampleUser").GetSection("fullname").Value ?? "" ?? "Admin",
                        IsAdmin = true,
                        ExternalId = Guid.NewGuid(),
                        IsActive = true,
                    });
                    await SaveChangesAsync();
                }
            }
            Console.WriteLine("geldi5");
        }

        public new DbSet<T> Set<T>() where T : BaseContextEntity
        {
            return base.Set<T>();
        }

        public async Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolation = IsolationLevel.Unspecified, CancellationToken cancellationToken = default)
        {
            return await Database.BeginTransactionAsync(isolation, cancellationToken);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void ApplyAuditInfo()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseDataModel &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var currentUserId = ApiConfiguration.CurrentUser.Id ?? 0;
            entries.Select(entry =>
            {
                var entity = (BaseDataModel)entry.Entity;
                return entry;
            }).ToList();
        }

        public DbSet<User> User { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<VehicleServiceRecord> VehicleServiceRecord { get; set; }
        public DbSet<VehicleServiceRecordLog> VehicleServiceRecordLog { get; set; }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
