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
