<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<SensorData> SensorData { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SensorData>()
				.Property(e => e.id)
				.ValueGeneratedOnAdd();
		}
	}
}
=======
﻿using Microsoft.EntityFrameworkCore;
using Monitoring_Service.Domain.Entity;

namespace Monitoring_Service.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<SensorData> SensorData { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
>>>>>>> 7a9adf1f8c04b7d7e362b4edabbc562e1a0f96d9
