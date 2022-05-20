using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RGMVC.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RGMVC.Data
{
	public class DataContext : IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }

	}
}
